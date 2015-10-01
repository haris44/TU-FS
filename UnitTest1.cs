using System;
using UnixFileSystem;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TU_FS
{
    [TestClass]

    public class TUFS
    {
        public Directory courants;
        public File TDD;
        public Directory TDDDirectory;
        public Directory ProMac;
        public Directory AMortJava;
        public Directory ViveBSD;
        public File CeciEstUneFile;

        [TestInitialize]
        public void SetUp()
        {
            courants = new Directory("/", true);
            courants.mkdir("TDD");
            courants.mkdir("ProMac");
            courants.mkdir("AMortJava");
            courants.createNewFile("CeciEstUneFile");
            TDD = courants.cd("TDD");
            TDDDirectory = (Directory)TDD;
            TDDDirectory.chmod(7);
            TDDDirectory.mkdir("ViveBSD");

            CeciEstUneFile = courants.cd("CeciEstUneFile");
            ProMac = (Directory)courants.cd("ProMac");
            AMortJava = (Directory)courants.cd("AMortJava");
            ViveBSD = (Directory)TDDDirectory.cd("ViveBSD");

        }



        [TestMethod]
        public void Cd()
        {
            File recup = courants.cd("TDD");
            Assert.AreEqual(recup, TDD);
        }

        [TestMethod]
        public void Mkdir()
        {
            Assert.IsTrue(courants.mkdir("Test"));            
        }

        [TestMethod]
        public void FalseMkdir()
        {
            Assert.IsFalse(courants.mkdir("TDD"));
        }

        [TestMethod]
        public void GetPath()
        {
            String recup = TDDDirectory.getPath();
            Assert.AreEqual("TDD/", recup);
        }

        [TestMethod]
        public void Ls()
        {        
            Assert.AreEqual(courants.ls().Count, 4);
        }

        [TestMethod]
        public void GetRoot()
        {
            Assert.AreEqual(ViveBSD.getRoot(), TDDDirectory);
        }

        [TestMethod]
        public void Rename()
        {
            AMortJava.Permissions = 7;
            courants.rename("AMortJava", "BonFinalement...");
            Assert.AreEqual(AMortJava.Nom, "BonFinalement...");
        }

        [TestMethod]
        public void CreateNewFile()
        {
            int pre = courants.contenu.Count;
            courants.createNewFile("TU");
            Assert.AreEqual(pre + 1, courants.contenu.Count);
        }

        [TestMethod]
        public void GetParent()
        {
            Assert.AreEqual(ViveBSD.getParent(), TDDDirectory);
        }

        [TestMethod]
        public void Search()
        {
            Assert.AreEqual(courants.search("AMortJava").Count, 1); 
        }

        [TestMethod]
        public void IsFile()
        {
            Assert.IsTrue(CeciEstUneFile.isFile());
        }

        [TestMethod]
        public void IsDirectory()
        {
            Assert.IsTrue(TDDDirectory.isDirectory());
        }

        [TestMethod]
        public void IsNotFile()
        {
            Assert.IsFalse(TDDDirectory.isFile());
        }

        [TestMethod]
        public void IsNotDirectory()
        {
            Assert.IsFalse(CeciEstUneFile.isDirectory());
        }

        [TestMethod]
        public void GetName()
        {
            Assert.AreEqual(TDDDirectory.Nom, "TDD");
        }

        [TestMethod]
        public void Delete()
        {
            int pre = courants.contenu.Count;
            courants.delete("TDD");
            Assert.AreEqual(pre - 1, courants.contenu.Count);
        }

        [TestMethod]
        public void Chmod()
        {
            AMortJava.chmod(7);
            Assert.AreEqual(AMortJava.Permissions, 7);
        }

   

    }
}
