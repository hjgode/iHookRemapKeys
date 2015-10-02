using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Resources;
using System.Reflection;

namespace tscscanedit
{
    /*
    0x00 0x00 // 0x6f - VK_DIVIDE
    0x3B 0x70 // 0x70 - VK_F1
    0x3C 0x71 // 0x71 - VK_F2
    0x3D 0x72 // 0x72 - VK_F3
    0x3E 0x73 // 0x73 - VK_F4
    */
    /// <summary>
    /// maps index (VKEY) to scancode
    /// </summary>
    class tscscan
    {
        /// <summary>
        /// the line number is equal to index
        /// </summary>
        public UInt16 scancode { get; set; }    //0x3B
        public byte index_vkey { get; set; }    //0x70
        public string comment { get; set; }     // "// 0x70  VK_F1

        public static int saveDefault()
        {
            int iRes = 0;
            if (System.Windows.Forms.MessageBox.Show("Reset windows/tscscan.txt and tscshift.txt to default?", "About to restore defaults", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return -1;
            var assembly = Assembly.GetExecutingAssembly();
            foreach (string s in assembly.GetManifestResourceNames())
                System.Diagnostics.Debug.WriteLine(s);

            var resourceName = "tscscanedit.default.tscscan.txt";
            try
            {
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        using (StreamWriter writer = new StreamWriter(@"\windows\tscscan.txt", false))
                        {
                            writer.Write(result);
                        }
                    }
                }
                resourceName = "tscscanedit.default.tscshift.txt";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        using (StreamWriter writer = new StreamWriter(@"\windows\tscshift.txt", false))
                        {
                            writer.Write(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error saving tscscan.txt or tscshift.txt" + ex.Message);
                iRes = -2;
            }
            return iRes;
        }
    }
}
