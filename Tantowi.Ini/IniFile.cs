using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Tantowi.Ini
{
    public class IniFile
    {
        private const int BUFFSIZE = 1024;
        private readonly string path;

        // https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-getprivateprofilestring
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string defValue, char[] retVal, int size, string filePath);

        // https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-writeprivateprofilestringa
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);


        /// <summary>
        /// Create IniFile object
        /// </summary>
        /// <param name="Path">path to ini file</param>
        private IniFile(string Path)
        {
            path = Path;
        }


        /// <summary>
        /// Get new IniFile object
        /// </summary>
        /// <param name="path">path to ini file</param>
        /// <returns></returns>
        public static IniFile Of(String path)
        {
            return new IniFile(path);
        }
    
        /// <summary>
        /// Read a key
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <returns>the value, or empty if key not found</returns>
        public string Read(string section, string key)
        {
            //StringBuilder buffer = new StringBuilder(BUFFSIZE);
            char[] buffer = new char[BUFFSIZE];
            int len = GetPrivateProfileString(section, key, null, buffer, BUFFSIZE, this.path);
            return new string(buffer, 0, len);
        }

        /// <summary>
        /// Read a key with default value
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="defaultValue">default value if key not found</param>
        /// <returns>the value, or defaultValue if the key not found</returns>
        public string Read(string section, string key, string defaultValue)
        {
            char[] buffer = new char[BUFFSIZE];
            int len = GetPrivateProfileString(section, key, defaultValue, buffer, BUFFSIZE, this.path);
            return new string(buffer, 0, len);
        }

        /// <summary>
        /// Get list of section 
        /// </summary>
        /// <returns>list of section</returns>
        public string[] GetSections()
        {
            char[] buffer = new char[BUFFSIZE];
            int len = GetPrivateProfileString(null, null, null, buffer, BUFFSIZE, this.path);
            return SplitBuffer(buffer, len);
        }


        /// <summary>
        /// get list of key in a section
        /// </summary>
        /// <param name="section">section</param>
        /// <returns>list of key</returns>
        public string[] GetKeys(string section)
        {
            char[] buffer = new char[BUFFSIZE];
            int len = GetPrivateProfileString(section, null, null, buffer, BUFFSIZE, this.path);
            return SplitBuffer(buffer, len);
        }

        /// <summary>
        /// Split buffer into string array. Each string must be terminated by null
        /// </summary>
        /// <param name="buffer">buffer to split</param>
        /// <param name="len">length of the buffer</param>
        /// <returns></returns>
        private string[] SplitBuffer(char[] buffer, int len)
        {
            List<string> astr = new List<string>();
            int start = 0;
            for (int i = 0; i < len; i++)
            {
                if (buffer[i] == '\x0000')
                {
                    if (i > start)
                    {
                        String st = new string(buffer, start, i-start);
                        astr.Add(st);
                    }
                    start = i + 1;
                }
            }
            return astr.ToArray();
        }


        /// <summary>
        /// Write to a key
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, this.path);
        }


        /// <summary>
        /// Delete a key
        /// </summary>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        public void Delete(string section, string key)
        {
            WritePrivateProfileString(section, key, null, this.path);
        }

    }
}
