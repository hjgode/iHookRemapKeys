using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PS2SCAN
{
    class ps2scancodes
    {
        public class PS2SCANCODE
        {
            public uint scancode
            {
                get;
                set;
            }
            public VKEYS.VK_Codes.VKEY vkey
            {
                get;
                set;
            }
            public PS2SCANCODE(uint scan, VKEYS.VK_Codes.VKEY vkkey)
            {
                scancode = scan;
                vkey = vkkey;
            }
        }

        public static uint getScanCode(VKEYS.VK_Codes.VKEY vk)
        {
            uint uRes = 0;
            foreach (PS2SCANCODE ps2code in PS2SCANCODES)
            {
                if (ps2code.vkey == vk)
                {
                    uRes = ps2code.scancode;
                    break;
                }
            }
            return uRes;
        }
        public static VKEYS.VK_Codes.VKEY getVKCode(uint scan)
        {
            VKEYS.VK_Codes.VKEY uRes = 0;
            foreach (PS2SCANCODE ps2code in PS2SCANCODES)
            {
                if (ps2code.scancode == scan)
                {
                    uRes = ps2code.vkey;
                    break;
                }
            }
            return uRes;
        }

        //HexScan CodeKey),
        public static PS2SCANCODE[] PS2SCANCODES ={
            new PS2SCANCODE(0x0, 	VKEYS.VK_Codes.VKEY.VK_NOTDEF),
            new PS2SCANCODE(0x1, 	VKEYS.VK_Codes.VKEY.VK_ESCAPE),
            new PS2SCANCODE(0x2, 	VKEYS.VK_Codes.VKEY.VK_1),
            new PS2SCANCODE(0x3, 	VKEYS.VK_Codes.VKEY.VK_2),
            new PS2SCANCODE(0x4, 	VKEYS.VK_Codes.VKEY.VK_3),
            new PS2SCANCODE(0x5, 	VKEYS.VK_Codes.VKEY.VK_4),
            new PS2SCANCODE(0x6, 	VKEYS.VK_Codes.VKEY.VK_5),
            new PS2SCANCODE(0x7, 	VKEYS.VK_Codes.VKEY.VK_6),
            new PS2SCANCODE(0x8, 	VKEYS.VK_Codes.VKEY.VK_7),
            new PS2SCANCODE(0x9, 	VKEYS.VK_Codes.VKEY.VK_8),
            new PS2SCANCODE(0x0A, 	VKEYS.VK_Codes.VKEY.VK_9),
            new PS2SCANCODE(0x0B, 	VKEYS.VK_Codes.VKEY.VK_0),
            new PS2SCANCODE(0x0C, 	VKEYS.VK_Codes.VKEY.VK_HYPHEN),
            new PS2SCANCODE(0x0D, 	VKEYS.VK_Codes.VKEY.VK_ADD),
            new PS2SCANCODE(0x0E, 	VKEYS.VK_Codes.VKEY.VK_BACK),
            new PS2SCANCODE(0x0F, 	VKEYS.VK_Codes.VKEY.VK_TAB),
            new PS2SCANCODE(0x10, 	VKEYS.VK_Codes.VKEY.VK_Q),
            new PS2SCANCODE(0x11, 	VKEYS.VK_Codes.VKEY.VK_W),
            new PS2SCANCODE(0x12, 	VKEYS.VK_Codes.VKEY.VK_E),
            new PS2SCANCODE(0x13, 	VKEYS.VK_Codes.VKEY.VK_R),
            new PS2SCANCODE(0x14, 	VKEYS.VK_Codes.VKEY.VK_T),
            new PS2SCANCODE(0x15, 	VKEYS.VK_Codes.VKEY.VK_Y),
            new PS2SCANCODE(0x16, 	VKEYS.VK_Codes.VKEY.VK_U),
            new PS2SCANCODE(0x17, 	VKEYS.VK_Codes.VKEY.VK_I),
            new PS2SCANCODE(0x18, 	VKEYS.VK_Codes.VKEY.VK_O),
            new PS2SCANCODE(0x19, 	VKEYS.VK_Codes.VKEY.VK_P),
            new PS2SCANCODE(0x1A, 	VKEYS.VK_Codes.VKEY.VK_LBRACKET), //[ {),
            new PS2SCANCODE(0x1B, 	VKEYS.VK_Codes.VKEY.VK_RBRACKET),// ] }),
            new PS2SCANCODE(0x1C, 	VKEYS.VK_Codes.VKEY.VK_RETURN),
            new PS2SCANCODE(0x1D, 	VKEYS.VK_Codes.VKEY.VK_CONTROL),
            new PS2SCANCODE(0x1E, 	VKEYS.VK_Codes.VKEY.VK_A),
            new PS2SCANCODE(0x1F, 	VKEYS.VK_Codes.VKEY.VK_S),
            new PS2SCANCODE(0x20, 	VKEYS.VK_Codes.VKEY.VK_D),
            new PS2SCANCODE(0x21, 	VKEYS.VK_Codes.VKEY.VK_F),
            new PS2SCANCODE(0x22, 	VKEYS.VK_Codes.VKEY.VK_G),
            new PS2SCANCODE(0x23, 	VKEYS.VK_Codes.VKEY.VK_H),
            new PS2SCANCODE(0x24, 	VKEYS.VK_Codes.VKEY.VK_J),
            new PS2SCANCODE(0x25, 	VKEYS.VK_Codes.VKEY.VK_K),
            new PS2SCANCODE(0x26, 	VKEYS.VK_Codes.VKEY.VK_L),
            new PS2SCANCODE(0x27, 	VKEYS.VK_Codes.VKEY.VK_SEMICOLON),// ; :),
            new PS2SCANCODE(0x28, 	VKEYS.VK_Codes.VKEY.VK_APOSTROPHE),// ' "),
            new PS2SCANCODE(0x29, 	VKEYS.VK_Codes.VKEY.VK_BACKQUOTE),// ` ~),
            new PS2SCANCODE(0x2A, 	VKEYS.VK_Codes.VKEY.VK_LSHIFT),
            new PS2SCANCODE(0x2B, 	VKEYS.VK_Codes.VKEY.VK_BACKSLASH),// \ |),
            new PS2SCANCODE(0x2C, 	VKEYS.VK_Codes.VKEY.VK_Z),
            new PS2SCANCODE(0x2D, 	VKEYS.VK_Codes.VKEY.VK_X),
            new PS2SCANCODE(0x2E, 	VKEYS.VK_Codes.VKEY.VK_C),
            new PS2SCANCODE(0x2F, 	VKEYS.VK_Codes.VKEY.VK_V),
            new PS2SCANCODE(0x30, 	VKEYS.VK_Codes.VKEY.VK_B),
            new PS2SCANCODE(0x31, 	VKEYS.VK_Codes.VKEY.VK_N),
            new PS2SCANCODE(0x32, 	VKEYS.VK_Codes.VKEY.VK_M),
            new PS2SCANCODE(0x33, 	VKEYS.VK_Codes.VKEY.VK_COMMA),// , <),
            new PS2SCANCODE(0x34, 	VKEYS.VK_Codes.VKEY.VK_PERIOD),// . >),
            new PS2SCANCODE(0x35, 	VKEYS.VK_Codes.VKEY.VK_SLASH),// / ?),
            new PS2SCANCODE(0x36, 	VKEYS.VK_Codes.VKEY.VK_RSHIFT),
            new PS2SCANCODE(0x37, 	VKEYS.VK_Codes.VKEY.VK_PRINT),
            new PS2SCANCODE(0x38, 	VKEYS.VK_Codes.VKEY.VK_MENU),// Alt),
            new PS2SCANCODE(0x39, 	VKEYS.VK_Codes.VKEY.VK_SPACE),
            new PS2SCANCODE(0x3A, 	VKEYS.VK_Codes.VKEY.VK_CAPITAL),
            new PS2SCANCODE(0x3B, 	VKEYS.VK_Codes.VKEY.VK_F1),
            new PS2SCANCODE(0x3C, 	VKEYS.VK_Codes.VKEY.VK_F2),
            new PS2SCANCODE(0x3D, 	VKEYS.VK_Codes.VKEY.VK_F3),
            new PS2SCANCODE(0x3E, 	VKEYS.VK_Codes.VKEY.VK_F4),
            new PS2SCANCODE(0x3F, 	VKEYS.VK_Codes.VKEY.VK_F5),
            new PS2SCANCODE(0x40, 	VKEYS.VK_Codes.VKEY.VK_F6),
            new PS2SCANCODE(0x41, 	VKEYS.VK_Codes.VKEY.VK_F7),
            new PS2SCANCODE(0x42, 	VKEYS.VK_Codes.VKEY.VK_F8),
            new PS2SCANCODE(0x43, 	VKEYS.VK_Codes.VKEY.VK_F9),
            new PS2SCANCODE(0x44, 	VKEYS.VK_Codes.VKEY.VK_F10),
            new PS2SCANCODE(0x45, 	VKEYS.VK_Codes.VKEY.VK_NUMLOCK),// Lk),
            new PS2SCANCODE(0x46, 	VKEYS.VK_Codes.VKEY.VK_SCROLL),//Scrl ),
            //new PS2SCANCODE(0x, 	VKEYS.VK_Codes.VKEY.VK_Lk),
            new PS2SCANCODE(0x47, 	VKEYS.VK_Codes.VKEY.VK_HOME),
            new PS2SCANCODE(0x48, 	VKEYS.VK_Codes.VKEY.VK_UP),// Arrow),
            new PS2SCANCODE(0x49, 	VKEYS.VK_Codes.VKEY.VK_PRIOR),// Pg Up),
            new PS2SCANCODE(0x4A, 	VKEYS.VK_Codes.VKEY.VK_SUBTRACT),// - (num)),
            new PS2SCANCODE(0x4B, 	VKEYS.VK_Codes.VKEY.VK_NUMPAD4),// 4 Left Arrow),
            new PS2SCANCODE(0x4C, 	VKEYS.VK_Codes.VKEY.VK_NUMPAD5),// 5 (num) ),
            new PS2SCANCODE(0x4D, 	VKEYS.VK_Codes.VKEY.VK_NUMPAD6),// 6 Rt Arrow),
            new PS2SCANCODE(0x4E, 	VKEYS.VK_Codes.VKEY.VK_ADD),// + (num)),
            new PS2SCANCODE(0x4F, 	VKEYS.VK_Codes.VKEY.VK_NUMPAD1),// 1 End),
            new PS2SCANCODE(0x50, 	VKEYS.VK_Codes.VKEY.VK_NUMPAD2),// 2 Dn Arrow),
            new PS2SCANCODE(0x51, 	VKEYS.VK_Codes.VKEY.VK_NUMPAD3),// 3 Pg Dn),
            new PS2SCANCODE(0x52, 	VKEYS.VK_Codes.VKEY.VK_NUMPAD0),// 0 Ins),
            new PS2SCANCODE(0x53, 	VKEYS.VK_Codes.VKEY.VK_DECIMAL),// Del .),
            new PS2SCANCODE(0x54, 	VKEYS.VK_Codes.VKEY.VK_F11),// SH F1),
            new PS2SCANCODE(0x55, 	VKEYS.VK_Codes.VKEY.VK_F12),// SH F2),
            new PS2SCANCODE(0x56, 	VKEYS.VK_Codes.VKEY.VK_F13),// SH F3),
            new PS2SCANCODE(0x57, 	VKEYS.VK_Codes.VKEY.VK_F14),// SH F4),
            new PS2SCANCODE(0x58, 	VKEYS.VK_Codes.VKEY.VK_F15),// SH F5),
            new PS2SCANCODE(0x59, 	VKEYS.VK_Codes.VKEY.VK_F16),//SH F6),
            new PS2SCANCODE(0x5A, 	VKEYS.VK_Codes.VKEY.VK_F17),//SH F7),
            new PS2SCANCODE(0x5B, 	VKEYS.VK_Codes.VKEY.VK_F18),//SH F8),
            new PS2SCANCODE(0x5C, 	VKEYS.VK_Codes.VKEY.VK_F19),// SH F9),
            new PS2SCANCODE(0x5D, 	VKEYS.VK_Codes.VKEY.VK_F20),// SH F10),
/*            new PS2SCANCODE(0x5E, 	VKEYS.VK_Codes.VKEY.VK_Ctrl F1),
            new PS2SCANCODE(0x5F, 	VKEYS.VK_Codes.VKEY.VK_Ctrl F2),
            new PS2SCANCODE(0x60, 	VKEYS.VK_Codes.VKEY.VK_Ctrl F3),
            new PS2SCANCODE(0x61, 	VKEYS.VK_Codes.VKEY.VK_Ctrl F4),
            new PS2SCANCODE(0x62, 	VKEYS.VK_Codes.VKEY.VK_Ctrl F5),
            new PS2SCANCODE(0x63, 	VKEYS.VK_Codes.VKEY.VK_Ctrl F6),
            new PS2SCANCODE(0x64, 	VKEYS.VK_Codes.VKEY.VK_Ctrl F7),
            new PS2SCANCODE(0x65, 	VKEYS.VK_Codes.VKEY.VK_Ctrl F8),
            new PS2SCANCODE(0x66, 	VKEYS.VK_Codes.VKEY.VK_Ctrl F9),
            new PS2SCANCODE(0x67, 	VKEYS.VK_Codes.VKEY.VK_Ctrl F10),
            new PS2SCANCODE(0x68, 	VKEYS.VK_Codes.VKEY.VK_Alt F1),
            new PS2SCANCODE(0x69, 	VKEYS.VK_Codes.VKEY.VK_Alt F2),
            new PS2SCANCODE(0x6A, 	VKEYS.VK_Codes.VKEY.VK_Alt F3),
            new PS2SCANCODE(0x6B, 	VKEYS.VK_Codes.VKEY.VK_Alt F4),
            new PS2SCANCODE(0x6C, 	VKEYS.VK_Codes.VKEY.VK_Alt F5),
            new PS2SCANCODE(0x6D, 	VKEYS.VK_Codes.VKEY.VK_Alt F6),
            new PS2SCANCODE(0x6E, 	VKEYS.VK_Codes.VKEY.VK_Alt F7),
            new PS2SCANCODE(0x6F, 	VKEYS.VK_Codes.VKEY.VK_Alt F8),
            new PS2SCANCODE(0x70, 	VKEYS.VK_Codes.VKEY.VK_Alt F9),
            new PS2SCANCODE(0x71, 	VKEYS.VK_Codes.VKEY.VK_Alt F10),
            new PS2SCANCODE(0x72, 	VKEYS.VK_Codes.VKEY.VK_Ctrl PtScr),
            new PS2SCANCODE(0x73, 	VKEYS.VK_Codes.VKEY.VK_Ctrl L Arrow),
            new PS2SCANCODE(0x74, 	VKEYS.VK_Codes.VKEY.VK_Ctrl R Arrow),
            new PS2SCANCODE(0x75, 	VKEYS.VK_Codes.VKEY.VK_Ctrl End),
            new PS2SCANCODE(0x76, 	VKEYS.VK_Codes.VKEY.VK_Ctrl PgDn),
            new PS2SCANCODE(0x77, 	VKEYS.VK_Codes.VKEY.VK_Ctrl Home),
            new PS2SCANCODE(0x78, 	VKEYS.VK_Codes.VKEY.VK_Alt 1),
            new PS2SCANCODE(0x79, 	VKEYS.VK_Codes.VKEY.VK_Alt 2),
            new PS2SCANCODE(0x7A, 	VKEYS.VK_Codes.VKEY.VK_Alt 3),
            new PS2SCANCODE(0x7B, 	VKEYS.VK_Codes.VKEY.VK_Alt 4),
            new PS2SCANCODE(0x7C, 	VKEYS.VK_Codes.VKEY.VK_Alt 5),
            new PS2SCANCODE(0x7D, 	VKEYS.VK_Codes.VKEY.VK_Alt 6),
            new PS2SCANCODE(0x7E, 	VKEYS.VK_Codes.VKEY.VK_Alt 7),
            new PS2SCANCODE(0x7F, 	VKEYS.VK_Codes.VKEY.VK_Alt 8),
            new PS2SCANCODE(0x80, 	VKEYS.VK_Codes.VKEY.VK_Alt 9),
            new PS2SCANCODE(0x81, 	VKEYS.VK_Codes.VKEY.VK_Alt 0),
            new PS2SCANCODE(0x82, 	VKEYS.VK_Codes.VKEY.VK_Alt - ),
            new PS2SCANCODE(0x82, 	VKEYS.VK_Codes.VKEY.VK_Alt =),
            new PS2SCANCODE(0x84, 	VKEYS.VK_Codes.VKEY.VK_Ctrl PgUp),
*/
            new PS2SCANCODE(0x85, 	VKEYS.VK_Codes.VKEY.VK_F11),
            new PS2SCANCODE(0x86, 	VKEYS.VK_Codes.VKEY.VK_F12),
            new PS2SCANCODE(0x87, 	VKEYS.VK_Codes.VKEY.VK_F21),// VK_SH F11),
            new PS2SCANCODE(0x88, 	VKEYS.VK_Codes.VKEY.VK_F22),// SH F12),
/*          new PS2SCANCODE(0x89, 	VKEYS.VK_Codes.VKEY.VK_Ctrl F11),
            new PS2SCANCODE(0x8A, 	VKEYS.VK_Codes.VKEY.VK_Ctrl F12),
            new PS2SCANCODE(0x8B, 	VKEYS.VK_Codes.VKEY.VK_Alt F11),
            new PS2SCANCODE(0x8C, 	VKEYS.VK_Codes.VKEY.VK_Alt F12),
            new PS2SCANCODE(0x8C, 	VKEYS.VK_Codes.VKEY.VK_Ctrl Up Arrow),
            new PS2SCANCODE(0x8E, 	VKEYS.VK_Codes.VKEY.VK_Ctrl - (num)),
            new PS2SCANCODE(0x8F, 	VKEYS.VK_Codes.VKEY.VK_Ctrl 5 (num)),
            new PS2SCANCODE(0x90, 	VKEYS.VK_Codes.VKEY.VK_Ctrl + (num)),
            new PS2SCANCODE(0x91, 	VKEYS.VK_Codes.VKEY.VK_Ctrl Dn  Arrow),
            new PS2SCANCODE(0x92, 	VKEYS.VK_Codes.VKEY.VK_Ctrl Ins),
            new PS2SCANCODE(0x93, 	VKEYS.VK_Codes.VKEY.VK_Ctrl Del),
            new PS2SCANCODE(0x94, 	VKEYS.VK_Codes.VKEY.VK_Ctrl Tab),
            new PS2SCANCODE(0x95, 	VKEYS.VK_Codes.VKEY.VK_Ctrl / (num)),
            new PS2SCANCODE(0x96, 	VKEYS.VK_Codes.VKEY.VK_Ctrl * (num)),
            new PS2SCANCODE(0x97, 	VKEYS.VK_Codes.VKEY.VK_Alt Home),
            new PS2SCANCODE(0x98, 	VKEYS.VK_Codes.VKEY.VK_Alt Up Arrow),
            new PS2SCANCODE(0x99, 	VKEYS.VK_Codes.VKEY.VK_Alt PgUp),
            new PS2SCANCODE(0x9A, 	VKEYS.VK_Codes.VKEY.VK_),
            new PS2SCANCODE(0x9B, 	VKEYS.VK_Codes.VKEY.VK_Alt Left Arrow),
            new PS2SCANCODE(0x9C, 	VKEYS.VK_Codes.VKEY.VK_),
            new PS2SCANCODE(0x9D, 	VKEYS.VK_Codes.VKEY.VK_Alt Rt Arrow),
            new PS2SCANCODE(0x9E, 	VKEYS.VK_Codes.VKEY.VK_),
            new PS2SCANCODE(0x9F, 	VKEYS.VK_Codes.VKEY.VK_Alt End),
            new PS2SCANCODE(0xA0, 	VKEYS.VK_Codes.VKEY.VK_Alt Dn Arrow),
            new PS2SCANCODE(0xA1, 	VKEYS.VK_Codes.VKEY.VK_Alt PgDn),
            new PS2SCANCODE(0xA2, 	VKEYS.VK_Codes.VKEY.VK_Alt Ins),
            new PS2SCANCODE(0xA3, 	VKEYS.VK_Codes.VKEY.VK_Alt Del),
            new PS2SCANCODE(0xA4, 	VKEYS.VK_Codes.VKEY.VK_Alt / (num)),
            new PS2SCANCODE(0xA5, 	VKEYS.VK_Codes.VKEY.VK_Alt Tab),
            new PS2SCANCODE(0xA6, 	VKEYS.VK_Codes.VKEY.VK_Alt Enter (num)),
*/    };
    }
}
