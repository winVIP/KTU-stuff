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
    public class DalyvisRepository
    {
        public List<DalyvisListViewModel> getDalyviai()
        {
            List<DalyvisListViewModel> dalyviai = new List<DalyvisListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT a.*, b.fk_Dalyvis, b.fk_Projektas, c.Nr, c.Pavadinimas as Projektas 
                                FROM dalyviai a LEFT JOIN dalyvauja b ON b.fk_Dalyvis = a.ID LEFT JOIN projektai c ON b.fk_Projektas = c.Nr";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();

            foreach (DataRow item in dataTable.Rows)
            {
                dalyviai.Add(new DalyvisListViewModel
                {
                    id = Convert.ToInt32(item["ID"]),
                    vardas = Convert.ToString(item["Vardas"]),
                    pavarde = Convert.ToString(item["Pavarde"]),
                    telefonas = Convert.ToString(item["Telefonas"]),
                    el_pastas = Convert.ToString(item["El_Pastas"]),
                    projektas = Convert.ToString(item["Projektas"])
                });
            }

            return dalyviai;
        }

        public DalyvisEditViewModel getDalyvis(int nr)
        {
            DalyvisEditViewModel dalyvis = new DalyvisEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM dalyviai WHERE ID=" + nr;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                dalyvis.id = Convert.ToInt32(item["ID"]);
                dalyvis.vardas = Convert.ToString(item["Vardas"]);
                dalyvis.pavarde = Convert.ToString(item["Pavarde"]);
                dalyvis.telefonas = Convert.ToString(item["Telefonas"]);
                dalyvis.el_pastas = Convert.ToString(item["El_Pastas"]);
            }

            return dalyvis;
        }

        public bool updateDalyvis (DalyvisEditViewModel dalyvis)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `dalyviai` SET
                                    `Vardas` = ?vardas,
                                    `Pavarde` = ?pavarde,
                                    `Telefonas` = ?tele,
                                    `El_Pastas` = ?elp
                                    WHERE ID=" + dalyvis.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?vardas", MySqlDbType.String).Value = dalyvis.vardas;
            mySqlCommand.Parameters.Add("?pavarde", MySqlDbType.String).Value = dalyvis.pavarde;
            mySqlCommand.Parameters.Add("?tele", MySqlDbType.String).Value = dalyvis.telefonas;
            mySqlCommand.Parameters.Add("?elp", MySqlDbType.String).Value = dalyvis.el_pastas;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addDalyvis(DalyvisEditViewModel dalyvis)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery1 = @"INSERT INTO `dalyviai`(
                                    `Vardas`,
                                    `Pavarde`,
                                    `Telefonas`,
                                    `El_Pastas`)
                                    VALUES(
                                     ?vardas,
                                     ?pavarde,
                                     ?tele,
                                     ?elp)";
            MySqlCommand mySqlCommand1 = new MySqlCommand(sqlquery1, mySqlConnection);
            mySqlCommand1.Parameters.Add("?vardas", MySqlDbType.String).Value = dalyvis.vardas;
            mySqlCommand1.Parameters.Add("?pavarde", MySqlDbType.String).Value = dalyvis.pavarde;
            mySqlCommand1.Parameters.Add("?tele", MySqlDbType.String).Value = dalyvis.telefonas;
            mySqlCommand1.Parameters.Add("?elp", MySqlDbType.String).Value = dalyvis.el_pastas;

            mySqlConnection.Open();
            mySqlCommand1.ExecuteNonQuery();
            mySqlConnection.Close();

            string sqlquery3 = @"SELECT MAX(ID) FROM dalyviai";
            MySqlCommand mySqlCommand3 = new MySqlCommand(sqlquery3, mySqlConnection);
            mySqlConnection.Open();
            object projektas = mySqlCommand3.ExecuteScalar();
            mySqlConnection.Close();

            int intProjektas = (Int32)projektas;

            string sqlquery2 = @"INSERT INTO `dalyvauja` (
                                             `fk_Dalyvis`,
                                             `fk_Projektas`)
                                             VALUES(
                                             ?dal,
                                             ?pro)";
            MySqlCommand mySqlCommand2 = new MySqlCommand(sqlquery2, mySqlConnection);
            mySqlCommand2.Parameters.Add("?dal", MySqlDbType.Int32).Value = intProjektas;
            mySqlCommand2.Parameters.Add("?pro", MySqlDbType.Int32).Value = dalyvis.fk_projektas;
            System.Diagnostics.Debug.WriteLine("dalyvauja: " + intProjektas + " " + dalyvis.fk_projektas);

            

            mySqlConnection.Open();
            mySqlCommand2.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public void setDalyvauja()
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery1 = @"SELECT MAX(ID) FROM dalyviai";
            MySqlCommand mySqlCommand1 = new MySqlCommand(sqlquery1, mySqlConnection);
        }

        public void deleteDalyvis(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery1 = @"DELETE FROM dalyvauja where fk_Dalyvis=?id";
            string sqlquery2 = @"DELETE FROM dalyviai where ID=?id";
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