using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace ProcessTSCSCAN
{
    class myTSCSCAN
    {
        string _sFile;
        List<tscscan> tscscanList = new List<tscscan>();

        public myTSCSCAN(string sFile)
        {
            _sFile = sFile;
        }

        public int mapVKeyToScancode(uint uVKey, uint uScancode, uint uCharNew, string comment)
        {
            int iRes = 0;

            //find line
            tscscan tscToChange = null;
            int listIndex = -1;
            foreach (tscscan t in tscscanList)
            {
                if (t._CharIn == uVKey)
                {
                    listIndex = tscscanList.IndexOf(t);
                    tscToChange = t;
                    break;
                }
            }
            if (tscToChange == null)
            {
                System.Diagnostics.Debug.WriteLine("No entry for: [" +uVKey.ToString()+"] ");
                return -1;
            }

            System.Diagnostics.Debug.WriteLine("About to change: [" +uVKey.ToString()+"] " + tscToChange.ToString()+ " at "+listIndex.ToString());
            tscscan tscNew = new tscscan((UInt16)uVKey, (UInt16)uCharNew, (UInt16)uScancode, comment);

            System.Diagnostics.Debug.WriteLine("New tscscan is : [" + uVKey.ToString() + "] " + tscNew.ToString() + " at " + listIndex.ToString());
            tscscanList[listIndex] = tscNew;
            
            return iRes;
        }

        public int saveFile(string sFile)
        {
            int iRet = 0;
            //tscscanList.Sort();
            using (StreamWriter sw = new StreamWriter(sFile)){
                foreach (tscscan tsc in tscscanList)
                {
                    sw.WriteLine(tsc.ToString());
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(tsc.ToString());
#endif
                }
            }
            return iRet;
        }

        public int readFile()
        {
            int iRet = 0;
            UInt16 iCharRow = 0;
            using(StreamReader sr= new StreamReader(_sFile)){
                string sLine;
                while ((sLine = sr.ReadLine()) != null)
                {
                    try
                    {
                        if (sLine.StartsWith("//"))
                            tscscanList.Add(new tscscan(iCharRow, sLine));
                        else
                        {
                            //split
                            string[] s = sLine.Split(new char[] { ' ' });
                            UInt16 newChar = 0;
                            UInt16 newScan = 0;
                            //convert from hex string to byte
                            newScan = Convert.ToUInt16(s[0], 16);   //first byte is SCANCODE
                            newChar = Convert.ToUInt16(s[1], 16);   //second byte is CHAR
                            //join rest as comment
                            string sComment = "";
                            for (int x = 2; x < s.Length; x++)
                                sComment += s[x] + " ";
                            //add
                            if (sComment.Length > 0)
                                tscscanList.Add(new tscscan(iCharRow, newChar, newScan));
                            else
                                tscscanList.Add(new tscscan(iCharRow, newChar, newScan, sComment));
                            iCharRow++;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message + " for line '" + sLine + "'\n");
                    }
                }
            }
            iRet = iCharRow;
            return iRet;
        }


    }
    /*
        // Scan Code    Expected unshifted char     comment
        0x00            0x00                        // 0x00
        0x00 0x00 // 0x01
        0x00 0x00 // 0x02
        0x00 0x00 // 0x03
        0x00 0x00 // 0x04
        0x00 0x00 // 0x05
        0x00 0x00 // 0x06
        0x00 0x00 // 0x07
        0x0e            0x00                        // 0x08 - VK_BACK
        0x0f            0x09                        // 0x09 - VK_TAB
    */
    class tscscan:IComparable
    {
        /// <summary>
        /// the char coming in (the line number)
        /// </summary>
        UInt16 uCharIN=0;
        public UInt16 _CharIn {
            get { return uCharIN; }
            set { uCharIN = value; }
        }
        /// <summary>
        /// the scancode to be used for the output char
        /// </summary>
        UInt16 uScanOut = 0;
        public UInt16 _ScanOut
        {
            get { return uScanOut; }
            set { uScanOut = value; }
        }
        /// <summary>
        /// the char to be output
        /// </summary>
        UInt16 uCharOUT = 0;
        public UInt16 _CharOut{
            get { return uCharOUT; }
            set { uCharOUT = value; }
        }
        string sComment = string.Empty;
        public string _Comment
        {
            get { return sComment; }
            set { sComment = value; }
        }
        static int iIdx = 0;
        public int _Idx
        {
            get { return iIdx; }
        }
        public bool isCommentOnly = false;

        public int CompareTo(object tsc)
        {
            if (tsc is tscscan)
            {
                tscscan tsc1 = this;
                tscscan tsc2 =(tscscan)tsc;
                if (tsc1 == null)
                {
                    if (tsc2 == null)
                    {
                        // If x is null and y is null, they're
                        // equal. 
                        return 0;
                    }
                    else
                    {
                        // If x is null and y is not null, y
                        // is greater. 
                        return -1;
                    }
                }
                else
                {
                    if (tsc2 == null)
                    // ...and y is null, x is greater.
                    {
                        return 1;
                    }
                    else
                    {
                        // ...and y is not null, compare the 
                        if (tsc1.uCharIN > tsc2.uCharIN)
                            return -1;
                        else if (tsc1.uCharIN < tsc2.uCharIN)
                            return 1;
                        else if (tsc1.uCharIN == tsc2.uCharIN)
                        {
                            if (tsc1.isCommentOnly) //comment lines before normal lines
                                return -1;
                            else
                                return 1;
                        }
                        else
                            return 0;
                    }
                }
            }
            else
                throw new ArgumentException("object is not a tscscan object");
        }

        /*
        // Scan Code        Expected unshifted char     comment
        0x00 0x00 // 0x00
        0x00 0x00 // 0x01
        0x00 0x00 // 0x02
        0x00 0x00 // 0x03
        0x00 0x00 // 0x04
        0x00 0x00 // 0x05
        0x00 0x00 // 0x06
        0x00 0x00 // 0x07
           0x0e             0x00                        // 0x08 - VK_BACK
           0x0f             0x09                        // 0x09 - VK_TAB
        */
        public override string ToString()
        {
            if (isCommentOnly)
                return "// "+this.sComment;
            if(this.sComment!=string.Empty)
                return "0x"+uScanOut.ToString("X4")+" 0x"+uCharOUT.ToString("X2")+" //"+sComment;
            else
                return "0x" + uScanOut.ToString("X4") + " 0x" + uCharOUT.ToString("X2") + //0x000F
                    " //input (line): 0x" + uCharIN.ToString("x2")+ //line 0x09
                    ", " + VK_Codes.getVK_Name(uCharIN) + // VK_ of 0x09
                    ", output (by scancode): " + ps2scancodes.getVKCode(uScanOut) +
                    ", output char code:" + VK_Codes.getVK_Name(uCharOUT);
        }

        public tscscan()
        {
        }
        
        public tscscan(UInt16 linenumber, string s)
        {
            uCharIN = linenumber;
            isCommentOnly = true;
            sComment = s;
        }
        public tscscan(UInt16 linenumber, UInt16 uOUT, UInt16 uScan)
        {
            uCharIN = linenumber;
            uCharOUT = uOUT;
            uScanOut = uScan;
            iIdx++;
        }
        public tscscan(UInt16 linenumber, UInt16 uOUT, UInt16 uScan, string s)
        {
            uCharIN = linenumber;
            uCharOUT = uOUT;
            uScanOut = uScan;
            sComment = s;
            iIdx++;
        }
    }
}
