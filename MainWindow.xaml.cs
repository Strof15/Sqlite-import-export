using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;

namespace PeterMiklosAdatbazisSQLITE
{
    //1. Olvasd be a games.txt tartalmát, és hozz létre az adatokból Sqlite adatbázis tábláját!
    //2. A létrehozás után olvasd vissza az Sqlite adatbázist, majd jelenítsd meg az adatokat DataGrid-ben!
    public partial class MainWindow : Window
    {
        private readonly string filePath = "Filename=Jatekok.db";
        List<Jatekok> JatekokDataList = new();
        public MainWindow()
        {
            InitializeComponent();
            CreateTable();
        }

        public void CreateTable()
        {
           
                
            SqliteConnection firstConnention = new(filePath);
            firstConnention.Open();
            string commandText = "CREATE TABLE IF NOT EXISTS Jatekok(name VARCHAR(100), review DOUBLE, price INT(20), studio VARCHAR(100))";
            SqliteCommand command = new(commandText, firstConnention);
            command.ExecuteNonQuery();
            string[] lines = File.ReadAllLines("games.txt");
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                line.Skip(1);
                string[] gamesArray = line.Split(";");
                Jatekok games = new Jatekok(
                    gamesArray[0],
                    double.Parse(gamesArray[1]),
                    int.Parse(gamesArray[2]),
                    gamesArray[3]);
                    JatekokDataList.Add(games);
            }
            



                SqliteConnection secondConnection = new(filePath);
                secondConnection.Open();
                string insertQuery = "INSERT INTO Jatekok (name,review,price,studio) VALUES (@name, @review, @price, @studio)";
                SqliteCommand insertCommand = new(insertQuery, secondConnection);
                foreach (var game in JatekokDataList)
                {
                    insertCommand.Parameters.Clear();
                    insertCommand.Parameters.AddWithValue("@name", game.Nev);
                    insertCommand.Parameters.AddWithValue("@review", game.Ertekeles);
                    insertCommand.Parameters.AddWithValue("@price", game.Ar);
                    insertCommand.Parameters.AddWithValue("@studio", game.Kiado);
                    insertCommand.ExecuteNonQuery();
                }


                SqliteCommand command2 = new SqliteCommand("SELECT * FROM Jatekok", secondConnection);
                SqliteDataReader reader = command2.ExecuteReader();
                while (reader.Read())
                {
                    string nev = reader.GetString(0);
                    double ertekeles = reader.GetDouble(1);
                    int ar = reader.GetInt32(2);
                    string kiado = reader.GetString(3);
                    Jatekok newGames = new(nev, ertekeles, ar, kiado);
                    JatekokDataList.Add(newGames);
                }
                DgShowDatas.ItemsSource = JatekokDataList;
           

        }
    }
}
