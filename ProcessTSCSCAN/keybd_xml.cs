using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Xml.Serialization;

namespace ProcessTSCSCAN
{
    /*
     * the CK3R/CK3X/Cx/x keyboard
    <?xml version="1.0" encoding="UTF-8"?>
    <KBDMap Keyboard="C2-Numeric\0001" Date="2003/01/01 12:57:52" Absolute="0">
     <Normal>
      <KEY Page="7" Usage="0" Value="0"/>
      <KEY Page="7" Usage="1" Value="0"/>
      <KEY Page="7" Usage="2" Value="0"/>
      <KEY Page="7" Usage="3" Value="0"/>
      <KEY Page="7" Usage="36" Value="61"/>
      <KEY Page="7" Usage="37" Value="62"/>
      <KEY Page="7" Usage="38" Value="70"/>
      <KEY Page="7" Usage="33" Value="37"/>
      <KEY Page="7" Usage="34" Value="46"/>
      <KEY Page="7" Usage="35" Value="54"/>
      <KEY Page="7" Usage="30" Value="22"/>
      <KEY Page="7" Usage="31" Value="30"/>
      <KEY Page="7" Usage="32" Value="38"/>
      <KEY Page="7" Usage="44" Value="41"/>
      <KEY Page="7" Usage="55" Value="73"/>
      <KEY Page="7" Usage="39" Value="69"/>
      <KEY Page="7" Usage="45" Value="78"/>
      <KEY Page="7" Usage="40" Value="90"/>
      <KEY Page="7" Usage="234" Value="5" Shifted="1"/>         //234 is PS/2 scan code of F1   Value 5 is USB code for Keyboard_B but I setup F1->Shifted_F1
      <KEY Page="7" Usage="235" Value="6" Shifted="1"/>         //235 is PS/2 scan code of F2
      <KEY Page="7" Usage="236" Value="4" Shifted="1"/>         //236 is PS/2 scan code of F3
      <KEY Page="7" Usage="237" Value="12" Shifted="1"/>        //235 is PS/2 scan code of F4
      <KEY Page="7" Usage="238" Value="3" Shifted="1"/>         //235 is PS/2 scan code of F5
      <KEY Page="7" Usage="239" Value="11" Shifted="1"/>        //235 is PS/2 scan code of F6
      <KEY Page="7" Usage="240" Value="131" Shifted="1"/>       //235 is PS/2 scan code of F7
      <KEY Page="7" Usage="241" Value="10" Shifted="1"/>        //235 is PS/2 scan code of F8
      <KEY Page="7" Usage="242" Value="1" Shifted="1"/>         //235 is PS/2 scan code of F9
      <KEY Page="7" Usage="243" Value="9" Shifted="1"/>         //235 is PS/2 scan code of F10
     * ...
    */

    [XmlRoot("KBDMaps")]
    class KBDMaps
    {
        [XmlElement("KBDMap")]
        public KBDMap[] kbdmap;
    }
    class KBDMap
    {
        [XmlAttribute("Keyboard")]  // "C2-Numeric\0001"
        string Keyboard = "";
        [XmlAttribute("Date")]  // "2003/01/01 12:43:04"
        string date = "";
    }
    //[XmlElement("Normal")]
    //class Normal
    //{
    //    KEY[] keys;
    //}
    //[XmlElement("Orange")]
    //class Orange
    //{
    //    KEY[] keys;
    //}
    //[XmlElement("Green")]
    //class Green
    //{
    //    KEY[] keys;
    //}
    //[XmlElement("KEY")]
    class KEY
    {
        [XmlAttribute("Page")]
        string page;
        [XmlAttribute("Usage")]
        string usage;
        [XmlAttribute("Value")]
        string value;

        [XmlAttribute("Modifier")]
        string modifier;
        [XmlAttribute("Multikey")]
        string multikey;
        [XmlAttribute("Extended")]
        string extended;
        [XmlAttribute("AppLaunch")]
        string applaunch;
        [XmlAttribute("NoRepeat")]
        string norepeat;
        [XmlAttribute("VKEY")]
        string vkey;
        [XmlAttribute("NamedEvent")]
        string namedevent;
        [XmlAttribute("PlaneShift")]
        string planeshift;
    }
}
