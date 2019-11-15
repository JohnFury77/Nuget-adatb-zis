using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adatbáziskezelés
{
    class Program
    {
        

        static void Main(string[] args)
        {
            using (var conn = new SQLiteConnection("Data Source=mydb.db"))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = @"CREATE TABLE IF NOT EXISTS macskak(id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                        nev VARCHAR(1000) NOT NULL,
                                        meret INTEGER NOT NULL)";
                command.ExecuteNonQuery();

                /*
                var beszurcmd = conn.CreateCommand();
                beszurcmd.CommandText = @"INSERT INTO macskak(nev, meret)
                                        VALUES ('Tigris', 45), ('Cirmos', 20), ('Pici', 120)";
                beszurcmd.ExecuteNonQuery();
                */
                var osszegcmd = conn.CreateCommand();
                osszegcmd.CommandText = @"SELECT COUNT(*) FROM macskak";
                long db = (long)osszegcmd.ExecuteScalar();

                Console.WriteLine("Darab: " + db);

                Console.WriteLine("Mekkora macskakő?");
                string usermeterstr = Console.ReadLine();
                int usermeret;
                if (!int.TryParse
                    (usermeterstr,out usermeret))
                {
                    Console.WriteLine("Ervenytelen meret");
                    return;
                }
                
                var lekerdezescmd = conn.CreateCommand();
                lekerdezescmd.CommandText = @"SELECT id, nev, meret FROM macskak
                                            WHERE meret>=@meret";

                using (var reader= lekerdezescmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string nev = reader.GetString(1);
                        int meret = reader.GetInt32(2);
                        Console.WriteLine("{0}, {1}cm, {2}", nev, meret, id);

                    }
                }
                Console.ReadKey();
            }
            
        }
    }
}
