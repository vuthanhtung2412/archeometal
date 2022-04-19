using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
//using System.Data.SQLite;


namespace bdd_ep
{
    /// <summary>
    /// Utility static class to interact with the Database
    /// </summary>
    public static class Database
    {
        /// <summary>
        /// Path to the Database
        /// </summary>
        private static string Cs = "URI=file:" + Application.persistentDataPath + "/db_artefact_augmente.db";

        /// <summary>
        /// The Database object
        /// </summary>
        private static SqliteConnection _db;

        /// <summary>
        /// Open the Database
        /// </summary>
        private static void open_database()
        {
            //Console.WriteLine("Opening Database...");
            _db = new SqliteConnection(Cs);
            _db.Open();
            //Console.WriteLine("Database is open !");
        }

        /// <summary>
        /// Close the Database
        /// </summary>
        private static void close_database()
        {
            //Console.WriteLine("Closing Database...");
            _db?.Close();
            //Console.WriteLine("Database is closed");
        }

        /// <summary>
        /// Check if Database number of tables is 5
        /// </summary>
        /// <returns>True if and only if the number of tables is 5</returns>
        private static bool check_database()
        {
            var tables = bdd_ep.Database.DataReader("SELECT tbl_name FROM sqlite_master WHERE type = 'table';");
            return (tables.Rows.Count - 1) == 5;
        }

        /// <summary>
        /// Inspect the database to check if all 5 tables are there
        /// </summary>
        public static void database_inspection()
        {
            if (check_database())
            {
                //Console.WriteLine("OK : Database contains 5 tables");
            }
            else
            {
                //Console.WriteLine("KO : Database doest not contain 5 tables");
                //Environment.Exit(-1);
            }
        }

        /// <summary>
        /// Read data from the Database
        /// </summary>
        /// <param name="command">String describing the request to the Database</param>
        /// <returns></returns>
        public static DataTable DataReader(string command)
        {
            var dataTable = new DataTable();

            open_database();
            //Console.WriteLine("Executing ReadCommand : " + command);
            using var cmd = new SqliteCommand(command, _db);
            var rdr = cmd.ExecuteReader();

            // Write information into the datatable
            dataTable.Load(rdr);

            close_database();
            return dataTable;
        }

        /// <summary>
        /// Write data from the Database
        /// </summary>
        /// <param name="command">String describing the request to the Database</param>
        /// <returns></returns>
        public static void DataWriter(string command)
        {
            open_database();
            //Console.WriteLine("Executing WriteCommand : " + command);
            using var cmd = new SqliteCommand(command, _db);
            //Console.WriteLine(cmd.ExecuteScalar());
            close_database();
        }
    }
}