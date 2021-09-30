using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Projektas_Irankiai.Controllers
{
    public class TradingController : Controller
    {
        IDataBase dataBase;
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;
        private string port;
        private string connectionString;
        private string sslM;

        public TradingController(IDataBase dataBase)
        {
            server = "46.17.175.64";
            database = "u145613208_meme";
            user = "u145613208_user";
            password = "kebabas123";
            port = "3306";
            sslM = "none";

            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);

            connection = new MySqlConnection(connectionString);

            this.dataBase = dataBase;
        }
        public ActionResult TradeOffers()
        {
            ViewBag.Message = "Mainų pasiūlymai";
            return View(GetTradeOffers());
        }

        public ActionResult NewTradeOffer()
        {
            ViewBag.Message = "Mainų pasiūlymo kūrimo puslapis.";

            List<Models.User> users = GetUsers();
            List<Models.TradableImage> timages = GetInvImages();

            Models.NewTradeOffer newTradeOffer = new Models.NewTradeOffer();
            newTradeOffer.SelectedImageId = 0;
            newTradeOffer.SelectedUserId = 0;
            newTradeOffer.text = "";
            List<SelectListItem> SLusers = new List<SelectListItem>();
            List<SelectListItem> SLimages = new List<SelectListItem>();
            foreach (Models.User user1 in users)
            {
                if(user1.id == 1)
                {
                    continue;
                }
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Value = user1.id.ToString();
                selectListItem.Text = user1.name;
                SLusers.Add(selectListItem);
            }
            newTradeOffer.users = SLusers;

            foreach (Models.TradableImage user1 in timages)
            {
                if(user1.userid == 1)
                {
                    SelectListItem selectListItem = new SelectListItem();
                    selectListItem.Value = user1.id.ToString();
                    selectListItem.Text = user1.name;
                    SLimages.Add(selectListItem);
                }
            }
            newTradeOffer.images = SLimages;

            return View(newTradeOffer);
        }

        [HttpPost]
        public ActionResult NewTradeOffer(Models.NewTradeOffer newTradeOffer)
        {
            NewTradeOfferToDB(newTradeOffer);

            return RedirectToAction("TradeOffers");
        }

        public void NewTradeOfferToDB(Models.NewTradeOffer newTradeOffer)
        {
            string zinute = newTradeOffer.text;
            int id = GetOffersIDs().Max() + 1;
            int userid = newTradeOffer.SelectedUserId;
            int imageid = newTradeOffer.SelectedImageId;
            int thisuyserid = 1;

            string sql = string.Format("INSERT INTO Mainu_Uzklausa(Zinute, id_Mainu_uzklausa, fk_Naudotojasid_Naudotojas, fk_Naudotojasid_Naudotojas1, fk_Mainomas_Paveikslelis) VALUES({0},{1},{2},{3},{4})", string.Format("\"{0}\"", zinute), id, thisuyserid, userid, imageid);

            dataBase.insertData(sql);
        }

        public List<int> GetOffersIDs()
        {
            List<Models.TradeOffer> images = GetAllTradeOffers();
            List<int> indexes = new List<int>();
            for (int i = 0; i < images.Count; i++)
            {
                indexes.Add(images.ElementAt(i).id);
            }

            return indexes;
        }

        public List<Models.TradeOffer> GetAllTradeOffers()
        {
            List<Models.TradeOffer> offers = new List<Models.TradeOffer>();
            List<string[]> sqlOffers = dataBase.selectData("SELECT * FROM Mainu_Uzklausa");

            foreach (string[] sOffer in sqlOffers)
            {
                Models.TradeOffer offer = new Models.TradeOffer();
                offer.message = sOffer[0];
                offer.id = int.Parse(sOffer[1]);
                offer.user1 = int.Parse(sOffer[2]);
                offer.user2 = int.Parse(sOffer[3]);
                offers.Add(offer);
            }

            return offers;
        }

        public List<Models.TradeOffer> GetTradeOffers()
        {
            List<Models.TradeOffer> offers = new List<Models.TradeOffer>();
            List<string[]> sqlOffers = dataBase.selectData("SELECT * FROM Mainu_Uzklausa WHERE fk_Naudotojasid_Naudotojas = 1 OR fk_Naudotojasid_Naudotojas1 = 1");

            foreach (string[] sOffer in sqlOffers)
            {
                Models.TradeOffer offer = new Models.TradeOffer();
                offer.message = sOffer[0];
                offer.id = int.Parse(sOffer[1]);
                offer.user1 = int.Parse(sOffer[2]);
                offer.user2 = int.Parse(sOffer[3]);
                offers.Add(offer);
            }

            return offers;
        }

        public List<Models.User> GetUsers()
        {
            List<Models.User> users = new List<Models.User>();
            List<string[]> sqlOffers = dataBase.selectData("SELECT * FROM Naudotojas");

            foreach (string[] sOffer in sqlOffers)
            {
                Models.User user = new Models.User();
                user.name = sOffer[0];
                user.email = sOffer[1];
                user.password = sOffer[2];
                user.lastname = sOffer[3];
                user.role = int.Parse(sOffer[4]);
                user.id = int.Parse(sOffer[5]);
                users.Add(user);
            }

            return users;
        }

        public List<Models.TradableImage> GetInvImages()
        {
            List<Models.TradableImage> images = new List<Models.TradableImage>();
            List<string[]> sqlImages = dataBase.selectData("SELECT * FROM Mainomas_Paveikslelis WHERE fk_Naudotojasid_Naudotojas = 1");

            foreach (string[] sImage in sqlImages)
            {
                Models.TradableImage image = new Models.TradableImage();
                image.name = sImage[0];
                image.path = sImage[1];
                image.id = int.Parse(sImage[2]);
                image.userid = int.Parse(sImage[3]);
                images.Add(image);
            }

            return images;
        }
    }
}