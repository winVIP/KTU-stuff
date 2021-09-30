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
    public class BiudzetasRepository
    {
        public List<BiudzetasListViewModel> getBudgets()
        {
            List<BiudzetasListViewModel> biudzetai = new List<BiudzetasListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.*, b.fk_Uzsakovas2, b.fk_Biudzetas, c.ID, d.fk_Uzsakovas, d.Pavadinimas as Projektas FROM biudzetai a 
                                LEFT JOIN lesos b ON b.fk_Biudzetas = a.id_Biudzetas 
                                LEFT JOIN uzsakovai c ON c.ID = b.fk_Uzsakovas2 
                                LEFT JOIN projektai d ON d.fk_Uzsakovas = c.ID";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();

            foreach (DataRow item in dataTable.Rows)
            {
                biudzetai.Add(new BiudzetasListViewModel
                {
                    id = Convert.ToInt32(item["id_Biudzetas"]),
                    esamas_biudzetas = Convert.ToDouble(item["Esamas_biudzetas"]),
                    projektas = Convert.ToString(item["Projektas"])
                });
            }

            return biudzetai;
        }

        public BiudzetasEditViewModel getBudget(int nr)
        {
            BiudzetasEditViewModel biudzetas = new BiudzetasEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM biudzetai WHERE id_Biudzetas=" + nr;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                biudzetas.id = Convert.ToInt32(item["id_Biudzetas"]);
                biudzetas.esamas_biudzetas = Convert.ToDouble(item["Esamas_biudzetas"]);
            }

            return biudzetas;
        }

        public bool updateBudget(BiudzetasEditViewModel budget)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `biudzetai` SET
                                    `Esamas_biudzetas` = ?biudzetas,
                                    WHERE id_Biudzetas=" + budget.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?biudzetas", MySqlDbType.Double).Value = budget.esamas_biudzetas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addBudget(BiudzetasEditViewModel budget)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `biudzetai` (
                                    `Esamas_biudzetas`)
                                    VALUES(
                                     ?biudzetas)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?biudzetas", MySqlDbType.Double).Value = budget.esamas_biudzetas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public int getBiudzetasIslaidosCount(int id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(Nr) as kiekis from islaidos where fk_Biudzetas2=" + id;
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

        public int getBiudzetasLesosCount(int id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(Nr) as kiekis from lesos where fk_Biudzetas=" + id;
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

        public void deleteBudget(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM biudzetai where id_Biudzetas=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }

    
}