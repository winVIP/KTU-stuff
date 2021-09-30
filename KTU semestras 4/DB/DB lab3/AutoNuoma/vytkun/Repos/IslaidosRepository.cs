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
    public class IslaidosRepository
    {
        public List<IslaidosListViewModel> getIslaidos()
        {
            List<IslaidosListViewModel> islaidos = new List<IslaidosListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.*, b.Nr, b.Pavadinimas as uzduotis FROM islaidos a LEFT JOIN uzduotys b ON b.Nr = a.fk_Uzduotis";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();

            foreach (DataRow item in dataTable.Rows)
            {
                islaidos.Add(new IslaidosListViewModel
                {
                    id = Convert.ToInt32(item["Nr"]),
                    suma = Convert.ToDouble(item["Suma"]),
                    paskirtis = Convert.ToString(item["Paskirtis"]),
                    data = Convert.ToDateTime(item["Data"]),
                    uzduotis = Convert.ToString(item["uzduotis"])
                });
            }

            return islaidos;
        }

        public IslaidosEditViewModel getIslaida(int nr)
        {
            IslaidosEditViewModel islaida = new IslaidosEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM islaidos WHERE Nr=" + nr;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                islaida.id = Convert.ToInt32(item["Nr"]);
                islaida.suma = Convert.ToDouble(item["Suma"]);
                islaida.paskirtis = Convert.ToString(item["Paskirtis"]);
                islaida.data = Convert.ToDateTime(item["Data"]);
                islaida.fk_biudzetas = Convert.ToInt32(item["fk_Biudzetas2"]);
                islaida.fk_uzduotis = Convert.ToInt32(item["fk_Uzduotis"]);
            }

            return islaida;
        }

        public bool updateIslaidos(IslaidosEditViewModel islaida)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `islaidos` SET
                                    `Suma` = ?sum,
                                    `Paskirtis` = ?pask,
                                    `Data` = ?data,
                                    `fk_Biudzetas2` = ?biudz
                                    `fk_Uzduotis` = ?uzd
                                    WHERE Nr=" + islaida.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?sum", MySqlDbType.String).Value = islaida.suma;
            mySqlCommand.Parameters.Add("?pask", MySqlDbType.String).Value = islaida.paskirtis;
            mySqlCommand.Parameters.Add("?data", MySqlDbType.DateTime).Value = islaida.data.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?biudz", MySqlDbType.Int32).Value = islaida.fk_biudzetas;
            mySqlCommand.Parameters.Add("?uzd", MySqlDbType.Int32).Value = islaida.fk_uzduotis;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addIslaidos(IslaidosEditViewModel islaida)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `islaidos` (
                                    `Suma`,
                                    `Paskirtis`,
                                    `Data`,
                                    `fk_Biudzetas2`,
                                    `fk_Uzduotis`)
                                    VALUES(
                                     ?sum,
                                     ?pask,
                                     ?data,
                                     ?biudz,
                                     ?uzd)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?sum", MySqlDbType.String).Value = islaida.suma;
            mySqlCommand.Parameters.Add("?pask", MySqlDbType.String).Value = islaida.paskirtis;
            mySqlCommand.Parameters.Add("?data", MySqlDbType.DateTime).Value = islaida.data.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?biudz", MySqlDbType.Int32).Value = islaida.fk_biudzetas;
            mySqlCommand.Parameters.Add("?uzd", MySqlDbType.Int32).Value = islaida.fk_uzduotis;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public void deleteIslaidos(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM islaidos where Nr=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}