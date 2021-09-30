using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Projektas_Irankiai.Controllers
{
    public class Template
    {
        int id;
        List<int> tags;
        int weight;
        public Template(int id)
        {
            this.id = id;
            tags = new List<int>();
            weight = 0;
        }

        public void addTag(int tag)
        {
            tags.Add(tag);
        }

        public void addWeight(int w)
        {
            weight += w;
        }

        public List<int> getTags() { return tags; }

        public int getId() { return id; }
        public int getWeight() { return weight; }

        public override string ToString()
        {
            return "id: " + id + " weight: " + weight;
        }
    }

    public class Text
    {
        int id;
        List<int> tags;
        int weight;
        public Text(int id)
        {
            this.id = id;
            tags = new List<int>();
            weight = 0;
        }

        public void addTag(int tag)
        {
            tags.Add(tag);
        }

        public void addWeight(int w)
        {
            weight += w;
        }

        public List<int> getTags() { return tags; }

        public int getId() { return id; }

        public int getWeight() { return weight; }

        public override string ToString()
        {
            return "id: " + id + " weight: " + weight;
        }
    }

    class StartupJob : CronNET.BaseJob
    {
        public override CronNET.CronExpression Cron
        {
            get { return CronNET.CronExpression.Startup; }
        }
        public override void Execute()
        {

            ImageGenerationController troller = new ImageGenerationController();

            var task1 = Task.Factory.StartNew(() => { troller.bestRatedTags = troller.selectBestRatedTags(ImageGenerationController.db); });
            var task2 = Task.Factory.StartNew(() => { troller.mostPopularTags = troller.selectMostPopularTags(ImageGenerationController.db); });
            var task3 = Task.Factory.StartNew(() => { troller.randomTags = troller.selectRandomTags(ImageGenerationController.db); });
            var task4 = Task.Factory.StartNew(() => { troller.userSelectedTags = troller.selectUserSelectedTags(ImageGenerationController.db); });

            Task.WaitAll(task1, task2, task3, task4);

            troller.getTemplatesBySelectedTags();

            troller.getTextBySelectedTags();

            troller.generateImage();

        }
    }
    public class ImageGenerationController : Controller
    {
        public static DataBase db = new DataBase();

        public List<int> bestRatedTags;
        public List<int> mostPopularTags;
        public List<int> userSelectedTags;
        public List<int> randomTags;

        public List<int> templatesByUserTags;
        public List<int> templatesByPopularTags;
        public List<Template> templates;

        public List<int> textByUserTags;
        public List<int> textByPopularTags;
        public List<Text> text;

        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;
        private string port;
        private string connectionString;
        private string sslM;

        public ImageGenerationController()
        {
            server = "46.17.175.64";
            database = "u145613208_meme";
            user = "u145613208_user";
            password = "kebabas123";
            port = "3306";
            sslM = "none";

            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);

            connection = new MySqlConnection(connectionString);

            Debug.Print("Tag controller created");

        }

        // GET: Tag
        public ActionResult Index()
        {
            return View();
        }

        public List<int> selectMostPopularTags(DataBase db)
        {
            List<int> mostPopularTags;

            mostPopularTags = db.selectInts("SELECT fk_Zymeid_Zyme, COUNT(fk_Zymeid_Zyme) AS zymeCount FROM Paveikslelis_Zyme " +
                    "GROUP BY fk_Zymeid_Zyme ORDER BY COUNT(fk_Zymeid_Zyme) DESC LIMIT 3");

            return mostPopularTags;
        }

        public List<int> selectBestRatedTags(DataBase db)
        {
            List<int> mostPopularImages = new List<int>();
            bestRatedTags = new List<int>();
            List<int> bestRatedTags3 = new List<int>();
            Random rnd = new Random();


            MySqlConnection conn = new MySqlConnection(connectionString);

            mostPopularImages = db.selectInts("SELECT id_Paveikslelis FROM Paveikslelis ORDER BY Ivertinimas DESC LIMIT 3");

            //-----------------------------------------

            try
            {
                string sql = "";
                if (mostPopularImages.Count() >= 3)
                {
                    sql = "SELECT * FROM Paveikslelis_Zyme WHERE fk_Paveikslelisid_Paveikslelis = " + mostPopularImages[0]
                    + " OR fk_Paveikslelisid_Paveikslelis = " + mostPopularImages[1] + " OR fk_Paveikslelisid_Paveikslelis = " + mostPopularImages[2];
                }
                else return null; //No tags


                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    bestRatedTags.Add(int.Parse(reader[0].ToString()));
                }
                reader.Close();
                Debug.Print(bestRatedTags.Count.ToString());

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            conn.Close();

            if (bestRatedTags.Count() < 3)
            {
                return null;
            }
            else
            {
                while (bestRatedTags3.Count < 3)
                {
                    Debug.Print("Best rated while");
                    int index = rnd.Next(0, bestRatedTags.Count());
                    if (!bestRatedTags3.Contains(bestRatedTags[index]))
                    {
                        bestRatedTags3.Add(bestRatedTags[index]);
                    }
                }
            }

            return bestRatedTags3;
        }

        public List<int> selectUserSelectedTags(DataBase db)
        {
            List<int> userSelectedTags = new List<int>();

            userSelectedTags = db.selectInts("SELECT fk_Zymeid_Zyme, COUNT(fk_Zymeid_Zyme) AS zymeCount FROM Pasirenka " +
                    "GROUP BY fk_Zymeid_Zyme ORDER BY COUNT(fk_Zymeid_Zyme) DESC LIMIT 3");


            return userSelectedTags;
        }

        public List<int> selectRandomTags(DataBase db)
        {

            List<int> alltags = new List<int>();
            List<int> randomTags = new List<int>();
            Random rnd = new Random();

            alltags = db.selectInts("SELECT `id_Zyme` FROM `Zyme`");

            while (randomTags.Count() < 3)
            {
                int index = rnd.Next(0, alltags.Count());
                if (!randomTags.Contains(alltags[index]))
                {
                    randomTags.Add(alltags[index]);
                }
            }

            return randomTags;
        }


        public List<int> getTemplatesByUserTags(DataBase db)
        {
            List<int> templatesByUserTags;
            templatesByUserTags = db.GetTemplatesByXTags("SELECT * FROM `Paveikslelis_Zyme` WHERE `fk_Paveikslelisid_Paveikslelis` IN (SELECT `id_Paveikslelis` FROM `Paveikslelis` WHERE `ArSabolnas` = 1)", userSelectedTags);
            return templatesByUserTags;
        }

        public List<int> getTemplatesByPopularTags(DataBase db)
        {
            templatesByPopularTags = new List<int>();
            templatesByPopularTags = db.GetTemplatesByXTags("SELECT * FROM `Paveikslelis_Zyme` WHERE `fk_Paveikslelisid_Paveikslelis` IN (SELECT `id_Paveikslelis` FROM `Paveikslelis` WHERE `ArSabolnas` = 1)", mostPopularTags);
            return templatesByPopularTags;
        }

        public List<Template> concatTemplateLists(List<int> templatesByPopularTags, List<int> templatesByUserTags, DataBase db)
        {
            List<int> ts = new List<int>();             //Nesikartojantys template id is atrinktu tagu
            foreach (int t in templatesByPopularTags)
            {
                ts.Add((t));
            }

            foreach (int t in templatesByUserTags)
            {
                if (!templatesByPopularTags.Contains(t))
                {
                    ts.Add((t));
                }
            }

            templates = new List<Template>();
            
            foreach (int t in ts)
            {
                templates.Add(new Template(t));
            }

            db.AddTagsToTemplates(templates);

            return templates;

        }
        public List<Text> addWeightPopT(List<Text> text, List<int> mostPopularTags)
        {
            List<int> tags;
            for (int i = 0; i < text.Count; i++)
            {
                tags = text[i].getTags();

                for (int j = 0; j < tags.Count; j++)
                {
                    if (mostPopularTags.Contains(tags[j]))
                    {
                        text[i].addWeight(2);
                    }
                }
            }

            return text;
        }


        public List<Template> addWeightPop(List<Template> templates, List<int> mostPopularTags)
        {
            List<int> tags;
            for (int i = 0; i < templates.Count; i++)
            {
                tags = templates[i].getTags();

                for (int j = 0; j < tags.Count; j++)
                {

                    if (mostPopularTags.Contains(tags[j]))
                    {
                        templates[i].addWeight(2);
                    }
                }
            }

            return templates;
        }

        public List<Text> addWeightUserT(List<Text> text, List<int> userSelectedTags)
        {
            List<int> tags;
            for (int i = 0; i < text.Count; i++)
            {
                tags = text[i].getTags();

                for (int j = 0; j < tags.Count; j++)
                {
                    if (userSelectedTags.Contains(tags[j]))
                    {
                        text[i].addWeight(1);
                    }
                }
            }

            return text;
        }

        public List<Template> addWeightUser(List<Template> templates, List<int> userSelectedTags)
        {
            List<int> tags;
            for (int i = 0; i < templates.Count; i++)
            {
                tags = templates[i].getTags();

                for (int j = 0; j < tags.Count; j++)
                {
                    if (userSelectedTags.Contains(tags[j]))
                    {
                        templates[i].addWeight(1);
                    }
                }
            }

            return templates;
        }

        public void getTemplatesBySelectedTags()
        {

            templatesByUserTags = getTemplatesByUserTags(db);
            getTemplatesByPopularTags(db);
            templates = concatTemplateLists(templatesByPopularTags, templatesByUserTags, db);
            addWeightPop(templates, mostPopularTags);
            addWeightUser(templates, userSelectedTags);

        }

        public void getTextBySelectedTags()
        {
            getTextByUserTags(db);
            getTextByPopularTags(db);
            concatTextLists(templatesByPopularTags, templatesByUserTags, db);
            addWeightPopT(text, mostPopularTags);
            addWeightUserT(text, userSelectedTags);
        }

        public List<int> getTextByUserTags(DataBase db)
        {
            textByUserTags = new List<int>();
            textByUserTags = db.GetTemplatesByXTags("SELECT * FROM `Paveikslelis_Zyme` WHERE `fk_Paveikslelisid_Paveikslelis` IN (SELECT `id_Paveikslelis` FROM `Paveikslelis` WHERE `ArSabolnas` = 0)", userSelectedTags);
            return textByUserTags;
        }

        public List<int> getTextByPopularTags(DataBase db)
        {
            textByPopularTags = new List<int>();
            textByPopularTags = db.GetTemplatesByXTags("SELECT * FROM `Paveikslelis_Zyme` WHERE `fk_Paveikslelisid_Paveikslelis` IN (SELECT `id_Paveikslelis` FROM `Paveikslelis` WHERE `ArSabolnas` = 0)", textByPopularTags);
            return textByPopularTags;
        }

        public List<Text> concatTextLists(List<int> textByPopularTags, List<int> textByUserTags, DataBase db)
        {
            text = new List<Text>();
            foreach (var t in textByPopularTags)
            {
                text.Add(new Text(t));
            }

            foreach (var t in textByUserTags)
            {
                if (!textByPopularTags.Contains(t))
                    text.Add(new Text(t));
            }

            db.GetTextTags(text);

            return text;
        }

        public void generateImage()
        {
            List<Template> temp = new List<Template>();
            List<Text> txt = new List<Text>();
            Random rnd = new Random();

            int maxTWeight = 0;
            foreach (var t in text)
            {
                if (t.getWeight() > maxTWeight)
                    maxTWeight = t.getWeight();
            }

            int maxTemplateWeight = 0;
            foreach (var t in templates)
            {
                if (t.getWeight() > maxTemplateWeight)
                    maxTemplateWeight = t.getWeight();
            }

            foreach (var t in text)
            {
                if (t.getWeight() == maxTWeight)
                {
                    txt.Add(t);
                }
            }

            foreach (var t in templates)
            {
                if (t.getWeight() == maxTemplateWeight)
                {
                    temp.Add(t);
                }
            }

            Template templateToUse;
            Text textToUse;

            textToUse = txt[rnd.Next(0, txt.Count())];
            templateToUse = temp[rnd.Next(0, temp.Count)];


            int imgId = templateToUse.getId();
            int imgWithTextId = textToUse.getId();
            string imgLocation = "", imgText = "";

            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT `Saugojimo_vieta` FROM `Paveikslelis` WHERE `id_Paveikslelis` = " + imgId; //Gaunami sablonai ir ju zymes
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    imgLocation = reader[0].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            conn.Close();


            try
            {
                conn.Open();

                string sql = "SELECT `Tekstas` FROM `Paveikslelis` WHERE `id_Paveikslelis` = " + imgWithTextId; //Gaunami sablonai ir ju zymes
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    imgText = reader[0].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            conn.Close();


            Debug.Print(System.AppDomain.CurrentDomain.BaseDirectory + "ImagesUploaded\\Templates\\" + imgLocation);

            Image image = Image.FromFile(System.AppDomain.CurrentDomain.BaseDirectory + "ImagesUploaded\\Templates\\" + imgLocation);
            Image newImage = writeOnImage(image, imgText);

            newImage.Save(System.AppDomain.CurrentDomain.BaseDirectory + "ImagesUploaded\\" + "Generated" + imgText + DateTime.Now.ToString("yymmssfff") + imgLocation);


            try
            {
                conn.Open();

                int maxID = getMaxID() + 1;

                string sql = "INSERT INTO `Paveikslelis`VALUES (" +
                    "'" + imgText + "'," +
                    "0," +
                    "" + "'Generated" + imgText + DateTime.Now.ToString("yymmssfff") + imgLocation + "'," +
                    "0," +
                    "0," +
                    "'" + imgText + "'," +
                    "" + maxID + "," +
                    "1," +
                    "1)";

                Debug.Print(sql);

                MySqlCommand command = new MySqlCommand(sql, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            conn.Close();


        }

        private int getMaxID()
        {
            int maxId = 0;

            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT MAX(`id_Paveikslelis`) FROM `Paveikslelis`";
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    maxId = int.Parse(reader[0].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            conn.Close();

            return maxId;
        }

        private Image writeOnImage(Image template, string text)
        {
            var font = new Font("TimesNewRoman", 25, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(template);
            graphics.DrawString(text, font, Brushes.White, (float)template.Width / 2 - graphics.MeasureString(text, font).Width / 2, (float)template.Height - graphics.MeasureString(text, font).Height);
            return template;
        }
    }
}