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
        public Directory FreeBSD;
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
            ViveBSD = (Directory)TDDDirectory.cd("ViveBSD");
            ViveBSD.chmod(7);
            ViveBSD.mkdir("FreeBSD");
            FreeBSD = (Directory)ViveBSD.cd("FreeBSD");

            CeciEstUneFile = courants.cd("CeciEstUneFile");
            ProMac = (Directory)courants.cd("ProMac");
            AMortJava = (Directory)courants.cd("AMortJava");
            

        }



        [TestMethod]
        public void Cd()
        {
            File recup = courants.cd("TDD");
            Assert.AreEqual(recup, TDD);
        }

        [TestMethod]
        public void NoFileCd()
        {
            File recup = courants.cd("TDDs");
            Assert.AreEqual(recup, null);
        }

        [TestMethod]
        public void NoRightCd()
        {
            TDD.Permissions = 0;
            File recup = courants.cd("TDD");
            Assert.AreEqual(recup, null);
        }

        [TestMethod]
        public void Mkdir()
        {
            Assert.IsTrue(courants.mkdir("Test"));            
        }

        [TestMethod]
        public void MkdirSameName()
        {
            Assert.IsFalse(courants.mkdir("TDD"));
        }

        [TestMethod]
        public void MkdirNoRight()
        {
            courants.Permissions = 0;
            Assert.IsFalse(courants.mkdir("TDD"));
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
        public void GetPathRacine()
        {
            String recup = courants.getPath();
            Assert.AreEqual("", recup);
        }

        [TestMethod]
        public void GetMorePath()
        {
            String recup = FreeBSD.getPath();
            Assert.AreEqual("TDD/ViveBSD/FreeBSD/", recup);
        }

        [TestMethod]
        public void GetPathNoRight()
        {
            FreeBSD.Permissions = 0;
            String recup = FreeBSD.getPath();
            Assert.AreEqual(null, recup);
        }

        [TestMethod]
        public void Ls()
        {        
            Assert.AreEqual(courants.ls().Count, 4);
        }

        [TestMethod]
        public void LsNothing()
        {
            Assert.AreEqual(FreeBSD.ls().Count, 0);
        }

        [TestMethod]
        public void LsNoRight()
        {
            courants.Permissions = 0;
            Assert.AreEqual(FreeBSD.ls().Count, 0);
        }

        [TestMethod]
        public void GetRoot()
        {
            Assert.AreEqual(ViveBSD.getRoot(), TDDDirectory);
        }

        [TestMethod]
        public void GetMoreRoot()
        {
            Assert.AreEqual(FreeBSD.getRoot(), TDDDirectory);
        }

        [TestMethod]
        public void GetRootSlash()
        {
            Assert.AreEqual(courants.getRoot(), courants);
        }

        [TestMethod]
        public void GetRootNoRight()
        {
            ViveBSD.chmod(0);
            Assert.AreEqual(ViveBSD.getRoot(), null);
        }

        [TestMethod]
        public void Rename()
        {
            AMortJava.Permissions = 7;
            courants.rename("AMortJava", "BonFinalement...");
            Assert.AreEqual(AMortJava.Nom, "BonFinalement...");
        }

        [TestMethod]
        public void RenameExistingName()
        {
            AMortJava.Permissions = 7;
            courants.rename("AMortJava", "ProMac");
            Assert.AreEqual(AMortJava.Nom, "AMortJava");
        }

        [TestMethod]
        public void RenameNoRightChild()
        {
            AMortJava.Permissions = 0;
            courants.rename("AMortJava", "test");
            Assert.AreEqual(AMortJava.Nom, "AMortJava");
        }

        [TestMethod]
        public void RenameNoRightParent()
        {
            courants.Permissions = 0;
            courants.rename("AMortJava", "test");
            Assert.AreEqual(AMortJava.Nom, "AMortJava");
        }


        [TestMethod]
        public void CreateNewFile()
        {
            int pre = courants.contenu.Count;
            courants.createNewFile("TU");
            Assert.AreEqual(pre + 1, courants.contenu.Count);
        }


        [TestMethod]
        public void CreateNewFileExistingName()
        {
            int pre = courants.contenu.Count;
            courants.createNewFile("ProMac");
            Assert.AreEqual(pre, courants.contenu.Count);
        }

        [TestMethod]
        public void CreateNewFileNoRight()
        {
            courants.Permissions = 0;
            int pre = courants.contenu.Count;
            courants.createNewFile("TU");
            Assert.AreEqual(pre, courants.contenu.Count);
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
        public void SearchMore()
        {
            ViveBSD.Permissions = 7;
            FreeBSD.Permissions = 7;
            ViveBSD.rename("FreeBSD", "AMortJava");
            Assert.AreEqual(courants.search("AMortJava").Count, 2);
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
