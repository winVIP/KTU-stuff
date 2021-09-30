namespace Projektas_Irankiai.Tests.Controllers
{
    using Projektas_Irankiai.Controllers;
    using System;
    using NUnit.Framework;
    using Projektas_Irankiai.Models;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Autofac.Extras.Moq;
    using MySql.Data;
    using MySql.Data.MySqlClient;
    using Moq;
    using System.Data;
    using System.IO;
    using System.Web;

    [TestFixture]
    public class ImageControllerTests
    {
        private IDataBase dataBase;

        [Test]
        public void CanConstruct()
        {
            var instance = new ImageController(dataBase);
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanCallDeleteFileFromDB()
        {
            string filePath = @"C:\Users\vyten\Desktop\Irankiu-projektas\Projektas Irankiai\ImagesUploaded\test123.jpg";
            var testfile = File.Create(filePath);
            testfile.Close();
            Models.Image imgToBeDeleted = new Models.Image();
            imgToBeDeleted.path = filePath;

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.deleteData(It.IsAny<string>()));

            ImageController testController = new ImageController(mock.Object);

            testController.DeleteImageFromDB(imgToBeDeleted);

            bool result = File.Exists(filePath);
            if (result == true)
            {
                File.Delete(filePath);
            }
            Assert.False(result);
        }

        [Test]
        public void CanCallEditImageInDB()
        {
            Models.Image imgToBeEdited = new Models.Image();
            imgToBeEdited.name = "TestName";
            imgToBeEdited.id = 1;

            string expected = "UPDATE Paveikslelis SET Pavadinimas = '" + imgToBeEdited.name + "' WHERE id_Paveikslelis = " + imgToBeEdited.id;

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.editData(It.IsAny<string>()));
            
            ImageController testController = new ImageController(mock.Object);
            testController.EditImageInDB(imgToBeEdited);

            string actual = mock.Invocations[0].Arguments[0].ToString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanCallInsertImageToSQL()
        {
            string filePath = @"C:\Users\vyten\Desktop\Irankiu-projektas\Projektas Irankiai\ImagesUploaded\test123.jpg";
            Models.Image imgToBeInserted = new Models.Image();
            imgToBeInserted.name = "TestImage";
            imgToBeInserted.path = filePath;
            imgToBeInserted.tag = "TestTag";

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.insertData(It.IsAny<string>()));

            string[] photoData = { "Name", "true", "C:/somepath", "5.5", "true", "some text", "1", "1", "2" };
            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            //mock.CallBase = true;
            mock.Setup(x => x.selectData("SELECT * FROM Paveikslelis")).Returns(testList);

            ImageController testController = new ImageController(mock.Object);
            testController.InsertImageToSQL(imgToBeInserted);

            string expected1 = "INSERT INTO Paveikslelis VALUES " + imgToBeInserted.toSqlValues();
            string expected2 = "INSERT INTO `Paveikslelis_Zyme`(`fk_Zymeid_Zyme`, `fk_Paveikslelisid_Paveikslelis`) VALUES (" + imgToBeInserted.tag + "," + (testController.GetMaxID() + 1) + ")";

            string actual1 = mock.Invocations[1].Arguments[0].ToString();
            string actual2 = mock.Invocations[2].Arguments[0].ToString();

            Assert.AreEqual(expected1, actual1);
            Assert.AreEqual(expected2, actual2);
        }

        [Test]
        public void CanCallGeneratedImagesFromDB()
        {
            Mock<IDataBase> mock = new Mock<IDataBase>();

            string[] photoData = { "Name", "true", "C:/Generated", "5.5", "true", "some text", "1", "1", "2" };
            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            //mock.CallBase = true;
            mock.Setup(x => x.selectData("SELECT * FROM Paveikslelis")).Returns(testList);

            ImageController testController = new ImageController(mock.Object);
            int actual = testController.GeneratedImagesFromDB().Count;

            Assert.AreEqual(1, actual);
        }

        [Test]
        public void CanCallGetImages()
        {
            string[] photoData = { "Name", "true", "C:/somepath", "5.5", "true", "some text", "1", "1", "2" };
            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.selectData(It.IsAny<string>())).Returns(testList);

            ImageController testController = new ImageController(mock.Object);
            int actual = testController.GetImages().Count;

            Assert.AreEqual(testList.Count, actual);
        }

        [Test]
        public void CanCallGetRecommendedImages()
        {
            string[] photoData = { "Name", "true", "C:/somepath", "5.5", "true", "some text", "1", "1", "2" };
            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.selectData(It.IsAny<string>())).Returns(testList);

            ImageController testController = new ImageController(mock.Object);
            int actual = testController.GetRecommendedImages().Count;

            Assert.AreEqual(testList.Count, actual);
        }

        [Test]
        public void CanCallGetInvImages()
        {
            string[] photoData = { "Name", "C:/somepath", "1", "1" };
            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.selectData(It.IsAny<string>())).Returns(testList);

            ImageController testController = new ImageController(mock.Object);
            int actual = testController.GetInvImages().Count;

            Assert.AreEqual(testList.Count, actual);
        }

        [Test]
        public void CanCallGetTradeOffers()
        {
            string[] photoData = { "Test message for offer", "1", "1", "2" };
            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.selectData(It.IsAny<string>())).Returns(testList);

            ImageController testController = new ImageController(mock.Object);
            int actual = testController.GetTradeOffers().Count;

            Assert.AreEqual(testList.Count, actual);
        }

        [Test]
        public void CanCallGetAllTradeOffers()
        {
            string[] photoData = { "Test message for offer", "1", "1", "2" };
            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.selectData(It.IsAny<string>())).Returns(testList);

            ImageController testController = new ImageController(mock.Object);
            int actual = testController.GetTradeOffers().Count;

            Assert.AreEqual(testList.Count, actual);
        }

        [Test]
        public void CanCallGetTemplates()
        {
            string[] photoData = { "Name", "true", "C:/somepath", "5.5", "true", "some text", "1", "1", "2" };
            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.selectData(It.IsAny<string>())).Returns(testList);

            ImageController testController = new ImageController(mock.Object);
            int actual = testController.GetTemplates().Count;

            Assert.AreEqual(testList.Count, actual);
        }

        [Test]
        public void CanCallGetUsers()
        {
            string[] photoData = { "Name", "name@email.com", "password", "lastname", "1", "1"};
            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.selectData(It.IsAny<string>())).Returns(testList);

            ImageController testController = new ImageController(mock.Object);
            int actual = testController.GetUsers().Count;

            Assert.AreEqual(testList.Count, actual);
        }

        [Test]
        public void CanCallGetAllID()
        {
            string[] photoData = { "Name", "true", "C:/somepath", "5.5", "true", "some text", "1", "1", "2" };
            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.selectData(It.IsAny<string>())).Returns(testList);

            ImageController testController = new ImageController(mock.Object);
            int actual = testController.GetAllID().Count;

            Assert.AreEqual(testList.Count, actual);
        }

        [Test]
        public void CanCallGetOffersIDs()
        {
            string[] photoData = { "Test message for offer", "1", "1", "2" };
            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.selectData(It.IsAny<string>())).Returns(testList);

            ImageController testController = new ImageController(mock.Object);
            int actual = testController.GetOffersIDs().Count;

            Assert.AreEqual(testList.Count, actual);
        }

        [Test]
        public void CanCallGetMaxID()
        {
            string[] photoData = { "Name", "true", "C:/somepath", "5.5", "true", "some text", "1", "1", "2" };
            string[] photoDataMax = { "Name", "true", "C:/somepath", "5.5", "true", "some text", "3", "1", "2" };
            string[] photoData1 = { "Name", "true", "C:/somepath", "5.5", "true", "some text", "2", "1", "2" };

            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);
            testList.Add(photoDataMax);
            testList.Add(photoData1);

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.selectData(It.IsAny<string>())).Returns(testList);

            ImageController testController = new ImageController(mock.Object);
            int actual = testController.GetMaxID();

            Assert.AreEqual(3, actual);
        }

        [Test]
        public void CanCallEditRating()
        {
            string sqlget = "SELECT * FROM Paveikslelis WHERE id_Paveikslelis = " + 1;
            string sqlup = "UPDATE Paveikslelis SET Ivertinimas = '" + 2 + "' WHERE id_Paveikslelis = " + 1;
            string[] photoData = { "Name", "true", "C:/somepath", "1", "true", "some text", "1", "1", "2" };

            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.selectData(It.IsAny<string>())).Returns(testList);
            mock.Setup(x => x.editData(It.IsAny<string>()));


            ImageController testController = new ImageController(mock.Object);
            testController.EditRating(1);

            string actual = mock.Invocations[1].Arguments[0].ToString();


            Assert.AreEqual(sqlup, actual);
        }
    }
}