using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace tscscanedit
{
/*
0xF1 0x70 0
0xF2 0x71 0
0xF3 0x72 0
0xF4 0x73 0
0xF5 0x74 0
0xF6 0x75 0
0xF7 0x76 0
0xF8 0x77 0
0xF9 0x78 0
0xFA 0x79 0
0xFB 0x7A 0
0xFC 0x7B 0
*/
    /// <summary>
    /// maps chars to index(VKEY) inside tscscan.txt
    /// </summary>
    class tscshift
    {
        public byte charval { get; set; }       //0xF1
        public byte index_vkey { get; set; }    //0x70
        public bool shiftflag { get; set; }     //0
        public string comment { get; set; }
    }
}
