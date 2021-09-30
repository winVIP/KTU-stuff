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
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;
        private string port;
        private string connectionString;
        private string sslM;

        public TradingController()
        {
            server = "46.17.175.64";
            database = "u145613208_meme";
            user = "u145613208_user";
            password = "kebabas123";
            port = "3306";
            sslM = "none";

            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);

            connection = new MySqlConnection(connectionString);
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
            string zinute = newTradeOffer.text;
            int id = GetOffersIDs().Max() + 1;
            int userid = newTradeOffer.SelectedUserId;
            int imageid = newTradeOffer.SelectedImageId;
            int thisuyserid = 1;

            try
            {
                connection.Open();
                string sql = String.Format("INSERT INTO Mainu_Uzklausa(Zinute, id_Mainu_uzklausa, fk_Naudotojasid_Naudotojas, fk_Naudotojasid_Naudotojas1, fk_Mainomas_Paveikslelis) VALUES({0},{1},{2},{3},{4})", String.Format("\"{0}\"", zinute) , id, thisuyserid, userid, imageid);
                Debug.Print(sql);
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return RedirectToAction("TradeOffers");
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
            try
            {
                connection.Open();
                string sql = "SELECT * FROM Mainu_Uzklausa";//Koks naudotojo id?
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Models.TradeOffer offer = new Models.TradeOffer();
                    offer.message = reader[0].ToString();
                    offer.id = int.Parse(reader[1].ToString());
                    offer.user1 = int.Parse(reader[2].ToString());
                    offer.user2 = int.Parse(reader[3].ToString());
                    offers.Add(offer);
                }
                reader.Close();
                Debug.Print("Connection Open ! ");
                connection.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Can not open connection ! ");
            }
            return offers;
        }

        public List<Models.TradeOffer> GetTradeOffers()
        {
            List<Models.TradeOffer> offers = new List<Models.TradeOffer>();
            try
            {
                List<Models.User> users = GetUsers();
                List<Models.TradableImage> timages = GetInvImages();
                connection.Open();
                string sql = "SELECT * FROM Mainu_Uzklausa WHERE fk_Naudotojasid_Naudotojas = 1 OR fk_Naudotojasid_Naudotojas1 = 1";//Koks naudotojo id?
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Models.TradeOffer offer = new Models.TradeOffer();
                    offer.message = reader[0].ToString();
                    offer.id = int.Parse(reader[1].ToString());
                    offer.user1 = int.Parse(reader[2].ToString());
                    offer.user1Name = users.Find(x => x.id == offer.user1).name;
                    offer.user2 = int.Parse(reader[3].ToString());
                    offer.user2Name = users.Find(x => x.id == offer.user2).name;
                    offer.imageId = int.Parse(reader[4].ToString());
                    offer.imageName = timages.Find(x => x.id == offer.imageId).name;
                    offers.Add(offer);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Can not open connection in GetTradeOffers");
            }
            return offers;
        }

        public List<Models.User> GetUsers()
        {
            List<Models.User> users = new List<Models.User>();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM Naudotojas";
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Models.User user = new Models.User();
                    user.name = reader[0].ToString();
                    user.email = reader[1].ToString();
                    user.password = reader[2].ToString();
                    user.lastname = reader[3].ToString();
                    user.role = int.Parse(reader[4].ToString());
                    user.id = int.Parse(reader[5].ToString());
                    users.Add(user);
                }
                reader.Close();
                Debug.Print("Connection Open ! ");
                connection.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Can not open connection in GetUsers");
            }
            return users;
        }

        public List<Models.TradableImage> GetInvImages()
        {
            List<Models.TradableImage> images = new List<Models.TradableImage>();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM Mainomas_Paveikslelis WHERE fk_Naudotojasid_Naudotojas = 1";//Koks naudotojo id?
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Models.TradableImage image = new Models.TradableImage();
                    image.name = reader[0].ToString();
                    image.path = reader[1].ToString();
                    image.id = int.Parse(reader[2].ToString());
                    image.userid = int.Parse(reader[3].ToString());
                    //image.tradeid = int.Parse(reader[4].ToString());
                    images.Add(image);
                }
                reader.Close();
                Debug.Print("Connection Open ! ");
                connection.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Can not open connection in GetInvImages");
            }
            return images;
        }
    }
}