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
    public class RoleRepository
    {
        public List<RoleListViewModel> getRoles()
        {
            List<RoleListViewModel> roles = new List<RoleListViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM roles";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();

            foreach (DataRow item in dataTable.Rows)
            {
                roles.Add(new RoleListViewModel
                {
                    id = Convert.ToInt32(item["Nr"]),
                    pavadinimas = Convert.ToString(item["Pavadinimas"]),
                    uzduotis = Convert.ToString(item["Uzduotis"]),
                    nuo_kada = Convert.ToDateTime(item["Nuo_kada"]),
                    iki_kada = Convert.ToDateTime(item["Iki_kada"]),
                });
            }

            return roles;
        }

        public RoleEditViewModel getRole(int nr)
        {
            RoleEditViewModel role = new RoleEditViewModel();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"SELECT * FROM roles WHERE Nr=" + nr;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlConnection.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mySqlConnection.Close();

            foreach (DataRow item in dt.Rows)
            {
                role.id = Convert.ToInt32(item["Nr"]);
                role.pavadinimas = Convert.ToString(item["Pavadinimas"]);
                role.uzduotis = Convert.ToString(item["Uzduotis"]);
                role.nuo_kada = Convert.ToDateTime(item["Nuo_kada"]);
                role.iki_kada = Convert.ToDateTime(item["Iki_kada"]);
            }

            return role;
        }

        public bool updateRole(RoleEditViewModel role)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"UPDATE `roles` SET
                                    `Pavadinimas` = ?pav,
                                    `Uzduotis` = ?uzd,
                                    `Nuo_kada` = ?nuo,
                                    `Iki_kada` = ?iki,
                                    WHERE Nr=" + role.id;
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.String).Value = role.pavadinimas;
            mySqlCommand.Parameters.Add("?uzd", MySqlDbType.String).Value = role.uzduotis;
            mySqlCommand.Parameters.Add("?nuo", MySqlDbType.DateTime).Value = role.nuo_kada.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?iki", MySqlDbType.DateTime).Value = role.iki_kada.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public bool addRole(RoleEditViewModel role)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"INSERT INTO `roles` (
                                    `Pavadinimas`,
                                    `Uzduotis`,
                                    `Nuo_kada`,
                                    `Iki_kada`)
                                    VALUES(
                                     ?pav,
                                     ?role,
                                     ?nuo,
                                     ?iki)";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pav", MySqlDbType.String).Value = role.pavadinimas;
            mySqlCommand.Parameters.Add("?uzd", MySqlDbType.String).Value = role.uzduotis;
            mySqlCommand.Parameters.Add("?nuo", MySqlDbType.DateTime).Value = role.nuo_kada.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlCommand.Parameters.Add("?iki", MySqlDbType.DateTime).Value = role.iki_kada.ToString("yyyy-MM-dd hh:mm:ss");
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();

            return true;
        }

        public void deleteRole(int id)
        {
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = @"DELETE FROM roles where Nr=?id";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
            mySqlConnection.Open();
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}