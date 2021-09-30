using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Projektas_Irankiai.Controllers
{
    public class ImageController : Controller
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;
        private string port;
        private string connectionString;
        private string sslM;

        public ImageController()
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
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadedImagesView()
        {
            ViewBag.Message = "Paveikslėlių sąrašas.";
            return View(GetImages());
        }

        public ActionResult ImageEdit(int id)
        {
            ViewBag.Message = "Paveikslėlio redagavimo puslapis.";

            return View(GetImages().Where(x => x.id == id).First());
        }

        public ActionResult Inventory() 
        {
            ViewBag.Message = "Turimi paveikslėliai";
            return View(GetInvImages());
        }

        public ActionResult RecommendedList()
        {
            ViewBag.Message = "Rekomenduojami paveikslėliai";
            return View(GetRecommendedImages());
        }

        public ActionResult ImageDeletion(int id)
        {
            ViewBag.Message = "Pašalinti paveikslėlį?";
            Models.Image img = GetImages().Find(x => x.id == id);
            Debug.Print(img.path);
            return View(img);
        }

        [HttpPost]
        public ActionResult ImageDeletion(Models.Image image)
        {
            ViewBag.Message = "Pašalinti paveikslėlį?";
            string path = Server.MapPath("..\\..\\ImagesUploaded") + GetImages().Find(x => x.id == image.id).path;
            Debug.Print("ID: " + image.id);
            Debug.Print("Path from image: " + image.path);
            Debug.Print("Full path: " + path);
            System.IO.File.Delete(path);
            string sql = "DELETE FROM Paveikslelis WHERE id_Paveikslelis = " + image.id;
            connection.Open();
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();

            return RedirectToAction("UploadedImagesView");
        }

        [HttpPost]
        public ActionResult ImageEdit(Models.Image image)
        {
            ViewBag.Message = "Paveikslėlio redagavimo puslapis.";

            if (image.name.Contains("blogas"))
            {
                ModelState.AddModelError("name", "Nepriimtinas vardas");

                return View(image);
            }
            else
            {
                string sql = "UPDATE Paveikslelis SET Pavadinimas = '" + image.name + "' WHERE id_Paveikslelis = " + image.id;
                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction("UploadedImagesView");
            }
        }

        [HttpGet]
        public ActionResult ImageUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImageUpload(Projektas_Irankiai.Models.Image imageModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageModel.imageFile.FileName);
            string extension = Path.GetExtension(imageModel.imageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.path = "~/ImagesUploaded/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/ImagesUploaded/"), fileName);
            imageModel.path = MySqlHelper.EscapeString(fileName);

            imageModel.imageFile.SaveAs(fileName);

            imageModel.id = GetMaxID() + 1;
            imageModel.uploader = 1;
            imageModel.isMarked = false;
            imageModel.isTemplate = false;
            imageModel.marker = 1;
            try
            {
                connection.Open();
                string sql = "INSERT INTO Paveikslelis VALUES " + imageModel.toSqlValues();
                MySqlCommand command = new MySqlCommand(sql, connection);
                Debug.Print(sql);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return View();
        }

        public ActionResult GeneratedImagesView()
        {
            ViewBag.Message = "Sugeneruotų paveikslėlių puslapis.";
            List<Models.Image> images = GetImages();
            List<Models.Image> generated = new List<Models.Image>();

            foreach(Models.Image img in images)
            {
                if (img.path.Contains("Generated"))
                    generated.Add(img);
            }
            return View(generated);
        }

        public ActionResult ImageCreationView()
        {
            ViewBag.Message = "Paveikslėlio kūrimo puslapis.";

            List<Models.Image> templates = GetTemplates();

            Models.selectImageAndText selectImageAndText = new Models.selectImageAndText();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach(Models.Image image1 in templates)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Value = image1.id.ToString();
                selectListItem.Text = image1.name;
                selectListItems.Add(selectListItem);
                selectImageAndText.Items = selectListItems;
            }
           
            return View(selectImageAndText);
        }

        [HttpPost]
        public ActionResult ImageCreationView(Models.selectImageAndText selectImageAndText)
        {
            List<Models.Image> templates = GetTemplates();
            Models.Image selectedTemp = templates.Where(x => x.id == int.Parse(selectImageAndText.SelectedItemId)).First();

            Image template = Image.FromFile(Server.MapPath("..\\ImagesUploaded") + selectedTemp.path);
            Image newImage = writeOnImage(template, selectImageAndText.text);
            newImage.Save(Server.MapPath("..\\ImagesUploaded\\Created") + "\\Created" + Path.GetFileName(selectedTemp.path));

            selectedTemp.id = GetMaxID() + 1;
            selectedTemp.name = "Created Image from: " + selectedTemp.name;
            selectedTemp.rating = 0;
            selectedTemp.isTemplate = false;
            selectedTemp.text = selectImageAndText.text;
            selectedTemp.isMarked = false;
            selectedTemp.path = @"\\Created\\Created" + Path.GetFileName(selectedTemp.path);
            
            try
            {
                connection.Open();
                Debug.Print("Trying to insert!");

                Debug.Print("insert string: " + selectedTemp.toSqlValues());
                string sql = "INSERT INTO Paveikslelis VALUES " + selectedTemp.toSqlValues();
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();
                
                connection.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return RedirectToAction("UploadedImagesView");
        }

        private Image writeOnImage(Image template, string text)
        {
            var font = new Font("TimesNewRoman", 25, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(template);
            graphics.DrawString(text, font, Brushes.White, (float)template.Width / 2 - graphics.MeasureString(text, font).Width / 2, (float)template.Height - graphics.MeasureString(text, font).Height);
            return template;
        }

        public List<Models.Image> GetImages()
        {
            List<Models.Image> images = new List<Models.Image>();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM Paveikslelis";
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Models.Image image = new Models.Image();
                    image.name = reader[0].ToString();
                    image.isMarked = bool.Parse(reader[1].ToString());
                    image.path = reader[2].ToString();
                    image.rating = double.Parse(reader[3].ToString());
                    image.isTemplate = bool.Parse(reader[4].ToString());
                    image.text = reader[5].ToString();
                    image.id = int.Parse(reader[6].ToString());
                    image.uploader = int.Parse(reader[7].ToString());
                    image.marker = int.Parse(reader[8].ToString());
                    images.Add(image);
                }
                reader.Close();
                Debug.Print("Connection Open ! ");
                connection.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Can not open connection ! ");
            }
            return images;
        }

        public List<Models.Image> GetRecommendedImages()
        {
            List<Models.Image> images = new List<Models.Image>();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM `Paveikslelis` ORDER BY Ivertinimas DESC";
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Models.Image image = new Models.Image();
                    image.name = reader[0].ToString();
                    image.isMarked = bool.Parse(reader[1].ToString());
                    image.path = reader[2].ToString();
                    image.rating = double.Parse(reader[3].ToString());
                    image.isTemplate = bool.Parse(reader[4].ToString());
                    image.text = reader[5].ToString();
                    image.id = int.Parse(reader[6].ToString());
                    image.uploader = int.Parse(reader[7].ToString());
                    image.marker = int.Parse(reader[8].ToString());
                    images.Add(image);
                }
                reader.Close();
                Debug.Print("Connection Open ! ");
                connection.Close();
            }
            catch (Exception ex)
            {
                Debug.Print("Can not open connection ! ");
            }
            return images;
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
                Debug.Print("Can not open connection ! " + ex.Message);
            }
            return images;
        }

        public List<Models.TradeOffer> GetTradeOffers()
        {
            List<Models.TradeOffer> offers = new List<Models.TradeOffer>();
            try
            {
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

        public List<Models.Image> GetTemplates()
        {
            List<Models.Image> images = GetImages();
            List<Models.Image> templates = new List<Models.Image>();
            foreach (Models.Image image in images)
            {
                if (image.isTemplate == true)
                {
                    templates.Add(image);
                }
            }
            return templates;
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
                Debug.Print("Can not open connection ! ");
            }
            return users;
        }

        public List<int> GetAllID()
        {
            List<Models.Image> images = GetImages();
            List<int> indexes = new List<int>();
            for (int i = 0; i < images.Count; i++)
            {
                indexes.Add(images.ElementAt(i).id);
            }

            return indexes;
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

        public int GetMaxID()
        {
            return GetAllID().Max();
        }

        public ActionResult rateImage(int id)
        {
            string sqlget = "SELECT * FROM Paveikslelis WHERE id_Paveikslelis = " + id;
            
            connection.Open();
            MySqlCommand command = new MySqlCommand(sqlget, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            double rating = double.Parse(reader[3].ToString());
            connection.Close();
            connection.Open();
            string sql = "UPDATE Paveikslelis SET Ivertinimas = '" + (rating + 1) + "' WHERE id_Paveikslelis = " + id;
            command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();

            // "return RedirectToAction("RecommendedList");" Pakeiciau, kad butu galima naudoti bet kur.
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
    }
}