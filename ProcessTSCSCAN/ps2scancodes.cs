using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ProcessTSCSCAN
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
            public VK_Codes.VKEY vkey
            {
                get;
                set;
            }
            public PS2SCANCODE(uint scan, VK_Codes.VKEY vkkey)
            {
                scancode = scan;
                vkey = vkkey;
            }
        }

        public static uint getScanCode(VK_Codes.VKEY vk)
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
        public static VK_Codes.VKEY getVKCode(uint scan)
        {
            VK_Codes.VKEY uRes = 0;
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
            new PS2SCANCODE(0x0, 	VK_Codes.VKEY.VK_NOTDEF),
            new PS2SCANCODE(0x1, 	VK_Codes.VKEY.VK_ESCAPE),
            new PS2SCANCODE(0x2, 	VK_Codes.VKEY.VK_1),
            new PS2SCANCODE(0x3, 	VK_Codes.VKEY.VK_2),
            new PS2SCANCODE(0x4, 	VK_Codes.VKEY.VK_3),
            new PS2SCANCODE(0x5, 	VK_Codes.VKEY.VK_4),
            new PS2SCANCODE(0x6, 	VK_Codes.VKEY.VK_5),
            new PS2SCANCODE(0x7, 	VK_Codes.VKEY.VK_6),
            new PS2SCANCODE(0x8, 	VK_Codes.VKEY.VK_7),
            new PS2SCANCODE(0x9, 	VK_Codes.VKEY.VK_8),
            new PS2SCANCODE(0x0A, 	VK_Codes.VKEY.VK_9),
            new PS2SCANCODE(0x0B, 	VK_Codes.VKEY.VK_0),
            new PS2SCANCODE(0x0C, 	VK_Codes.VKEY.VK_HYPHEN),
            new PS2SCANCODE(0x0D, 	VK_Codes.VKEY.VK_ADD),
            new PS2SCANCODE(0x0E, 	VK_Codes.VKEY.VK_BACK),
            new PS2SCANCODE(0x0F, 	VK_Codes.VKEY.VK_TAB),
            new PS2SCANCODE(0x10, 	VK_Codes.VKEY.VK_Q),
            new PS2SCANCODE(0x11, 	VK_Codes.VKEY.VK_W),
            new PS2SCANCODE(0x12, 	VK_Codes.VKEY.VK_E),
            new PS2SCANCODE(0x13, 	VK_Codes.VKEY.VK_R),
            new PS2SCANCODE(0x14, 	VK_Codes.VKEY.VK_T),
            new PS2SCANCODE(0x15, 	VK_Codes.VKEY.VK_Y),
            new PS2SCANCODE(0x16, 	VK_Codes.VKEY.VK_U),
            new PS2SCANCODE(0x17, 	VK_Codes.VKEY.VK_I),
            new PS2SCANCODE(0x18, 	VK_Codes.VKEY.VK_O),
            new PS2SCANCODE(0x19, 	VK_Codes.VKEY.VK_P),
            new PS2SCANCODE(0x1A, 	VK_Codes.VKEY.VK_LBRACKET), //[ {),
            new PS2SCANCODE(0x1B, 	VK_Codes.VKEY.VK_RBRACKET),// ] }),
            new PS2SCANCODE(0x1C, 	VK_Codes.VKEY.VK_RETURN),
            new PS2SCANCODE(0x1D, 	VK_Codes.VKEY.VK_CONTROL),
            new PS2SCANCODE(0x1E, 	VK_Codes.VKEY.VK_A),
            new PS2SCANCODE(0x1F, 	VK_Codes.VKEY.VK_S),
            new PS2SCANCODE(0x20, 	VK_Codes.VKEY.VK_D),
            new PS2SCANCODE(0x21, 	VK_Codes.VKEY.VK_F),
            new PS2SCANCODE(0x22, 	VK_Codes.VKEY.VK_G),
            new PS2SCANCODE(0x23, 	VK_Codes.VKEY.VK_H),
            new PS2SCANCODE(0x24, 	VK_Codes.VKEY.VK_J),
            new PS2SCANCODE(0x25, 	VK_Codes.VKEY.VK_K),
            new PS2SCANCODE(0x26, 	VK_Codes.VKEY.VK_L),
            new PS2SCANCODE(0x27, 	VK_Codes.VKEY.VK_SEMICOLON),// ; :),
            new PS2SCANCODE(0x28, 	VK_Codes.VKEY.VK_APOSTROPHE),// ' "),
            new PS2SCANCODE(0x29, 	VK_Codes.VKEY.VK_BACKQUOTE),// ` ~),
            new PS2SCANCODE(0x2A, 	VK_Codes.VKEY.VK_LSHIFT),
            new PS2SCANCODE(0x2B, 	VK_Codes.VKEY.VK_BACKSLASH),// \ |),
            new PS2SCANCODE(0x2C, 	VK_Codes.VKEY.VK_Z),
            new PS2SCANCODE(0x2D, 	VK_Codes.VKEY.VK_X),
            new PS2SCANCODE(0x2E, 	VK_Codes.VKEY.VK_C),
            new PS2SCANCODE(0x2F, 	VK_Codes.VKEY.VK_V),
            new PS2SCANCODE(0x30, 	VK_Codes.VKEY.VK_B),
            new PS2SCANCODE(0x31, 	VK_Codes.VKEY.VK_N),
            new PS2SCANCODE(0x32, 	VK_Codes.VKEY.VK_M),
            new PS2SCANCODE(0x33, 	VK_Codes.VKEY.VK_COMMA),// , <),
            new PS2SCANCODE(0x34, 	VK_Codes.VKEY.VK_PERIOD),// . >),
            new PS2SCANCODE(0x35, 	VK_Codes.VKEY.VK_SLASH),// / ?),
            new PS2SCANCODE(0x36, 	VK_Codes.VKEY.VK_RSHIFT),
            new PS2SCANCODE(0x37, 	VK_Codes.VKEY.VK_PRINT),
            new PS2SCANCODE(0x38, 	VK_Codes.VKEY.VK_MENU),// Alt),
            new PS2SCANCODE(0x39, 	VK_Codes.VKEY.VK_SPACE),
            new PS2SCANCODE(0x3A, 	VK_Codes.VKEY.VK_CAPITAL),
            new PS2SCANCODE(0x3B, 	VK_Codes.VKEY.VK_F1),
            new PS2SCANCODE(0x3C, 	VK_Codes.VKEY.VK_F2),
            new PS2SCANCODE(0x3D, 	VK_Codes.VKEY.VK_F3),
            new PS2SCANCODE(0x3E, 	VK_Codes.VKEY.VK_F4),
            new PS2SCANCODE(0x3F, 	VK_Codes.VKEY.VK_F5),
            new PS2SCANCODE(0x40, 	VK_Codes.VKEY.VK_F6),
            new PS2SCANCODE(0x41, 	VK_Codes.VKEY.VK_F7),
            new PS2SCANCODE(0x42, 	VK_Codes.VKEY.VK_F8),
            new PS2SCANCODE(0x43, 	VK_Codes.VKEY.VK_F9),
            new PS2SCANCODE(0x44, 	VK_Codes.VKEY.VK_F10),
            new PS2SCANCODE(0x45, 	VK_Codes.VKEY.VK_NUMLOCK),// Lk),
            new PS2SCANCODE(0x46, 	VK_Codes.VKEY.VK_SCROLL),//Scrl ),
            //new PS2SCANCODE(0x, 	VK_Codes.VKEY.VK_Lk),
            new PS2SCANCODE(0x47, 	VK_Codes.VKEY.VK_HOME),
            new PS2SCANCODE(0x48, 	VK_Codes.VKEY.VK_UP),// Arrow),
            new PS2SCANCODE(0x49, 	VK_Codes.VKEY.VK_PRIOR),// Pg Up),
            new PS2SCANCODE(0x4A, 	VK_Codes.VKEY.VK_SUBTRACT),// - (num)),
            new PS2SCANCODE(0x4B, 	VK_Codes.VKEY.VK_NUMPAD4),// 4 Left Arrow),
            new PS2SCANCODE(0x4C, 	VK_Codes.VKEY.VK_NUMPAD5),// 5 (num) ),
            new PS2SCANCODE(0x4D, 	VK_Codes.VKEY.VK_NUMPAD6),// 6 Rt Arrow),
            new PS2SCANCODE(0x4E, 	VK_Codes.VKEY.VK_ADD),// + (num)),
            new PS2SCANCODE(0x4F, 	VK_Codes.VKEY.VK_NUMPAD1),// 1 End),
            new PS2SCANCODE(0x50, 	VK_Codes.VKEY.VK_NUMPAD2),// 2 Dn Arrow),
            new PS2SCANCODE(0x51, 	VK_Codes.VKEY.VK_NUMPAD3),// 3 Pg Dn),
            new PS2SCANCODE(0x52, 	VK_Codes.VKEY.VK_NUMPAD0),// 0 Ins),
            new PS2SCANCODE(0x53, 	VK_Codes.VKEY.VK_DECIMAL),// Del .),
            new PS2SCANCODE(0x54, 	VK_Codes.VKEY.VK_F11),// SH F1),
            new PS2SCANCODE(0x55, 	VK_Codes.VKEY.VK_F12),// SH F2),
            new PS2SCANCODE(0x56, 	VK_Codes.VKEY.VK_F13),// SH F3),
            new PS2SCANCODE(0x57, 	VK_Codes.VKEY.VK_F14),// SH F4),
            new PS2SCANCODE(0x58, 	VK_Codes.VKEY.VK_F15),// SH F5),
            new PS2SCANCODE(0x59, 	VK_Codes.VKEY.VK_F16),//SH F6),
            new PS2SCANCODE(0x5A, 	VK_Codes.VKEY.VK_F17),//SH F7),
            new PS2SCANCODE(0x5B, 	VK_Codes.VKEY.VK_F18),//SH F8),
            new PS2SCANCODE(0x5C, 	VK_Codes.VKEY.VK_F19),// SH F9),
            new PS2SCANCODE(0x5D, 	VK_Codes.VKEY.VK_F20),// SH F10),
/*            new PS2SCANCODE(0x5E, 	VK_Codes.VKEY.VK_Ctrl F1),
            new PS2SCANCODE(0x5F, 	VK_Codes.VKEY.VK_Ctrl F2),
            new PS2SCANCODE(0x60, 	VK_Codes.VKEY.VK_Ctrl F3),
            new PS2SCANCODE(0x61, 	VK_Codes.VKEY.VK_Ctrl F4),
            new PS2SCANCODE(0x62, 	VK_Codes.VKEY.VK_Ctrl F5),
            new PS2SCANCODE(0x63, 	VK_Codes.VKEY.VK_Ctrl F6),
            new PS2SCANCODE(0x64, 	VK_Codes.VKEY.VK_Ctrl F7),
            new PS2SCANCODE(0x65, 	VK_Codes.VKEY.VK_Ctrl F8),
            new PS2SCANCODE(0x66, 	VK_Codes.VKEY.VK_Ctrl F9),
            new PS2SCANCODE(0x67, 	VK_Codes.VKEY.VK_Ctrl F10),
            new PS2SCANCODE(0x68, 	VK_Codes.VKEY.VK_Alt F1),
            new PS2SCANCODE(0x69, 	VK_Codes.VKEY.VK_Alt F2),
            new PS2SCANCODE(0x6A, 	VK_Codes.VKEY.VK_Alt F3),
            new PS2SCANCODE(0x6B, 	VK_Codes.VKEY.VK_Alt F4),
            new PS2SCANCODE(0x6C, 	VK_Codes.VKEY.VK_Alt F5),
            new PS2SCANCODE(0x6D, 	VK_Codes.VKEY.VK_Alt F6),
            new PS2SCANCODE(0x6E, 	VK_Codes.VKEY.VK_Alt F7),
            new PS2SCANCODE(0x6F, 	VK_Codes.VKEY.VK_Alt F8),
            new PS2SCANCODE(0x70, 	VK_Codes.VKEY.VK_Alt F9),
            new PS2SCANCODE(0x71, 	VK_Codes.VKEY.VK_Alt F10),
            new PS2SCANCODE(0x72, 	VK_Codes.VKEY.VK_Ctrl PtScr),
            new PS2SCANCODE(0x73, 	VK_Codes.VKEY.VK_Ctrl L Arrow),
            new PS2SCANCODE(0x74, 	VK_Codes.VKEY.VK_Ctrl R Arrow),
            new PS2SCANCODE(0x75, 	VK_Codes.VKEY.VK_Ctrl End),
            new PS2SCANCODE(0x76, 	VK_Codes.VKEY.VK_Ctrl PgDn),
            new PS2SCANCODE(0x77, 	VK_Codes.VKEY.VK_Ctrl Home),
            new PS2SCANCODE(0x78, 	VK_Codes.VKEY.VK_Alt 1),
            new PS2SCANCODE(0x79, 	VK_Codes.VKEY.VK_Alt 2),
            new PS2SCANCODE(0x7A, 	VK_Codes.VKEY.VK_Alt 3),
            new PS2SCANCODE(0x7B, 	VK_Codes.VKEY.VK_Alt 4),
            new PS2SCANCODE(0x7C, 	VK_Codes.VKEY.VK_Alt 5),
            new PS2SCANCODE(0x7D, 	VK_Codes.VKEY.VK_Alt 6),
            new PS2SCANCODE(0x7E, 	VK_Codes.VKEY.VK_Alt 7),
            new PS2SCANCODE(0x7F, 	VK_Codes.VKEY.VK_Alt 8),
            new PS2SCANCODE(0x80, 	VK_Codes.VKEY.VK_Alt 9),
            new PS2SCANCODE(0x81, 	VK_Codes.VKEY.VK_Alt 0),
            new PS2SCANCODE(0x82, 	VK_Codes.VKEY.VK_Alt - ),
            new PS2SCANCODE(0x82, 	VK_Codes.VKEY.VK_Alt =),
            new PS2SCANCODE(0x84, 	VK_Codes.VKEY.VK_Ctrl PgUp),
*/
            new PS2SCANCODE(0x85, 	VK_Codes.VKEY.VK_F11),
            new PS2SCANCODE(0x86, 	VK_Codes.VKEY.VK_F12),
            new PS2SCANCODE(0x87, 	VK_Codes.VKEY.VK_F21),// VK_SH F11),
            new PS2SCANCODE(0x88, 	VK_Codes.VKEY.VK_F22),// SH F12),
/*          new PS2SCANCODE(0x89, 	VK_Codes.VKEY.VK_Ctrl F11),
            new PS2SCANCODE(0x8A, 	VK_Codes.VKEY.VK_Ctrl F12),
            new PS2SCANCODE(0x8B, 	VK_Codes.VKEY.VK_Alt F11),
            new PS2SCANCODE(0x8C, 	VK_Codes.VKEY.VK_Alt F12),
            new PS2SCANCODE(0x8C, 	VK_Codes.VKEY.VK_Ctrl Up Arrow),
            new PS2SCANCODE(0x8E, 	VK_Codes.VKEY.VK_Ctrl - (num)),
            new PS2SCANCODE(0x8F, 	VK_Codes.VKEY.VK_Ctrl 5 (num)),
            new PS2SCANCODE(0x90, 	VK_Codes.VKEY.VK_Ctrl + (num)),
            new PS2SCANCODE(0x91, 	VK_Codes.VKEY.VK_Ctrl Dn  Arrow),
            new PS2SCANCODE(0x92, 	VK_Codes.VKEY.VK_Ctrl Ins),
            new PS2SCANCODE(0x93, 	VK_Codes.VKEY.VK_Ctrl Del),
            new PS2SCANCODE(0x94, 	VK_Codes.VKEY.VK_Ctrl Tab),
            new PS2SCANCODE(0x95, 	VK_Codes.VKEY.VK_Ctrl / (num)),
            new PS2SCANCODE(0x96, 	VK_Codes.VKEY.VK_Ctrl * (num)),
            new PS2SCANCODE(0x97, 	VK_Codes.VKEY.VK_Alt Home),
            new PS2SCANCODE(0x98, 	VK_Codes.VKEY.VK_Alt Up Arrow),
            new PS2SCANCODE(0x99, 	VK_Codes.VKEY.VK_Alt PgUp),
            new PS2SCANCODE(0x9A, 	VK_Codes.VKEY.VK_),
            new PS2SCANCODE(0x9B, 	VK_Codes.VKEY.VK_Alt Left Arrow),
            new PS2SCANCODE(0x9C, 	VK_Codes.VKEY.VK_),
            new PS2SCANCODE(0x9D, 	VK_Codes.VKEY.VK_Alt Rt Arrow),
            new PS2SCANCODE(0x9E, 	VK_Codes.VKEY.VK_),
            new PS2SCANCODE(0x9F, 	VK_Codes.VKEY.VK_Alt End),
            new PS2SCANCODE(0xA0, 	VK_Codes.VKEY.VK_Alt Dn Arrow),
            new PS2SCANCODE(0xA1, 	VK_Codes.VKEY.VK_Alt PgDn),
            new PS2SCANCODE(0xA2, 	VK_Codes.VKEY.VK_Alt Ins),
            new PS2SCANCODE(0xA3, 	VK_Codes.VKEY.VK_Alt Del),
            new PS2SCANCODE(0xA4, 	VK_Codes.VKEY.VK_Alt / (num)),
            new PS2SCANCODE(0xA5, 	VK_Codes.VKEY.VK_Alt Tab),
            new PS2SCANCODE(0xA6, 	VK_Codes.VKEY.VK_Alt Enter (num)),
*/    };
    }
}
