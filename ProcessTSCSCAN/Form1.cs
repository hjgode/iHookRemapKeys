using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProcessTSCSCAN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            readTscScan();
        }
        void readTscScan()
        {
            ProcessTSCSCAN.myTSCSCAN tsc = new myTSCSCAN(@"\windows\tscscan.txt");
            int iRes = tsc.readFile();

            foreach (tscmap tmap in g_myMap)
            {
                tsc.mapVKeyToScancode(tmap.inputVK, tmap.outScan, tmap.outChar, "mappped " +(VK_Codes.VKEY)tmap.inputVK + " to scancode 0x"+tmap.outScan.ToString("X4"));

//                tsc.mapVKeyToScancode((uint)VK_Codes.VKEY.VK_F1, 0xE3, 0x00, "mappped F1 to scancode 0xE3");
            }
            if (iRes > 0)
                tsc.saveFile(@"\mytscscan.txt");
        }

        class tscmap
        {
            public uint inputVK;
            public uint outScan;
            public uint outChar;
            public tscmap(uint iVK, uint oScn, uint oChar)
            {
                inputVK = iVK;
                outScan = oScn;
                outChar = oChar;
            }
        }
        tscmap[] g_myMap=new tscmap[]{
	        //char scan		maps	 decimal	VK_ name	original	hex
	        new tscmap(0x70, 0xE3, 0x00),	//F1	 F1=227		undef		VK_F1       0x70
	        new tscmap(0x71, 0xE4, 0x00),	//F2	 F2=228		undef
	        new tscmap(0x72, 0xE6, 0x00),	//F3	 F3=230		undef
	        new tscmap(0x73, 0xE9, 0x00),	//F4	 F4=233		undef
	        new tscmap(0x74, 0xEA, 0x00),	//F5	 F5=234		undef
	        new tscmap(0x75, 0xEB, 0x00),	//F6	 F6=235		undef
	        new tscmap(0x76, 0xEC, 0x00),	//F7	 F7=236		undef
	        new tscmap(0x77, 0xED, 0x00),	//F8	 F8=237		undef
	        new tscmap(0x78, 0xEE, 0x00),	//F9	 F9=238		undef
	        new tscmap(0x79, 0xEF, 0x00),	//F10	 F10=239    undef
	        new tscmap(0x7A, 0x89, 0x00),	//F11	 F11=137
	        new tscmap(0x7B, 0x8A, 0x00),	//F12	 F12=138
	        new tscmap(0x7C, 0x8B, 0x00),	//F13	 F13=139
	        new tscmap(0x7D, 0x8C, 0x00),	//F14	 F14=140
	        new tscmap(0x7E, 0x8D, 0x00),	//F15	 F15=141
	        new tscmap(0x7F, 0x8E, 0x00),	//F16	 F16=142
	        new tscmap(0x80, 0x8F, 0x00),	//F17	 F17=143
	        new tscmap(0x81, 0x9C, 0x00),	//F18	 F18=156
	        new tscmap(0x82, 0x9D, 0x00),	//F19	 F19=157
	        new tscmap(0x83, 0x9E, 0x00),	//F20	 F20=158	VK_F20            0x83
        };

    }
}