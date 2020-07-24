using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using Tantowi.Ini;

namespace Tantowi.Ini.Test
{

    [TestFixture]
    class IniFileTest
    {
        readonly string TESTFILE = "d:\\test1.ini";
        readonly string TESTFILE2 = "d:\\test2.ini";


        [SetUp]
        protected void SetUp()
        {
        }


        [Test]
        public void WriteTest1()
        {
            IniFile ini = IniFile.Of(TESTFILE);
            ini.Write("config", "name", "Tantowi Mustofa");
            ini.Write("config", "city", "Sidoarjo");
            ini.Write("access", "login", "yes");
            ini.Write("access", "profile", "yes");
            ini.Write("access", "stream", "no");

            string result = File.ReadAllText(TESTFILE);
            //File.Delete(TESTFILE);

            string expected = "[config]\r\nname=Tantowi Mustofa\r\ncity=Sidoarjo\r\n[access]\r\nlogin=yes\r\nprofile=yes\r\nstream=no\r\n";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ReadTest1()
        {
            IniFile ini = IniFile.Of(TESTFILE);
            string name = ini.Read("config", "name");
            Assert.AreEqual("Tantowi Mustofa", name);
        }

        [Test]
        public void ReadTest2()
        {
            IniFile ini = IniFile.Of(TESTFILE);
            string login = ini.Read("access", "login");
            Assert.AreEqual("yes", login);
        }

        [Test]
        public void ReadTest3()
        {
            IniFile ini = IniFile.Of(TESTFILE);
            string name = ini.Read("config", "city");
            Assert.AreEqual("Sidoarjo", name);
        }



        [Test]
        public void Test2()
        {
            IniFile ini = IniFile.Of(TESTFILE);
            String[] sections = ini.GetSections();

            Assert.AreEqual(2, sections.Length);
            Assert.AreEqual("config", sections[0]);
            Assert.AreEqual("access", sections[1]);
        }

        [Test]
        public void Test3()
        {
            IniFile ini = IniFile.Of(TESTFILE);
            String[] keys = ini.GetKeys("config");

            Assert.AreEqual(2, keys.Length);
            Assert.AreEqual("name", keys[0]);
            Assert.AreEqual("city", keys[1]);
        }

        [Test]
        public void Test4()
        {
            IniFile ini = IniFile.Of(TESTFILE);
            String[] keys = ini.GetKeys("access");

            Assert.AreEqual(3, keys.Length);
            Assert.AreEqual("login", keys[0]);
            Assert.AreEqual("profile", keys[1]);
            Assert.AreEqual("stream", keys[2]);
        }

        public void UnicodeTest1()
        {
            IniFile ini = IniFile.Of(TESTFILE2);
            string finish = "إنهاء";
            string finish2 = "完";
            ini.Write("config", "status", finish);
            ini.Write("config", "status2", finish2);

            string result = File.ReadAllText(TESTFILE2);
            //File.Delete(TESTFILE2);

            string expected = "[config]\r\nstatus=إنهاء\r\nstatus2=完\r\n";
            Assert.AreEqual(expected, result);
        }
    }
}