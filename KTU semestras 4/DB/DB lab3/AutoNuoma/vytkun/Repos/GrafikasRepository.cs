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
    public class GrafikasRepository
    {
        public List<GrafikasListViewModel> getGrafikai()
        {
            List<GrafikasListViewModel> grafikai = new List<GrafikasListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.*, b.Nr, b.Pavadinimas as projektas FROM grafikai a LEFT JOIN projektai b ON b.Nr = a.fk_Projektas2";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();

            foreach (DataRow item in dataTable.Rows)
            {
                grafikai.Add(new GrafikasListViewModel
                {
                    id = Convert.ToInt32(item["Nr"]),
                    pavadinimas = Convert.ToString(item["Pavadinimas"]),
                    nuo_kada = Convert.ToDateTime(item["Nuo_kada"]),
                    iki_kada = Convert.ToDateTime(item["Iki_kada"]),
                    projektas = Convert.ToString(item["projektas"])
                });
            }

            return grafikai;
        }

        public GrafikasEditViewModel getGrafikas(int nr)
        {
            GrafikasEditViewModel grafikas = new GrafikasEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM grafikai WHERE Nr=" + nr;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                grafikas.id = Convert.ToInt32(item["Nr"]);
                grafikas.pavadinimas = Convert.ToString(item["Pavadinimas"]);
                grafikas.nuo_kada = Convert.ToDateTime(item["Nuo_kada"]);
                grafikas.iki_kada = Convert.ToDateTime(item["Iki_kada"]);
                grafikas.fk_projektas = Convert.ToInt32(item["fk_Projektas2"]);
            }

            return grafikas;
        }

        public bool updateGrafikas(GrafikasEditViewModel grafikas)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `grafikai` SET
                                    `Pavadinimas` = ?pav,
                                    `Nuo_kada` = ?nuo,
                                    `Iki_kada` = ?iki,
                                    `fk_Projektas2` = ?proj
                                    WHERE Nr=" + grafikas.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.String).Value = grafikas.pavadinimas;
            mySqlCommand.Parameters.Add("?nuo", MySqlDbType.DateTime).Value = grafikas.nuo_kada.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?iki", MySqlDbType.DateTime).Value = grafikas.iki_kada.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?proj", MySqlDbType.Int32).Value = grafikas.fk_projektas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addGrafikas(GrafikasEditViewModel grafikas)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `grafikai` (
                                    `Pavadinimas`,
                                    `Nuo_kada`,
                                    `Iki_kada`,
                                    `fk_Projektas2`)
                                    VALUES(
                                     ?pav,
                                     ?nuo,
                                     ?iki,
                                     ?proj)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.String).Value = grafikas.pavadinimas;
            mySqlCommand.Parameters.Add("?nuo", MySqlDbType.DateTime).Value = grafikas.nuo_kada.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?iki", MySqlDbType.DateTime).Value = grafikas.iki_kada.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?proj", MySqlDbType.Int32).Value = grafikas.fk_projektas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public int getGrafikasUzduotysCount(int id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(Nr) as kiekis from uzduotys where fk_Grafikas=" + id;
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

        public void deleteGrafikas(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM grafikai where Nr=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            System.Diagnostics.Debug.WriteLine(id);
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}