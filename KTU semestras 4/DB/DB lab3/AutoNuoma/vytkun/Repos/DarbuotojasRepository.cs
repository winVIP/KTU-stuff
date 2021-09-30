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
    public class DarbuotojasRepository
    {
        public List<DarbuotojasListViewModel> getDarbuotojai()
        {
            List<DarbuotojasListViewModel> darbuotojai = new List<DarbuotojasListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.*, b.Nr, b.Pavadinimas as role FROM darbuotojai a LEFT JOIN roles b ON a.fk_Role = b.Nr";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();

            foreach (DataRow item in dataTable.Rows)
            {
                darbuotojai.Add(new DarbuotojasListViewModel
                {
                    id = Convert.ToInt32(item["ID"]),
                    vardas = Convert.ToString(item["Vardas"]),
                    pavarde = Convert.ToString(item["Pavarde"]),
                    telefonas = Convert.ToString(item["Telefonas"]),
                    el_pastas = Convert.ToString(item["El_Pastas"]),
                    adresas = Convert.ToString(item["Adresas"]),
                    role = Convert.ToString(item["role"])
                });
            }

            return darbuotojai;
        }

        public DarbuotojasEditViewModel getDarbuotojas(int nr)
        {
            DarbuotojasEditViewModel darbuotojas = new DarbuotojasEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM darbuotojai WHERE ID=" + nr;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                darbuotojas.id = Convert.ToInt32(item["ID"]);
                darbuotojas.vardas = Convert.ToString(item["Vardas"]);
                darbuotojas.pavarde = Convert.ToString(item["Pavarde"]);
                darbuotojas.telefonas = Convert.ToString(item["Telefonas"]);
                darbuotojas.el_pastas = Convert.ToString(item["El_Pastas"]);
                darbuotojas.adresas = Convert.ToString(item["Adresas"]);
                darbuotojas.fk_uzduotis = Convert.ToInt32(item["fk_Uzduotis2"]);
                darbuotojas.fk_role = Convert.ToInt32(item["fk_Role"]);
            }

            return darbuotojas;
        }

        public bool updateDarbuotojas(DarbuotojasEditViewModel darbuotojas)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `darbuotojai` SET
                                    `Vardas` = ?vard,
                                    `Pavarde` = ?pavard,
                                    `Telefonas` = ?tele,
                                    `El_Pastas` = ?elp
                                    `Adresas` = ?adr
                                    `fk_Uzduotis2` = ?uzd
                                    `fk_Role` = ?role
                                    WHERE ID=" + darbuotojas.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?vard", MySqlDbType.String).Value = darbuotojas.vardas;
            mySqlCommand.Parameters.Add("?pavard", MySqlDbType.String).Value = darbuotojas.pavarde;
            mySqlCommand.Parameters.Add("?tele", MySqlDbType.String).Value = darbuotojas.telefonas;
            mySqlCommand.Parameters.Add("?elp", MySqlDbType.String).Value = darbuotojas.el_pastas;
            mySqlCommand.Parameters.Add("?adr", MySqlDbType.String).Value = darbuotojas.adresas;
            mySqlCommand.Parameters.Add("?uzd", MySqlDbType.Int32).Value = darbuotojas.fk_uzduotis;
            mySqlCommand.Parameters.Add("?role", MySqlDbType.Int32).Value = darbuotojas.fk_role;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addDarbuotojas(DarbuotojasEditViewModel darbuotojas)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `darbuotojai` (
                                    `Vardas`,
                                    `Pavarde`,
                                    `Telefonas`,
                                    `El_Pastas`,
                                    `Adresas`,
                                    `fk_Uzduotis2`,
                                    `fk_Role`)
                                    VALUES(
                                     ?vard,
                                     ?pavard,
                                     ?tele,
                                     ?elp,
                                     ?adr,
                                     ?uzd,
                                     ?role)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?vard", MySqlDbType.String).Value = darbuotojas.vardas;
            mySqlCommand.Parameters.Add("?pavard", MySqlDbType.String).Value = darbuotojas.pavarde;
            mySqlCommand.Parameters.Add("?tele", MySqlDbType.String).Value = darbuotojas.telefonas;
            mySqlCommand.Parameters.Add("?elp", MySqlDbType.String).Value = darbuotojas.el_pastas;
            mySqlCommand.Parameters.Add("?adr", MySqlDbType.String).Value = darbuotojas.adresas;
            mySqlCommand.Parameters.Add("?uzd", MySqlDbType.Int32).Value = darbuotojas.fk_uzduotis;
            mySqlCommand.Parameters.Add("?role", MySqlDbType.Int32).Value = darbuotojas.fk_role;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public void deleteProject(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM darbuotojai where ID=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}