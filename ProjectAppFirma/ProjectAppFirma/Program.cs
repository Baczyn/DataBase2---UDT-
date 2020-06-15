using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProjectAppFirma
{
    //Main program Loop
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.WindowWidth + Console.WindowWidth/2, Console.WindowHeight);
            bool running = true;
            while (running)
            {
                Console.Clear();
                Menu.PrintMenu();
                string c = Console.ReadLine();
                switch (c)
                {
                    case "1":
                        Menu.PrintSelectMenu();
                        Menu.HandleSelectMenu();
                        break;
                    case "2":
                        Menu.PrintInsertMenu();
                        Menu.HandleInsertMenu();
                        break;
                    case "3":
                        Menu.PrintDeleteMenu();
                        Menu.HandleDeleteMenu();
                        break;
                    case "0":
                        running = false;
                        break;

                }

            }
        }
    }
}
