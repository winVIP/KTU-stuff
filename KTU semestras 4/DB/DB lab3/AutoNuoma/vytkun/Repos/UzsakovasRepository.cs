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
    public class UzsakovasRepository
    {
        public List<UzsakovasListViewModel> getUzsakovai()
        {
            List<UzsakovasListViewModel> uzsakovai = new List<UzsakovasListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM uzsakovai";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();

            foreach (DataRow item in dataTable.Rows)
            {
                uzsakovai.Add(new UzsakovasListViewModel
                {
                    id = Convert.ToInt32(item["ID"]),
                    vardas = Convert.ToString(item["Vardas"]),
                    pavarde = Convert.ToString(item["Pavarde"]),
                    telefonas = Convert.ToString(item["Telefonas"]),
                    kompanija = Convert.ToString(item["Kompanija"]),
                    el_pastas = Convert.ToString(item["El_Pastas"]),
                    adresas = Convert.ToString(item["Adresas"]),
                });
            }

            return uzsakovai;
        }

        public UzsakovasEditViewModel getUzsakovas(int nr)
        {
            UzsakovasEditViewModel uzsakovas = new UzsakovasEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM uzsakovai WHERE ID=" + nr;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                uzsakovas.id = Convert.ToInt32(item["ID"]);
                uzsakovas.vardas = Convert.ToString(item["Vardas"]);
                uzsakovas.pavarde = Convert.ToString(item["Pavarde"]);
                uzsakovas.telefonas = Convert.ToString(item["Telefonas"]);
                uzsakovas.kompanija = Convert.ToString(item["Kompanija"]);
                uzsakovas.el_pastas = Convert.ToString(item["El_Pastas"]);
                uzsakovas.adresas = Convert.ToString(item["Adresas"]);
            }

            return uzsakovas;
        }

        public bool updateUzsakovas(UzsakovasEditViewModel uzsakovas)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `uzsakovai` SET
                                    `Vardas` = ?vard,
                                    `Pavarde` = ?pavard,
                                    `Telefonas` = ?tele,
                                    `Kompanija` = ?kom,
                                    `El_Pastas` = ?elp,
                                    `Adresas` = ?adr
                                    WHERE ID=" + uzsakovas.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?vard", MySqlDbType.String).Value = uzsakovas.vardas;
            mySqlCommand.Parameters.Add("?pavard", MySqlDbType.String).Value = uzsakovas.pavarde;
            mySqlCommand.Parameters.Add("?tele", MySqlDbType.String).Value = uzsakovas.telefonas;
            mySqlCommand.Parameters.Add("?kom", MySqlDbType.String).Value = uzsakovas.kompanija;
            mySqlCommand.Parameters.Add("?elp", MySqlDbType.String).Value = uzsakovas.el_pastas;
            mySqlCommand.Parameters.Add("?adr", MySqlDbType.String).Value = uzsakovas.adresas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addUzsakovas(UzsakovasEditViewModel uzsakovas)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `uzsakovai` (
                                    `Vardas`,
                                    `Pavarde`,
                                    `Telefonas`,
                                    `Kompanija`,
                                    `El_Pastas`,
                                    `Adresas`)
                                    VALUES(
                                     ?vard,
                                     ?pavard,
                                     ?tele,
                                     ?kom,
                                     ?elp,
                                     ?adr)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?vard", MySqlDbType.String).Value = uzsakovas.vardas;
            mySqlCommand.Parameters.Add("?pavard", MySqlDbType.String).Value = uzsakovas.pavarde;
            mySqlCommand.Parameters.Add("?tele", MySqlDbType.String).Value = uzsakovas.telefonas;
            mySqlCommand.Parameters.Add("?kom", MySqlDbType.String).Value = uzsakovas.kompanija;
            mySqlCommand.Parameters.Add("?elp", MySqlDbType.String).Value = uzsakovas.el_pastas;
            mySqlCommand.Parameters.Add("?adr", MySqlDbType.String).Value = uzsakovas.adresas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public int getUzsakovasProjektaiCount(int id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(fk_Uzsakovas) as kiekis from projektai where fk_Uzsakovas=" + id;
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

        public int getUzsakovasLesosCount(int id)
        {
            int naudota = 0;
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT count(fk_Uzsakovas2) as kiekis from lesos where fk_Uzsakovas2=" + id;
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

        public void deleteUzsakovas(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM uzsakovai where ID=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}