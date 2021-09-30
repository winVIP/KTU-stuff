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
using System.Data;

namespace Projektas_Irankiai.Controllers
{
    public class ImageController : Controller, IImageController
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

        public ImageController(IDataBase dataBase)
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

            DeleteImageFromDB(image);

            return RedirectToAction("UploadedImagesView");
        }

        public void DeleteImageFromDB(Models.Image image)
        {
            System.IO.File.Delete(image.path);
            string sql = "DELETE FROM Paveikslelis WHERE id_Paveikslelis = " + image.id;
            dataBase.deleteData(sql);
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
                EditImageInDB(image);

                return RedirectToAction("UploadedImagesView");
            }
        }

        public void EditImageInDB(Models.Image image)
        {
            string sql = "UPDATE Paveikslelis SET Pavadinimas = '" + image.name + "' WHERE id_Paveikslelis = " + image.id;
            dataBase.editData(sql);
        }

        [HttpGet]
        public ActionResult ImageUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImageUpload(Models.Image imageModel)
        {
            InsertImageToDB(imageModel);

            return View();
        }

        public void InsertImageToDB(Models.Image imageModel)
        {
            imageModel.imageFile.SaveAs(imageModel.path);
        }

        public void InsertImageToSQL(Models.Image imageModel)
        {
            imageModel.id = GetMaxID() + 1;
            imageModel.uploader = 1;
            imageModel.isMarked = false;
            imageModel.isTemplate = false;
            imageModel.marker = 1;
            imageModel.text = imageModel.name;
            try
            {
                string sql = "INSERT INTO Paveikslelis VALUES " + imageModel.toSqlValues();
                dataBase.insertData(sql);

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            try
            {
                string sql = "INSERT INTO `Paveikslelis_Zyme`(`fk_Zymeid_Zyme`, `fk_Paveikslelisid_Paveikslelis`) VALUES (" + imageModel.tag + "," + imageModel.id + ")";
                dataBase.insertData(sql);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        public ActionResult GeneratedImagesView()
        {
            ViewBag.Message = "Sugeneruotų paveikslėlių puslapis.";

            return View(GeneratedImagesFromDB());
        }

        public List<Models.Image> GeneratedImagesFromDB()
        {
            List<Models.Image> images = GetImages();
            List<Models.Image> generated = new List<Models.Image>();

            foreach (Models.Image img in images)
            {
                if (img.path.Contains("Generated"))
                    generated.Add(img);
            }

            return generated;
        }

        public ActionResult ImageCreationView()
        {
            ViewBag.Message = "Paveikslėlio kūrimo puslapis.";

            List<Models.Image> templates = GetTemplates();

            Models.selectImageAndText selectImageAndText = new Models.selectImageAndText();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (Models.Image image1 in templates)
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

            InsertImageToDB(selectedTemp);

            selectedTemp.id = GetMaxID() + 1;
            selectedTemp.name = "Created Image from: " + selectedTemp.name;
            selectedTemp.rating = 0;
            selectedTemp.isTemplate = false;
            selectedTemp.text = selectImageAndText.text;
            selectedTemp.isMarked = false;
            selectedTemp.path = @"\\Created\\Created" + Path.GetFileName(selectedTemp.path);

            InsertImageToSQL(selectedTemp);

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
            List<string[]> sqlImages = dataBase.selectData("SELECT * FROM Paveikslelis");

            foreach (string[] sImage in sqlImages)
            {
                Models.Image image = new Models.Image();
                image.name = sImage[0];
                image.isMarked = bool.Parse(sImage[1]);
                image.path = sImage[2];
                image.rating = double.Parse(sImage[3]);
                image.isTemplate = bool.Parse(sImage[4]);
                image.text = sImage[5];
                image.id = int.Parse(sImage[6]);
                image.uploader = int.Parse(sImage[7]);
                image.marker = int.Parse(sImage[8]);
                images.Add(image);
            }
            return images;
        }        

        public List<Models.Image> GetRecommendedImages()
        {
            List<Models.Image> images = new List<Models.Image>();
            List<string[]> sqlImages = dataBase.selectData("SELECT * FROM `Paveikslelis` ORDER BY Ivertinimas DESC");

            foreach (string[] sImage in sqlImages)
            {
                Models.Image image = new Models.Image();
                image.name = sImage[0];
                image.isMarked = bool.Parse(sImage[1]);
                image.path = sImage[2];
                image.rating = double.Parse(sImage[3]);
                image.isTemplate = bool.Parse(sImage[4]);
                image.text = sImage[5];
                image.id = int.Parse(sImage[6]);
                image.uploader = int.Parse(sImage[7]);
                image.marker = int.Parse(sImage[8]);
                images.Add(image);
            }
            return images;
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
            EditRating(id);

            // "return RedirectToAction("RecommendedList");" Pakeiciau, kad butu galima naudoti bet kur.
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        public void EditRating(int id)
        {
            string sqlget = "SELECT * FROM Paveikslelis WHERE id_Paveikslelis = " + id;

            string[] image = dataBase.selectData(sqlget).First();

            double rating = double.Parse(image[3].ToString());

            string sqlup = "UPDATE Paveikslelis SET Ivertinimas = '" + (rating + 1) + "' WHERE id_Paveikslelis = " + id;
            dataBase.editData(sqlup);
        }
    }
}