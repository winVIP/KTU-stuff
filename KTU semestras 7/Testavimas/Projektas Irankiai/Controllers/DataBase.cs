using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Projektas_Irankiai.Controllers
{
    public class DataBase : IDataBase
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;
        private string port;
        private string connectionString;
        private string sslM;

        public DataBase()
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

        public virtual List<string[]> selectData(string query)
        {
            List<string[]> allImages = new List<string[]>();

            try
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string[] image = new string[9];
                    image[0] = reader[0].ToString();
                    image[1] = reader[1].ToString();
                    image[2] = reader[2].ToString();
                    image[3] = reader[3].ToString();
                    image[4] = reader[4].ToString();
                    image[5] = reader[5].ToString();
                    image[6] = reader[6].ToString();
                    image[7] = reader[7].ToString();
                    image[8] = reader[8].ToString();
                    allImages.Add(image);
                }
                reader.Close();
                Debug.WriteLine("Pavyko prisiconnectint");
                connection.Close();
            }
            catch
            {
                Debug.WriteLine("Nepavyko prisiconnectint");
            }
            

            return allImages;
        }

        public virtual List<int> selectInts(string query)
        {
            List<int> ints = new List<int>();

            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                conn.Open();

                string sql = query;
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ints.Add(int.Parse(reader[0].ToString()));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            conn.Close();

            return ints;
        }

        public virtual List<Template> AddTagsToTemplates(List<Template> templates)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT * FROM `Paveikslelis_Zyme` WHERE `fk_Paveikslelisid_Paveikslelis` IN (SELECT `id_Paveikslelis` FROM `Paveikslelis` WHERE `ArSabolnas` = 1)"; //Gaunami sablonai ir ju zymes
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < templates.Count; i++)
                    {
                        if (int.Parse(reader[1].ToString()).Equals(templates[i].getId()))
                        {
                            templates[i].addTag(int.Parse(reader[0].ToString()));
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            conn.Close();

            return templates;
        }

        public virtual List<int> GetTemplatesByXTags(string query, List<int> selectedTags)
        {
            List<int> tags = new List<int>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();

                string sql = query; //Gaunamos sablonu zymes
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (selectedTags.Contains(int.Parse(reader[0].ToString())))
                    {
                        tags.Add(int.Parse(reader[1].ToString()));
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            conn.Close();

            return tags;
        }

        public virtual List<Text> GetTextTags(List<Text> text)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();

                string sql = "SELECT * FROM `Paveikslelis_Zyme` WHERE `fk_Paveikslelisid_Paveikslelis` IN (SELECT `id_Paveikslelis` FROM `Paveikslelis` WHERE `ArSabolnas` = 0)"; //Gaunami sablonai ir ju zymes
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < text.Count; i++)
                    {
                        if (int.Parse(reader[1].ToString()).Equals(text[i].getId()))
                        {
                            text[i].addTag(int.Parse(reader[0].ToString()));
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            conn.Close();

            return text;
        }

        public virtual void deleteData(string query)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public virtual void editData(string query)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public virtual void insertData(string query)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}