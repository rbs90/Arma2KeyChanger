using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Key
    {
        private string _CDKey = "";
        private string _HexData = "";
        private byte[] _ByteData = new byte[15];
        public bool SkipCleaning = false;
        public bool SkipChecks = false;
        public bool UseDivider = true;
        public char CDKeyDivider = '-';
        public char HexDataDivider = ',';

        public byte[] ByteData
        {
            get { return _ByteData; }
        }

        public string CDKey
        {
            get { return _CDKey; }
            set
            {
                string vaildChars = "0123456789ABCDEFGHJKLMNPRSTVWXYZ";
                bool vaildFormat = true;
                if (!SkipCleaning)
                {
                    value = value.ToUpper();
                    value = value.Replace("-", "");
                    value = value.Replace(",", "");
                    value = value.Replace(" ", "");
                    value = value.Replace("O", "0");
                    value = value.Replace("I", "1");
                }
                if (!SkipChecks)
                {
                    if (value.Length != 24)
                        vaildFormat = false;
                    else
                    {
                        for (int i = 0; i < value.Length && vaildFormat; i++)
                        {
                            char currentChar = value[i];
                            bool vaildChar = false;
                            for (int ii = 0; ii < vaildChars.Length && !vaildChar; ii++) if (currentChar == vaildChars[ii]) vaildChar = true;
                            if (!vaildChar) vaildFormat = false;
                        }
                    }
                    
                }
                if (vaildFormat)
                {
                    _ByteData = new byte[15];
                    for (int i = 0; i < 3; i++)
                    {
                        UInt64 BitwiseResult = 0;
                        for (int ii = 0; ii < 8; ii++)
                        {
                            BitwiseResult |= (UInt64)vaildChars.IndexOf(value[i * 8 + ii]) << (ii * 5);
                        }
                        for (int ii = 0; ii < 5; ii++)
                        {
                            _ByteData[i * 5 + 5 - 1 - ii] += Convert.ToByte(BitwiseResult & 0xff);
                            BitwiseResult >>= 8;
                        }
                    }
                    _HexData = BitConverter.ToString(_ByteData);
                    if (UseDivider)
                    {
                        if (HexDataDivider != '-') _HexData = _HexData.Replace('-', HexDataDivider);
                        value = value.Insert(4, new string(CDKeyDivider, 1));
                        value = value.Insert(10, new string(CDKeyDivider, 1));
                        value = value.Insert(16, new string(CDKeyDivider, 1));
                        value = value.Insert(22, new string(CDKeyDivider, 1));
                    }
                    else
                    {
                        _HexData = _HexData.Replace('-', '\0');
                    }
                    _CDKey = value;
                }
                else
                    _HexData = "";
            }
        }

        

        public string HexData
        {
            get { return _HexData; }
            set
            {
                string vaildHexChars = "0123456789ABCDEF";
                string vaildKeyChars = "0123456789ABCDEFGHJKLMNPRSTVWXYZ";
                bool vaildFormat = true;
                if (!SkipCleaning)
                {
                    value = value.ToUpper();
                    value = value.Replace("-", "");
                    value = value.Replace(",", "");
                    value = value.Replace(" ", "");
                }
                if (!SkipChecks)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        char currentChar = value[i];
                        bool vaildChar = false;
                        for (int ii = 0; ii < vaildHexChars.Length && !vaildChar; ii++) if (currentChar == vaildHexChars[ii]) vaildChar = true;
                        if (!vaildChar) vaildFormat = false;
                    }
                }
                if (vaildFormat)
                {
                    _ByteData = Enumerable.Range(0, value.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(value.Substring(x, 2), 16)).ToArray();
                    _CDKey = "";
                    for (int i = 0; i < 3; i++)
                    {
                        UInt64 BitwiseResult = 0;
                        for (int ii = 0; ii != 5; ii++)
                        {
                            BitwiseResult = _ByteData[i * 5 + ii] | (BitwiseResult << 8);
                        }
                        for (int ii = 0; ii != 8; ii++)
                        {
                            _CDKey += vaildKeyChars[(int)(BitwiseResult & 0x1f)];
                            BitwiseResult >>= 5;
                        }
                    }
                    _HexData = BitConverter.ToString(_ByteData);
                    if (UseDivider)
                    {
                        if (HexDataDivider != '-') _HexData = _HexData.Replace('-', HexDataDivider);
                        _CDKey = _CDKey.Insert(4, new string(CDKeyDivider, 1));
                        _CDKey = _CDKey.Insert(10, new string(CDKeyDivider, 1));
                        _CDKey = _CDKey.Insert(16, new string(CDKeyDivider, 1));
                        _CDKey = _CDKey.Insert(22, new string(CDKeyDivider, 1));
                    }
                    else
                    {
                        _HexData = _HexData.Replace('-', '\0');
                    }
                }
            }
        }
    }
}
