using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using vytkun.ViewModels;
using MySql.Data.MySqlClient;

namespace vytkun.Repos
{
    public class ProjektasRepository
    {
        public List<ProjektasListViewModel> getProjects()
        {
            List<ProjektasListViewModel> projektai = new List<ProjektasListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.*, b.ID, CONCAT(b.Vardas, ' ', b.Pavarde) as Uzsakovas FROM projektai a LEFT JOIN uzsakovai b ON b.ID = a.fk_Uzsakovas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();

            foreach(DataRow item in dataTable.Rows)
            {
                projektai.Add(new ProjektasListViewModel
                {
                    id = Convert.ToInt32(item["Nr"]),
                    pavadinimas = Convert.ToString(item["Pavadinimas"]),
                    nuo_kada = Convert.ToDateTime(item["Nuo_kada"]),
                    iki_kada = Convert.ToDateTime(item["Iki_kada"]),
                    uzsakovas = Convert.ToString(item["Uzsakovas"])
                });
            }

            return projektai;
        }

        public ProjektasEditViewModel getProject(int nr)
        {
            ProjektasEditViewModel projektas = new ProjektasEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM projektai WHERE Nr=" + nr;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                projektas.id = Convert.ToInt32(item["Nr"]);
                projektas.pavadinimas = Convert.ToString(item["Pavadinimas"]);
                projektas.nuo_kada = Convert.ToDateTime(item["Nuo_kada"]);
                projektas.iki_kada = Convert.ToDateTime(item["Iki_kada"]);
                projektas.fk_uzsakovas = Convert.ToInt32(item["fk_Uzsakovas"]);
            }

            return projektas;
        }

        public bool updateProject(ProjektasEditViewModel project)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `projektai` SET
                                    `Pavadinimas` = ?pav,
                                    `Nuo_kada` = ?nuo,
                                    `Iki_kada` = ?iki,
                                    `fk_Uzsakovas` = ?uzsak
                                    WHERE Nr=" + project.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.String).Value = project.pavadinimas;
            mySqlCommand.Parameters.Add("?nuo", MySqlDbType.DateTime).Value = project.nuo_kada.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?iki", MySqlDbType.DateTime).Value = project.iki_kada.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?uzsak", MySqlDbType.Int32).Value = project.fk_uzsakovas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addProject(ProjektasEditViewModel project)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `projektai` (
                                    `Pavadinimas`,
                                    `Nuo_kada`,
                                    `Iki_kada`,
                                    `fk_Uzsakovas`)
                                    VALUES(
                                     ?pav,
                                     ?nuo,
                                     ?iki,
                                     ?uzsak)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.String).Value = project.pavadinimas;
            mySqlCommand.Parameters.Add("?nuo", MySqlDbType.DateTime).Value = project.nuo_kada.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?iki", MySqlDbType.DateTime).Value = project.iki_kada.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?uzsak", MySqlDbType.Int32).Value = project.fk_uzsakovas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public int getProjektasDalyviaiCount(int id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(fk_Projektas) as kiekis from dalyvauja where fk_Projektas=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                naudota = Convert.ToInt32(item["kiekis"] == DBNull.Value ? 0 : item["kiekis"]);
            }
            return naudota;
        }

        public int getProjektasGrafikaiCount(int id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(fk_Projektas2) as kiekis from grafikai where fk_Projektas2=" + id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                naudota = Convert.ToInt32(item["kiekis"] == DBNull.Value ? 0 : item["kiekis"]);
            }
            return naudota;
        }

        public void deleteProject(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM projektai where Nr=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}