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
    public class UzduotisRepository
    {
        public List<UzduotisListViewModel> getUzduotis()
        {
            List<UzduotisListViewModel> uzduotis = new List<UzduotisListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.*, b.Nr, b.Nuo_kada as nuo, b.Iki_kada as iki FROM uzduotys a LEFT JOIN grafikai b ON b.Nr = a.fk_Grafikas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();

            foreach (DataRow item in dataTable.Rows)
            {
                uzduotis.Add(new UzduotisListViewModel
                {
                    id = Convert.ToInt32(item["Nr"]),
                    pavadinimas = Convert.ToString(item["Pavadinimas"]),
                    nuo_kada = Convert.ToDateTime(item["nuo"]),
                    iki_kada = Convert.ToDateTime(item["iki"])
                });
            }

            return uzduotis;
        }

        public UzduotisEditViewModel getUzduotis(int nr)
        {
            UzduotisEditViewModel uzduotis = new UzduotisEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM uzduotys WHERE Nr=" + nr;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                uzduotis.id = Convert.ToInt32(item["Nr"]);
                uzduotis.pavadinimas = Convert.ToString(item["Pavadinimas"]);
                uzduotis.fk_grafikas = Convert.ToInt32(item["fk_Grafikas"]);
            }

            return uzduotis;
        }

        public bool updateUzduotis(UzduotisEditViewModel uzduotis)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `uzduotys` SET
                                    `Pavadinimas` = ?pav,
                                    `fk_Grafikas` = ?graf
                                    WHERE Nr=" + uzduotis.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.String).Value = uzduotis.pavadinimas;
            mySqlCommand.Parameters.Add("?graf", MySqlDbType.Int32).Value = uzduotis.fk_grafikas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addUzduotis(UzduotisEditViewModel uzduotis)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `uzduotys` (
                                    `Pavadinimas`,
                                    `fk_Grafikas`)
                                    VALUES(
                                     ?pav,
                                     ?graf)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.String).Value = uzduotis.pavadinimas;
            mySqlCommand.Parameters.Add("?graf", MySqlDbType.Int32).Value = uzduotis.fk_grafikas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public void deleteUzduotis(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM uzduotys where Nr=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}