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
    using System.Linq;

    [TestFixture]
    public class TradingControllerTests
    {
        private IDataBase dataBase;

        [Test]
        public void CanConstruct()
        {
            var instance = new TradingController(dataBase);
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanCallNewTradeOfferToDB()
        {
            string[] photoData = { "Test message for offer", "1", "1", "2" };
            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.selectData(It.IsAny<string>())).Returns(testList);
            mock.Setup(x => x.insertData(It.IsAny<string>()));

            TradingController testController = new TradingController(mock.Object);

            string zinute = "Test text";
            int id = testController.GetOffersIDs().Max() + 1;
            int userid = 2;
            int imageid = 1;
            int thisuyserid = 1;

            string expected = string.Format("INSERT INTO Mainu_Uzklausa(Zinute, id_Mainu_uzklausa, fk_Naudotojasid_Naudotojas, fk_Naudotojasid_Naudotojas1, fk_Mainomas_Paveikslelis) VALUES({0},{1},{2},{3},{4})", string.Format("\"{0}\"", zinute), id, thisuyserid, userid, imageid);
            
            Models.NewTradeOffer newTradeOffer = new NewTradeOffer();
            newTradeOffer.text = zinute;
            newTradeOffer.SelectedUserId = userid;
            newTradeOffer.SelectedImageId = imageid;

            testController.NewTradeOfferToDB(newTradeOffer);

            string actual = mock.Invocations[2].Arguments[0].ToString();

            Assert.AreEqual(expected, actual);
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

            TradingController testController = new TradingController(mock.Object);
            int actual = testController.GetOffersIDs().Count;

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

            TradingController testController = new TradingController(mock.Object);
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

            TradingController testController = new TradingController(mock.Object);
            int actual = testController.GetTradeOffers().Count;

            Assert.AreEqual(testList.Count, actual);
        }

        [Test]
        public void CanCallGetUsers()
        {
            string[] photoData = { "Name", "name@email.com", "password", "lastname", "1", "1" };
            List<string[]> testList = new List<string[]>();
            testList.Add(photoData);

            Mock<IDataBase> mock = new Mock<IDataBase>();
            //mock.CallBase = true;
            mock.Setup(x => x.selectData(It.IsAny<string>())).Returns(testList);

            TradingController testController = new TradingController(mock.Object);
            int actual = testController.GetUsers().Count;

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

            TradingController testController = new TradingController(mock.Object);
            int actual = testController.GetInvImages().Count;

            Assert.AreEqual(testList.Count, actual);
        }
    }
}
