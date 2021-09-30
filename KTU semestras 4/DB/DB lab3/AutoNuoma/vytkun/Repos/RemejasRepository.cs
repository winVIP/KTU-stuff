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
    public class RemejasRepository
    {
        public List<RemejasListViewModel> getRemejai()
        {
            List<RemejasListViewModel> remejai = new List<RemejasListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM remejai";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();

            foreach (DataRow item in dataTable.Rows)
            {
                remejai.Add(new RemejasListViewModel
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

            return remejai;
        }

        public RemejasEditViewModel getRemejas(int nr)
        {
            RemejasEditViewModel remejas = new RemejasEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM remejai WHERE ID=" + nr;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                remejas.id = Convert.ToInt32(item["ID"]);
                remejas.vardas = Convert.ToString(item["Vardas"]);
                remejas.pavarde = Convert.ToString(item["Pavarde"]);
                remejas.telefonas = Convert.ToString(item["Telefonas"]);
                remejas.kompanija = Convert.ToString(item["Kompanija"]);
                remejas.el_pastas = Convert.ToString(item["El_Pastas"]);
                remejas.adresas = Convert.ToString(item["Adresas"]);
            }

            return remejas;
        }

        public bool updateRemejas(RemejasEditViewModel remejas)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `remejai` SET
                                    `Vardas` = ?vard,
                                    `Pavarde` = ?pavard,
                                    `Telefonas` = ?tele,
                                    `Kompanija` = ?kom,
                                    `El_Pastas` = ?elp,
                                    `Adresas` = ?adr
                                    WHERE ID=" + remejas.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?vard", MySqlDbType.String).Value = remejas.vardas;
            mySqlCommand.Parameters.Add("?pavard", MySqlDbType.String).Value = remejas.pavarde;
            mySqlCommand.Parameters.Add("?tele", MySqlDbType.String).Value = remejas.telefonas;
            mySqlCommand.Parameters.Add("?kom", MySqlDbType.String).Value = remejas.kompanija;
            mySqlCommand.Parameters.Add("?elp", MySqlDbType.String).Value = remejas.el_pastas;
            mySqlCommand.Parameters.Add("?adr", MySqlDbType.String).Value = remejas.adresas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addRemejas(RemejasEditViewModel remejas)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `remejai` (
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
            mySqlCommand.Parameters.Add("?vard", MySqlDbType.String).Value = remejas.vardas;
            mySqlCommand.Parameters.Add("?pavard", MySqlDbType.String).Value = remejas.pavarde;
            mySqlCommand.Parameters.Add("?tele", MySqlDbType.String).Value = remejas.telefonas;
            mySqlCommand.Parameters.Add("?kom", MySqlDbType.String).Value = remejas.kompanija;
            mySqlCommand.Parameters.Add("?elp", MySqlDbType.String).Value = remejas.el_pastas;
            mySqlCommand.Parameters.Add("?adr", MySqlDbType.String).Value = remejas.adresas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public void deleteRemejas(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery1 = @"DELETE FROM lesos where fk_Remejas=?id";
            string sqlquery2 = @"DELETE FROM remejai where ID=?id";
            MySqlCommand mySqlCommand1 = new MySqlCommand(sqlquery1, mySqlConnection);
            MySqlCommand mySqlCommand2 = new MySqlCommand(sqlquery2, mySqlConnection);
            mySqlCommand1.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlCommand2.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand1.ExecuteNonQuery();
            mySqlCommand2.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}