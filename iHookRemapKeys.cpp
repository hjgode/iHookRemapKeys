// iHookRemapKeys.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "iHookRemapKeys.h"
#include "hooks.h"
#include "registry.h"

#pragma message( "Compiling " __FILE__ )

TCHAR szAppName[] = L"iHookRemapKeys_v0.0.1";

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE			g_hInst;			// current instance
HWND				g_hWndMenuBar;		// menu bar handle
HWND				g_hWnd;				// main window handle

HINSTANCE	g_hHookApiDLL	= NULL;			// Handle to loaded library (system DLL where the API is located)

TCHAR* regMainKey = L"SOFTWARE\\Intermec\\iHook3Keymap";
TCHAR* regKeys = L"SOFTWARE\\Intermec\\iHook3Keymap\\keys";
/*
			[HKEY_LOCAL_MACHINE\SOFTWARE\Intermec\iHook3Keymap]
			"ForwardKey"=hex:0
			[HKEY_LOCAL_MACHINE\SOFTWARE\Intermec\iHook3Keymap\keys]
			"VKin0"=hex:70
			"VKout0"=hex:e6
			"VKin1"=hex:70
			"VKout1"=hex:e6
*/
typedef struct {
	DWORD vkey;
	DWORD scan;
}MYKEYSTRUCT;
/*
MYKEYSTRUCT g_myMap[]={
	{0xF6, 0x00},	//F1 0x70->0xF6	0x3B},scancode=0x3b	#define VK_ATTN           0xF6
	{0xF7, 0x00},	//F2			0x3C},#define VK_CRSEL          0xF7
	{0xF8, 0x00},	//F3			0x3D},#define VK_EXSEL          0xF8
	{0xF9, 0x00},	//F4			0x3E},#define VK_EREOF          0xF9
	{0xFA, 0x00},	//F5			0x3F},#define VK_PLAY           0xFA
	{0xFB, 0x00},	//F6			0x40},#define VK_ZOOM           0xFB
	{0xFC, 0x00},	//F7			0x41},#define VK_NONAME         0xFC
	{0xFD, 0x00},	//F8			0x42},#define VK_PA1            0xFD
	{0xFE, 0x00},	//F9			0x43},#define VK_OEM_CLEAR      0xFE
	{0xA6, 0x00},	//F10			0x44},#define VK_BROWSER_BACK                  0xA6
	{0xA7, 0x00},	//F11			0x57},#define VK_BROWSER_FORWARD               0xA7
	{0xA8, 0x00},	//F12			0x58},#define VK_BROWSER_REFRESH               0xA8
};
*/
//CK_ value to use and scancode to send (if any)
MYKEYSTRUCT g_myMap[]={
	//char scan		maps	 decimal	VK_ name	original	hex
	{0xE3, 0x00},	//F1	 F1=227		undef		VK_F1       0x70
	{0xE4, 0x00},	//F2	 F2=228		undef
	{0xE6, 0x00},	//F3	 F3=230		undef
	{0xE9, 0x00},	//F4	 F4=233		undef
	{0xEA, 0x00},	//F5	 F5=234		undef
	{0xEB, 0x00},	//F6	 F6=235		undef
	{0xEC, 0x00},	//F7	 F7=236		undef
	{0xED, 0x00},	//F8	 F8=237		undef
	{0xEE, 0x00},	//F9	 F9=238		undef
	{0xEF, 0x00},	//F10	 F10=239    undef
	{0x89, 0x00},	//F11	 F11=137
	{0x8A, 0x00},	//F12	 F12=138
	{0x8B, 0x00},	//F13	 F13=139
	{0x8C, 0x00},	//F14	 F14=140
	{0x8D, 0x00},	//F15	 F15=141
	{0x8E, 0x00},	//F16	 F16=142
	{0x8F, 0x00},	//F17	 F17=143
	{0x9C, 0x00},	//F18	 F18=156
	{0x9D, 0x00},	//F19	 F19=157
	{0x9E, 0x00},	//F20	 F20=158	VK_F20            0x83
};

//global to hold keycodes and there replacements
typedef struct {
	byte VKkeyCodeIn;
	byte VKkeyCodeOut;
} hookmap;
static hookmap kMap[24];

NOTIFYICONDATA nid;
//
void ShowIcon(HWND hWnd, HINSTANCE hInst);
void RemoveIcon(HWND hWnd);

// Forward declarations of functions included in this code module:
ATOM			MyRegisterClass(HINSTANCE, LPTSTR);
BOOL			InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);

int ReadReg();
void WriteReg();

#ifndef LLKHF_LOWER_IL_INJECTED
	//#define LLKHF_EXTENDED (KF_EXTENDED>>8)
	#define LLKHF_LOWER_IL_INJECTED 0x0002
	#define LLKHF_INJECTED 0x00000010
	//#define LLKHF_ALTDOWN (KF_ALTDOWN>>8)
	//#define LLKHF_UP (KF_UP>>8)
#endif

#define myExtraInfo 0x0F	//mask
#define myInjected  0x0F	//flag

//use 1 for EVENT and 0 for POST
#define USE_POST_OR_EVENT 0

TCHAR* g_szMyEventDOWN = L"MyEventDOWN";
TCHAR* g_szMyEventUP = L"MyEventUP";
HANDLE g_hMyEventDOWN = NULL;
HANDLE g_hMyEventUP = NULL;
HANDLE g_hmyEvents[2];

// Global functions: The original Open Source
BOOL g_HookDeactivate();
BOOL g_HookActivate(HINSTANCE hInstance);

void Add2Log(TCHAR* msg, ...){
	;
}

HWND getTargetWindow(){
//	return GetActiveWindow();
	return GetForegroundWindow();
}

//
#pragma data_seg(".HOOKDATA")									//	Shared data (memory) among all instances.
HHOOK g_hInstalledLLKBDhook = NULL;						// Handle to low-level keyboard hook
//HWND hWnd	= NULL;											// If in a DLL, handle to app window receiving WM_USER+n message
#pragma data_seg()
#pragma comment(linker, "/SECTION:.HOOKDATA,RWS")		//linker directive
// The command below tells the OS that this EXE has an export function so we can use the global hook without a DLL
__declspec(dllexport) LRESULT CALLBACK g_LLKeyboardHookCallback(
   int nCode,      // The hook code
   WPARAM wParam,  // The window message (WM_KEYUP, WM_KEYDOWN, etc.)
   LPARAM lParam   // A pointer to a struct with information about the pressed key
) 
{
	/*	typedef struct {
	    DWORD vkCode;
	    DWORD scanCode;
	    DWORD flags;
	    DWORD time;
	    ULONG_PTR dwExtraInfo;
	} KBDLLHOOKSTRUCT, *PKBDLLHOOKSTRUCT;*/
	
	// Get out of hooks ASAP; no modal dialogs or CPU-intensive processes!
	// UI code really should be elsewhere, but this is just a test/prototype app
	// In my limited testing, HC_ACTION is the only value nCode is ever set to in CE
	static int iActOn = HC_ACTION;
	int iResult=0;
	bool processed_key=false;
	if (nCode == iActOn) 
	{ 
		PKBDLLHOOKSTRUCT pkbhData = (PKBDLLHOOKSTRUCT)lParam;
		HWND hwndTarget=getTargetWindow();//FindWindow(L"KeyTest3AK",L"KeyTest3AK");
		if(hwndTarget==NULL)	// if IE is not loaded or not in foreground or browser window not found
			return CallNextHookEx(g_hInstalledLLKBDhook, nCode, wParam, lParam);

#if USE_POST_OR_EVENT == 1
		//is an event set, then do not process again!
		DWORD dwWait = WaitForMultipleObjects(2, g_hmyEvents, false, 0);
		switch (dwWait){
			case WAIT_OBJECT_0://down
				ResetEvent(g_hMyEventDOWN);
				//DEBUGMSG(1, (L"g_hMyEventDOWN: self created event, do not process\n"));
				DEBUGMSG(1,(L"- SetEvent(g_hMyEventDOWN)\n"));
				return TRUE; //no further processing
				break;
			case WAIT_OBJECT_0+1://up
				ResetEvent(g_hMyEventUP);
				DEBUGMSG(1,(L"- SetEvent(g_hMyEventUP)\n"));
				//DEBUGMSG(1, (L"g_hMyEventUP: self created event, do not process\n"));
				return TRUE; //no further processing
				break;
			case WAIT_TIMEOUT:
				DEBUGMSG(1, (L"WAIT_TIMEOUT: self created event\n"));
				break;
			default:
				break;
		}
#endif
		//we are only interested in FKey press/release
		//if(pkbhData->vkCode >= VK_F1 && pkbhData->vkCode <=VK_F24){
		//if(pkbhData->vkCode > 0 && pkbhData->vkCode < 255){ //vkCodes are only between 1 and 254
		if(pkbhData->vkCode >= VK_F1 && pkbhData->vkCode <=VK_F20){	//map F1 to F20 to 
			
			DWORD newVKEY = g_myMap[pkbhData->vkCode-0x70].vkey;	//wParam
			DWORD newCode = g_myMap[pkbhData->vkCode-0x70].scan;
			DWORD newLParam = (lParam & 0xFF00FFFF)| g_myMap[pkbhData->vkCode-0x70].scan; //mask scancode and set new one

			DEBUGMSG(1,(L"hook for key 0x%08x mapped to 0x%08x...\n", pkbhData->vkCode, newVKEY));
			if(processed_key==false){
				if (wParam == WM_KEYUP)
				{
					//synthesize a WM_KEYUP
					DEBUGMSG(1,(L"posting WM_KEYUP 0x%08x to 0x%08x, lParam=0x%08x...\n", newVKEY, hwndTarget, lParam));
					processed_key=true;
#if USE_POST_OR_EVENT == 0
					PostMessage(hwndTarget, WM_KEYUP, newVKEY, newLParam);
#else
					//keybd_event will create another loop to here!!! Therefor set an event
					DEBUGMSG(1,(L"+ SetEvent(g_hMyEventUP)\n"));
					SetEvent(g_hMyEventUP);
					keybd_event(newVKEY, newCode, pkbhData->flags, pkbhData->dwExtraInfo);
					//flags are KEYEVENTF_EXTENDEDKEY or KEYEVENTF_KEYUP 
#endif
				}
				else if (wParam == WM_KEYDOWN)
				{
					//synthesize a WM_KEYDOWN
					DEBUGMSG(1,(L"posting WM_KEYDOWN 0x%08x to 0x%08x, lParam=0x%08x...\n", newVKEY, hwndTarget, lParam));
					processed_key=true;
#if USE_POST_OR_EVENT == 0
					PostMessage(hwndTarget, WM_KEYDOWN, newVKEY, newLParam);
#else
					SetEvent(g_hMyEventDOWN);
					DEBUGMSG(1,(L"+ SetEvent(g_hMyEventDOWN)\n"));
					keybd_event(newVKEY, newCode, pkbhData->flags, pkbhData->dwExtraInfo);
					//flags are KEYEVENTF_EXTENDEDKEY or KEYEVENTF_KEYUP 
#endif
				}
				else if (wParam == WM_CHAR)	//this will never happen
				{
					DEBUGMSG(1, (L"WM_CHAR: 0x%x\n", pkbhData->vkCode));
				}
			}
		}
		else if(pkbhData->vkCode==VK_TSTAR){
			DEBUGMSG(1, (L"VK_TSTAR: 0x%x\n", pkbhData->vkCode));
		}
		else if(pkbhData->vkCode==VK_TPOUND){
			DEBUGMSG(1, (L"VK_TPOUND: 0x%x\n", pkbhData->vkCode));
		}
		else if(pkbhData->vkCode==VK_HYPHEN){
			//got VK_PERIOD wParam=0x100 lParam=0x1f7cfc44 
			//got VK_PERIOD wParam=0x101 lParam=0x1f7cfc44
			/* down,char,up:
			VK_COMMA(0xbc)	1
			,(0x2c)	1
			VK_COMMA(0xbc)	c0000001

			VK_PERIOD(0xbe)	490001
			.(0x2e)	490001
			VK_PERIOD(0xbe)	c0490001
			*/
			if(processed_key==false){
				DEBUGMSG(1, (L"got VK_HYPHEN wParam=0x%02x lParam=0x%02x \n", wParam, lParam));
				if(wParam==WM_KEYUP){
					processed_key=TRUE;
					keybd_event(VK_COMMA, 0x00, KEYEVENTF_KEYUP | 0, 0);
					//PostMessage(hwndBrowserComponent , WM_CHAR, 0x2c,   0x00000001);
					//PostMessage(hwndBrowserComponent , WM_KEYUP, VK_COMMA,   0xC0000001);
				}else if(wParam==WM_KEYDOWN){
					processed_key=TRUE;
					keybd_event(VK_COMMA, 0x00, 0, 0);
					//PostMessage(hwndBrowserComponent, WM_KEYDOWN, VK_COMMA, 0x00000001 );
				}
			}
		}
	}//nCode == iActOn
	else{
		DEBUGMSG(1, (L"Got unknown action code: 0x%08x\n", nCode));
	}
	if(processed_key==true){
		processed_key=false;
		DEBUGMSG(1, (L"leaving hook without forward\n"));
		return TRUE;
	}
	else{
		DEBUGMSG(1, (L"leaving hook with forward\n"));
		return CallNextHookEx(g_hInstalledLLKBDhook, nCode, wParam, lParam);
	}
}

BOOL g_HookActivate(HINSTANCE hInstance)
{
	// We manually load these standard Win32 API calls (Microsoft says "unsupported in CE")
	SetWindowsHookEx		= NULL;
	CallNextHookEx			= NULL;
	UnhookWindowsHookEx	= NULL;

	// Load the core library. If it's not found, you've got CErious issues :-O
	DEBUGMSG(1, (_T("LoadLibrary(coredll.dll)...")));
	g_hHookApiDLL = LoadLibrary(_T("coredll.dll"));
	if(g_hHookApiDLL == NULL) 
		return false;
	else {
		// Load the SetWindowsHookEx API call (wide-char)
		DEBUGMSG(1, (_T("OK\nGetProcAddress(SetWindowsHookExW)...")));
		SetWindowsHookEx = (_SetWindowsHookExW)GetProcAddress(g_hHookApiDLL, _T("SetWindowsHookExW"));
		if(SetWindowsHookEx == NULL) 
			return false;
		else
		{
			// Load the hook.  Save the handle to the hook for later destruction.
			DEBUGMSG(1, (_T("OK\nCalling SetWindowsHookEx...")));
			g_hInstalledLLKBDhook = SetWindowsHookEx(WH_KEYBOARD_LL, g_LLKeyboardHookCallback, hInstance, 0);
			if(g_hInstalledLLKBDhook == NULL){ 
				DEBUGMSG(1, (L"Calling SetWindowsHookEx FAILED: %i\n", GetLastError()));
				return false;
			}
		}

		// Get pointer to CallNextHookEx()
		DEBUGMSG(1, (_T("OK\nGetProcAddress(CallNextHookEx)...")));
		CallNextHookEx = (_CallNextHookEx)GetProcAddress(g_hHookApiDLL, _T("CallNextHookEx"));
		if(CallNextHookEx == NULL) 
			return false;

		// Get pointer to UnhookWindowsHookEx()
		DEBUGMSG(1, (_T("OK\nGetProcAddress(UnhookWindowsHookEx)...")));
		UnhookWindowsHookEx = (_UnhookWindowsHookEx)GetProcAddress(g_hHookApiDLL, _T("UnhookWindowsHookEx"));
		if(UnhookWindowsHookEx == NULL) 
			return false;
	}
	//DEBUGMSG(1, (_T("OK\nEverything loaded OK\n"));
	g_hMyEventDOWN=CreateEvent(NULL, TRUE, FALSE, g_szMyEventDOWN);
	g_hMyEventUP=CreateEvent(NULL, TRUE, FALSE, g_szMyEventUP);
	g_hmyEvents[0]=g_hMyEventDOWN;
	g_hmyEvents[1]=g_hMyEventUP;

	return true;
}


BOOL g_HookDeactivate()
{
	//DEBUGMSG(1, (_T("Uninstalling hook..."));
	if(g_hInstalledLLKBDhook != NULL)
	{
		UnhookWindowsHookEx(g_hInstalledLLKBDhook);		// Note: May not unload immediately because other apps may have me loaded
		g_hInstalledLLKBDhook = NULL;
	}

	//DEBUGMSG(1, (_T("OK\nUnloading coredll.dll..."));
	if(g_hHookApiDLL != NULL)
	{
		FreeLibrary(g_hHookApiDLL);
		g_hHookApiDLL = NULL;
	}
	//DEBUGMSG(1, (_T("OK\nEverything unloaded OK\n"));
	return true;
}

void readReg(){
	int iRes = OpenKey(regMainKey);
	
}
void writeReg(){
}

int WINAPI WinMain(HINSTANCE hInstance,
                   HINSTANCE hPrevInstance,
                   LPTSTR    lpCmdLine,
                   int       nCmdShow)
{
	MSG msg;

	// Perform application initialization:
	if (!InitInstance(hInstance, nCmdShow)) 
	{
		return FALSE;
	}

	HACCEL hAccelTable;
	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_IHOOKREMAPKEYS));

	//Notification icon
	Add2Log(L"Adding notification icon\r\n", FALSE);
	HICON hIcon;
	hIcon=(HICON) LoadImage (g_hInst, MAKEINTRESOURCE (IHOOK_STARTED), IMAGE_ICON, 16,16,0);
	nid.cbSize = sizeof (NOTIFYICONDATA);
	nid.hWnd = g_hWnd;
	nid.uID = 1;
	nid.uFlags = NIF_ICON | NIF_MESSAGE;
	// NIF_TIP not supported    
	nid.uCallbackMessage = MYMSG_TASKBARNOTIFY;
	nid.hIcon = hIcon;
	nid.szTip[0] = '\0';
	BOOL res = Shell_NotifyIcon (NIM_ADD, &nid);
	if(!res){
		DEBUGMSG(1 ,(L"Could not add taskbar icon. LastError=%i\r\n", GetLastError() ));
		Add2Log(L"Could not add taskbar icon. LastError=%i (0x%x)\r\n", GetLastError(), GetLastError());
	}else
		Add2Log(L"Taskbar icon added.\r\n", FALSE);

	// Main message loop:
	while (GetMessage(&msg, NULL, 0, 0)) 
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg)) 
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int) msg.wParam;
}

//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
//  COMMENTS:
//
ATOM MyRegisterClass(HINSTANCE hInstance, LPTSTR szWindowClass)
{
	WNDCLASS wc;

	wc.style         = CS_HREDRAW | CS_VREDRAW;
	wc.lpfnWndProc   = WndProc;
	wc.cbClsExtra    = 0;
	wc.cbWndExtra    = 0;
	wc.hInstance     = hInstance;
	wc.hIcon         = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_IHOOKREMAPKEYS));
	wc.hCursor       = 0;
	wc.hbrBackground = (HBRUSH) GetStockObject(WHITE_BRUSH);
	wc.lpszMenuName  = 0;
	wc.lpszClassName = szWindowClass;

	return RegisterClass(&wc);
}

//
//   FUNCTION: InitInstance(HINSTANCE, int)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
    HWND hWnd;
    TCHAR szTitle[MAX_LOADSTRING];		// title bar text
    TCHAR szWindowClass[MAX_LOADSTRING];	// main window class name

    g_hInst = hInstance; // Store instance handle in our global variable

    // SHInitExtraControls should be called once during your application's initialization to initialize any
    // of the device specific controls such as CAPEDIT and SIPPREF.
    SHInitExtraControls();

    LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING); 
    LoadString(hInstance, IDC_IHOOKREMAPKEYS, szWindowClass, MAX_LOADSTRING);

    //If it is already running, then focus on the window, and exit
    hWnd = FindWindow(szWindowClass, szTitle);	
    if (hWnd) 
    {
        // set focus to foremost child window
        // The "| 0x00000001" is used to bring any owned windows to the foreground and
        // activate them.
        SetForegroundWindow((HWND)((ULONG) hWnd | 0x00000001));
        return 0;
    } 

    if (!MyRegisterClass(hInstance, szWindowClass))
    {
    	return FALSE;
    }

    hWnd = CreateWindow(szWindowClass, szTitle, WS_VISIBLE,
        CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, NULL, NULL, hInstance, NULL);

    if (!hWnd)
    {
        return FALSE;
    }

    // When the main window is created using CW_USEDEFAULT the height of the menubar (if one
    // is created is not taken into account). So we resize the window after creating it
    // if a menubar is present
    if (g_hWndMenuBar)
    {
        RECT rc;
        RECT rcMenuBar;

        GetWindowRect(hWnd, &rc);
        GetWindowRect(g_hWndMenuBar, &rcMenuBar);
        rc.bottom -= (rcMenuBar.bottom - rcMenuBar.top);
		
        MoveWindow(hWnd, rc.left, rc.top, rc.right-rc.left, rc.bottom-rc.top, FALSE);
    }

    //ShowWindow(hWnd, nCmdShow);
	ShowWindow(hWnd, SW_MINIMIZE);
    UpdateWindow(hWnd);


    return TRUE;
}

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE:  Processes messages for the main window.
//
//  WM_COMMAND	- process the application menu
//  WM_PAINT	- Paint the main window
//  WM_DESTROY	- post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    int wmId, wmEvent;
    PAINTSTRUCT ps;
    HDC hdc;

    static SHACTIVATEINFO s_sai;
	
    switch (message) 
    {
        case WM_COMMAND:
            wmId    = LOWORD(wParam); 
            wmEvent = HIWORD(wParam); 
            // Parse the menu selections:
            switch (wmId)
            {
                case IDM_HELP_ABOUT:
                    DialogBox(g_hInst, (LPCTSTR)IDD_ABOUTBOX, hWnd, About);
                    break;
                case IDM_OK:
                    SendMessage (hWnd, WM_CLOSE, 0, 0);				
                    break;
                default:
                    return DefWindowProc(hWnd, message, wParam, lParam);
            }
            break;
        case WM_CREATE:
            SHMENUBARINFO mbi;

            memset(&mbi, 0, sizeof(SHMENUBARINFO));
            mbi.cbSize     = sizeof(SHMENUBARINFO);
            mbi.hwndParent = hWnd;
            mbi.nToolBarId = IDR_MENU;
            mbi.hInstRes   = g_hInst;

            if (!SHCreateMenuBar(&mbi)) 
            {
                g_hWndMenuBar = NULL;
            }
            else
            {
                g_hWndMenuBar = mbi.hwndMB;
            }

            // Initialize the shell activate info structure
            memset(&s_sai, 0, sizeof (s_sai));
            s_sai.cbSize = sizeof (s_sai);

			readReg();

			if (g_HookActivate(g_hInst))
			{
				Add2Log(L"g_HookActivate loaded OK\r\n", FALSE);
				MessageBeep(MB_OK);
				//system bar icon
				//ShowIcon(hwnd, g_hInstance);
				DEBUGMSG(1, (L"Hook loaded OK"));
			}
			else
			{
				MessageBeep(MB_ICONEXCLAMATION);
				Add2Log(L"g_HookActivate FAILED. EXIT!\r\n", FALSE);
				MessageBox(g_hWnd, L"Could not hook. Already running a copy of iHook3Keymap? Will exit now.", szAppName, MB_OK | MB_ICONEXCLAMATION);
				PostQuitMessage(-1);
			}

            break;
        case WM_PAINT:
            hdc = BeginPaint(hWnd, &ps);
            
            // TODO: Add any drawing code here...
            
            EndPaint(hWnd, &ps);
            break;
		case MYMSG_TASKBARNOTIFY:
				switch (lParam) {
					case WM_LBUTTONUP:
						//ShowWindow(hwnd, SW_SHOWNORMAL);
						SetWindowPos(g_hWnd, HWND_TOPMOST, 0,0,0,0, SWP_NOSIZE | SWP_NOREPOSITION | SWP_SHOWWINDOW);
						if (MessageBox(g_hWnd, L"Hook is loaded. End hooking?", szAppName, 
							MB_YESNO | MB_ICONQUESTION | MB_APPLMODAL | MB_SETFOREGROUND | MB_TOPMOST)==IDYES)
						{
							g_HookDeactivate();
							Shell_NotifyIcon(NIM_DELETE, &nid);
							PostQuitMessage (0) ; 
						}
						ShowWindow(g_hWnd, SW_HIDE);
					}
			return 0;
			break;
        case WM_DESTROY:
            CommandBar_Destroy(g_hWndMenuBar);
            PostQuitMessage(0);
            break;

        case WM_ACTIVATE:
            // Notify shell of our activate message
            SHHandleWMActivate(hWnd, wParam, lParam, &s_sai, FALSE);
            break;
        case WM_SETTINGCHANGE:
            SHHandleWMSettingChange(hWnd, wParam, lParam, &s_sai);
            break;

        default:
            return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

// Message handler for about box.
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
        case WM_INITDIALOG:
            {
                // Create a Done button and size it.  
                SHINITDLGINFO shidi;
                shidi.dwMask = SHIDIM_FLAGS;
                shidi.dwFlags = SHIDIF_DONEBUTTON | SHIDIF_SIPDOWN | SHIDIF_SIZEDLGFULLSCREEN | SHIDIF_EMPTYMENU;
                shidi.hDlg = hDlg;
                SHInitDialog(&shidi);
            }
            return (INT_PTR)TRUE;

        case WM_COMMAND:
            if (LOWORD(wParam) == IDOK)
            {
                EndDialog(hDlg, LOWORD(wParam));
                return TRUE;
            }
            break;

        case WM_CLOSE:
            EndDialog(hDlg, message);
            return TRUE;

    }
    return (INT_PTR)FALSE;
}
