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
    public class AtaskaituRepository
    {
        public List<AtaskaitaDarbuotojaiViewModel> getDarbuotojai(DateTime? nuo, DateTime? iki, string pavadinimas)
        {
            List<AtaskaitaDarbuotojaiViewModel> darbutojai = new List<AtaskaitaDarbuotojaiViewModel>();
            string conn = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            MySqlConnection mySqlConnection = new MySqlConnection(conn);
            string sqlquery = "SELECT projektai.Nr, projektai.Pavadinimas AS proPav, proCC.projektu_skaicius, darCC.darbuotoju_skaicius, " +
                "graMin.minData, graMax.maxData, " +
                "darbuotojai.ID, darbuotojai.Vardas as Vardas, darbuotojai.Pavarde as Pavarde, darbuotojai.fk_Uzduotis2, " +
                "grafikai.Nr, grafikai.Nuo_kada as nuo, grafikai.Iki_kada as iki, grafikai.fk_Projektas2, " +
                "uzduotys.Nr, uzduotys.Pavadinimas as uzdPav, uzduotys.fk_Grafikas " +
                "FROM projektai " +
                "LEFT JOIN grafikai ON projektai.Nr = grafikai.fk_Projektas2 " +
                "LEFT JOIN uzduotys ON grafikai.Nr = uzduotys.fk_Grafikas " +
                "LEFT JOIN darbuotojai ON uzduotys.Nr = darbuotojai.fk_Uzduotis2 " +

                "LEFT JOIN (SELECT COUNT(projektai.Pavadinimas) as projektu_skaicius " +
                "FROM projektai, grafikai " +
                "WHERE projektai.Nr=grafikai.fk_Projektas2 " +
                "AND projektai.Pavadinimas=IFNULL(?pavadinimas, projektai.Pavadinimas) " +
                "AND grafikai.Nuo_kada>=IFNULL(?nuo, grafikai.Nuo_kada) " +
                "AND grafikai.Iki_kada<=IFNULL(?iki, grafikai.Iki_kada)) AS proCC ON projektai.Nr=projektai.Nr " +

                "LEFT JOIN (SELECT COUNT(darbuotojai.ID) as darbuotoju_skaicius " +
                "FROM projektai, grafikai, uzduotys, darbuotojai " +
                "WHERE projektai.Nr=grafikai.fk_Projektas2 " +
                "AND grafikai.Nr=uzduotys.fk_Grafikas " +
                "AND uzduotys.Nr=darbuotojai.fk_Uzduotis2 " +
                "AND projektai.Pavadinimas=IFNULL(?pavadinimas, projektai.Pavadinimas) " +
                "AND grafikai.Nuo_kada>=IFNULL(?nuo, grafikai.Nuo_kada) " +
                "AND grafikai.Iki_kada<=IFNULL(?iki, grafikai.Iki_kada)) AS darCC ON darbuotojai.ID=darbuotojai.ID " +
                
                "LEFT JOIN (SELECT MIN(grafikai.Nuo_kada) AS minData FROM projektai, grafikai " +
                "WHERE projektai.Nr=grafikai.fk_Projektas2 " +
                "AND projektai.Pavadinimas=IFNULL(?pavadinimas, projektai.Pavadinimas) " +
                "AND grafikai.Nuo_kada>=IFNULL(?nuo, grafikai.Nuo_kada) " +
                "AND grafikai.Iki_kada<=IFNULL(?iki, grafikai.Iki_kada)) AS graMin ON grafikai.Nr=grafikai.Nr " +

                "LEFT JOIN (SELECT MAX(grafikai.Iki_kada) AS maxData FROM projektai, grafikai " +
                "WHERE projektai.Nr=grafikai.fk_Projektas2 " +
                "AND projektai.Pavadinimas=IFNULL(?pavadinimas, projektai.Pavadinimas) " +
                "AND grafikai.Nuo_kada>=IFNULL(?nuo, grafikai.Nuo_kada) " +
                "AND grafikai.Iki_kada<=IFNULL(?iki, grafikai.Iki_kada)) AS graMax ON grafikai.Nr=grafikai.Nr " +

                "WHERE projektai.Pavadinimas=IFNULL(?pavadinimas, projektai.Pavadinimas)" +
                "AND grafikai.Nuo_kada>=IFNULL(?nuo, grafikai.Nuo_kada) " +
                "AND grafikai.Iki_kada<=IFNULL(?iki, grafikai.Iki_kada) " +
                "ORDER BY projektai.Pavadinimas ASC";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlquery, mySqlConnection);
            mySqlCommand.Parameters.Add("?pavadinimas", MySqlDbType.String).Value = pavadinimas;
            mySqlCommand.Parameters.Add("?nuo", MySqlDbType.DateTime).Value = nuo;
            mySqlCommand.Parameters.Add("?iki", MySqlDbType.DateTime).Value = iki;
            mySqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();

            foreach(DataRow item in dataTable.Rows)
            {
                darbutojai.Add(new AtaskaitaDarbuotojaiViewModel
                {
                    projektoPavadinimas = Convert.ToString(item["proPav"]),
                    Vardas = Convert.ToString(item["Vardas"]),
                    Pavarde = Convert.ToString(item["Pavarde"]),
                    uzdPavadinimas = Convert.ToString(item["uzdPav"]),
                    nuo = Convert.ToDateTime(item["nuo"]),
                    iki = Convert.ToDateTime(item["iki"]),
                    sumaProjektu = Convert.ToInt32(item["projektu_skaicius"]),
                    sumaDarbuotoju = Convert.ToInt32(item["darbuotoju_skaicius"]),
                    minDate = Convert.ToDateTime(item["minData"]),
                    maxDate = Convert.ToDateTime(item["maxData"])
                }
                );
            }

            return darbutojai;
        }

    }
}