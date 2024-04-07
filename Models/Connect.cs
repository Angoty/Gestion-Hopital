using Npgsql;
using System;

namespace Hopital.Models
{
    public class Connect
    {
        public static NpgsqlConnection connectDB()
        {
            var server = "localhost";
            var port = "5432"; // Port par défaut de PostgreSQL
            var database = "hopital";
            var username = "postgres";
            var password = "mdpprom15";

            // Chaîne de connexion à la base de données PostgreSQL
            var connString = $"Host={server};Port={port};Database={database};Username={username};Password={password}";

            // Créer une instance de connexion à la base de données
            NpgsqlConnection conn = new NpgsqlConnection(connString);

            try{
                Console.WriteLine("Ouverture de la connexion...");
                conn.Open();
                Console.WriteLine("Connexion réussie !");
            }
            catch (Exception e){
                throw e;
            }
            
            return conn;
        }
    }
}
