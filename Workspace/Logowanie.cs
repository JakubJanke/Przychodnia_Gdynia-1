using System;
using System.Data.SQLite;

namespace Przychodnia_Gdynia
{
    class Logowanie
    {
        public static void LogowanieUzytkownika()
        {
            string cs = "Data Source=./uzytkownicy.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            System.Console.WriteLine("Wpisz numer Pesel: ");
            string pesel = Console.ReadLine();
            using var cmd4 = new SQLiteCommand($"SELECT COUNT (*) FROM users WHERE pesel = '{pesel}' ", con);
            //cmd4.ExecuteNonQuery();
            object temp = cmd4.ExecuteScalar();
            string tmp = temp.ToString();
            int temp2 = int.Parse(tmp);
            if(temp2 == 1)
            {
                //System.Console.WriteLine(temp);
                //Console.ReadKey();
                using var cmd3 = new SQLiteCommand($"SELECT pesel FROM users WHERE pesel='{pesel}'", con);


                object peselCheck = cmd3.ExecuteScalar();
                string psl = peselCheck.ToString();
                if(pesel == psl)
                {
                    System.Console.WriteLine("Wpisz Hasło: ");
                    string password = Console.ReadLine();

                    var cmd = new SQLiteCommand(con);
                    var cmd2 = new SQLiteCommand(con);
                    //String spr_haslo = "";
                    using var cmd1 = new SQLiteCommand("SELECT password FROM users WHERE pesel="+pesel, con);
                    //using SQLiteDataReader reader = cmd1.ExecuteReader();
                    object passwordCheck = cmd1.ExecuteScalar();
                    string passwd = passwordCheck.ToString();
                    if(passwd == password)
                    {

                        System.Console.WriteLine("Zalogowano pomyślnie.");
                        cmd.CommandText = $"UPDATE users SET user_isLog = true WHERE pesel="+pesel;
                        cmd.ExecuteNonQuery();
                        cmd2.CommandText = "SELECT user_isLog FROM users WHERE pesel="+pesel;
                        //Console.WriteLine(cmd2.ExecuteScalar());
                        Funkcje_Pomocnicze.ClickToContinue();
                        Menu.User_Panel();
                    }
                    else
                    {
                    System.Console.WriteLine("Niepoprawne hasło.");
                    Funkcje_Pomocnicze.EmptySpaceDots(3);
                    Funkcje_Pomocnicze.ClickToContinue();
                    Menu.Menu_Logowanie();
                    }
                }  
            }
            else
            {
                Console.WriteLine("Nie znaleziono peselu w bazie. ");
                Console.WriteLine("Spróbuj ponownie.");
                LogowanieUzytkownika();
            }
        }
    }
}