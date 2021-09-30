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
    public class LesosRepository
    {
        public List<LesosListViewModel> getLesos()
        {
            List<LesosListViewModel> lesos = new List<LesosListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.*, b.ID, b.Vardas as Vardas1, b.Pavarde as Pavarde1, c.ID, c.Vardas as Vardas2, c.Pavarde as Pavarde2 FROM lesos a LEFT JOIN uzsakovai b ON b.ID = a.fk_Uzsakovas2 LEFT JOIN remejai c ON c.ID = a.fk_Remejas";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();

            foreach (DataRow item in dataTable.Rows)
            {
                lesos.Add(new LesosListViewModel
                {
                    id = Convert.ToInt32(item["Nr"]),
                    suma = Convert.ToDouble(item["Suma"]),
                    data = Convert.ToDateTime(item["Data"]),
                    uzsakovas = Convert.ToString(item["Vardas1"] +" "+ item["Pavarde1"]),
                    remejas = Convert.ToString(item["Vardas2"] + " " + item["Pavarde2"]),
                });
            }

            return lesos;
        }

        public LesosEditViewModel getLesos(int nr)
        {
            LesosEditViewModel lesos = new LesosEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM lesos WHERE Nr=" + nr;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                lesos.id = Convert.ToInt32(item["Nr"]);
                lesos.suma = Convert.ToDouble(item["Suma"]);
                lesos.data = Convert.ToDateTime(item["Data"]);
                lesos.fk_biudzetas = Convert.ToInt32(item["fk_Biudzetas"]);
                lesos.fk_uzsakovas = Convert.ToInt32(item["fk_Uzsakovas2"]);
                lesos.fk_remejas = Convert.ToInt32(item["fk_Remejas"]);
            }

            return lesos;
        }

        public bool updateLesos(LesosEditViewModel lesos)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `lesos` SET
                                    `Suma` = ?sum,
                                    `Data` = ?data,
                                    `fk_Biudzetas` = ?biudz
                                    `fk_Uzsakovas2` = ?uzs
                                    `fk_Remejas` = ?rem
                                    WHERE Nr=" + lesos.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?sum", MySqlDbType.String).Value = lesos.suma;
            mySqlCommand.Parameters.Add("?data", MySqlDbType.DateTime).Value = lesos.data.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?biudz", MySqlDbType.Int32).Value = lesos.fk_biudzetas;
            mySqlCommand.Parameters.Add("?uzs", MySqlDbType.Int32).Value = lesos.fk_uzsakovas;
            mySqlCommand.Parameters.Add("?rem", MySqlDbType.Int32).Value = lesos.fk_remejas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addLesos(LesosEditViewModel lesos)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `lesos` (
                                    `Suma`,
                                    `Data`,
                                    `fk_Biudzetas`,
                                    `fk_Uzsakovas2`,
                                    `fk_Remejas`)
                                    VALUES(
                                     ?sum,
                                     ?data,
                                     ?biudz,
                                     ?uzs,
                                     ?rem)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?sum", MySqlDbType.String).Value = lesos.suma;
            mySqlCommand.Parameters.Add("?data", MySqlDbType.DateTime).Value = lesos.data.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?biudz", MySqlDbType.Int32).Value = lesos.fk_biudzetas;
            mySqlCommand.Parameters.Add("?uzs", MySqlDbType.Int32).Value = lesos.fk_uzsakovas;
            mySqlCommand.Parameters.Add("?rem", MySqlDbType.Int32).Value = lesos.fk_remejas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public void deleteIslaidos(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM lesos where Nr=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}