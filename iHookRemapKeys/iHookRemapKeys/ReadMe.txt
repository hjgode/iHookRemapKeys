========================================================================
    WIN32 APPLICATION : iHookRemapKeys Project Overview
========================================================================

iHookRemapKeys
	tool that will hook into keyboard messages and 'translates' F1 to F25
	key presses to a char message if a defined window is running in
	foreground
	
	Usefull to let Remote Desktop Mobile (RDM) send Function key presses to host.
	Per default, RDM does not send Function keys. By mapping function key presses
	to char messages RDM will send function key scan codes to the host if the
	RDM mapping files tscshift.txt and tscscan.txt are prepared correctly.
	
	The default iHookRemapKeys mapping will map F1 to F25 to the chars with
	hex value 0x81 to 0x99.
	
	tscshift.txt must map those chars to tscscan.txt entries
	
	tscshift.txt (first val = input char, second byte = index to tscscan.txt)
	    ...
		0x81 0x70 0
		0x82 0x71 0
		0x83 0x72 0
		0x84 0x73 0
		0x85 0x74 0
		0x86 0x75 0
		0x87 0x76 0
		0x88 0x77 0
		0x89 0x78 0
		0x8A 0x79 0
		0x8B 0x7A 0
		0x8C 0x7B 0
		...
	
	tscscan.txt (first byte is scan code, second byte = lookup value used by tscshift.txt)
	
		...
		0x3B 0x70 // 0x70 - VK_F1
		0x3C 0x71 // 0x71 - VK_F2
		0x3D 0x72 // 0x72 - VK_F3
		0x3E 0x73 // 0x73 - VK_F4
		0x3F 0x74 // 0x74 - VK_F5
		0x40 0x75 // 0x75 - VK_F6
		0x41 0x76 // 0x76 - VK_F7
		0x42 0x77 // 0x77 - VK_F8
		0x43 0x78 // 0x78 - VK_F9
		0x44 0x79 // 0x79 - VK_F10
		0x57 0x7A // 0x7a - VK_F11
		0x58 0x7B // 0x7b - VK_F12
		...
	
configuration
	
	All configuration is done by registry values at [HKLM]SOFTWARE\\Intermec\\iHook3Keymap and 
	[HKLM]SOFTWARE\\Intermec\\iHook3Keymap\\Keys
	
		[HKEY_LOCAL_MACHINE\Software\Intermec\iHook3Keymap]
		"TargetWinText"=""
		"TargetWinClass"="TSSHELLWND"

	key				type	default			comment
	TargetWinClass	string	"TSSHELLWND"	window class to look for
	TargetWinText	string	""				window title to look for
	
	At least one entry must be defined. iHookRemapKeys will look for this window as foreground window.
	If the foreground window does not match the class and or title, iHookRemapKey will NOT map the
	function keys.
	
	At [HKLM]SOFTWARE\\Intermec\\iHook3Keymap\\Keys there is a number of entries like F1, F2 up to F25.
	The byte value of F1 defines the char to be send for the F1 key press. The byte value of F2 defines
	the char to be send for the F2 key press.
	
	[HKEY_LOCAL_MACHINE\Software\Intermec\iHook3Keymap\keys]
	"F1"=hex:81
	"F2"=hex:82
	...
	
	There is no need to define all values. If a value is not defined, iHookRemap will do no replacement
	of the function key that is not listed.

autostart

	To let iHookRemapKeys start with the OS put a link to the exe inside \Windows\StartUp of the
	device.
	
arguments

	Normally iHookRemapKeys is launched without any argument.
	
	If started with "-writereg" a default registry will be written:
	
		iHookRemapKeys.exe -writereg

history

	version
	
	the version information can be found by tapping the [U] symbol on the home/today
	screen of the device after iHookRemapKeys has been started
	
		0.0.3
			initial version with hard coded chars supporting F1 to F12 only
			
		0.0.4
			supports configuration by registry
			supports F1 to F25
			
/////////////////////////////////////////////////////////////////////////////s