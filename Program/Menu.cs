using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectAppFirma
{
    //Class Menu - Contains Method to Print and Handle program Menu
    class Menu
    {
        public static void PrintMenu()
        {
            Console.WriteLine(
                @"
####################################
#                                  #
#          MENU - Firma            #
#                                  #
#                                  #
#     Mikolaj Baczynski - 2020     #
#                                  #
####################################
#                                  #
#  Dostepne opcje:                 #
# 1. Wyswietlanie informacji       #
# 2. Wprowadzanie danych           #
# 3. Usuwanie danych               #
#                                  #
# 0. Wyjscie                       #
#                                  #
####################################
"
            );
        }

        public static void PrintSelectMenu()
        {
            Console.Clear();
            Console.WriteLine(
                @"
####################################
#                                  #
#   MENU - Wyświetlanie raportow   #
#                                  #
####################################
#                                  #
#  Wyswietl:                       #
# 1.  Pracownika/ow                #
# 2.  Sprzet Pracownika/ow         #
# 3.  Nr.Telefonu Pracownika/ow    #
# 4.  Nr.Konta Pracownika/ow       #
# 5.  Wszystkie Samochody          #
# 6.  Wszystkie Laptopy            #
# 7.  Wszystkie Telefony           #
# 8.  Dostepne Samochody           #
# 9.  Dostepne Laptopy             #
# 10. Dostepne Telefony            #
#                                  #
#                                  #
#                                  #
####################################
"
            );
        }

        public static void HandleSelectMenu()
        {
            int option;
            int.TryParse(Console.ReadLine(), out option);
            string surname = "";
            if (option > 0 && option < 11)
            {
                if (option < 5)
                {
                    Console.WriteLine("Podaj nazwisko pracownika, lub pozostaw puste by wyswietlic wszystkich");
                    surname = Console.ReadLine();
                }
                Select.ExecuteSelect(option, surname);
            }


        }

        public static void HandleInsertMenu()
        {
            int option;
            int.TryParse(Console.ReadLine(), out option);
            string data = "";
            if (option > 0 && option < 5)
            {
                switch (option)
                {
                    case 1:
                        Console.WriteLine("Dodaj pracownika (osoba,adres,nrkonta)\n");
                        Console.WriteLine("Wprowadz osoba (nazwisko,imie,plec(mezczyna/kobieta),pesel(11 cyfr) )\n");
                        string osoba = Console.ReadLine();
                        Console.WriteLine("Wprowadz adres (miasto,ulica,nr,kodPocztowy )\n");
                        string adres = Console.ReadLine();
                        Console.WriteLine("Wprowadz neKonta (nrKonta(26-cyfr))\n");
                        string nrKonta = Console.ReadLine();
                        data = osoba + "','" + adres + "','" + nrKonta;

                        break;
                    case 2:
                        Console.WriteLine("Dodaj telefon (Frima,Model,nrTelefonu(9-cyfr))\n");
                        data = Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Dodaj laptop (Frima,Model,nrSeryjny)\n");
                        data = Console.ReadLine();
                        break;
                    case 4:
                        Console.WriteLine("Dodaj samochod (Frima,Model,Rejestracja(2-zanki+5-cyfr))\n");
                        data = Console.ReadLine();
                        break;
                }
                Insert.ExecuteInsert(option, data);
            }
            else if (option >= 5 && option < 8)
            {
                Console.WriteLine("Dodaj sprzet pracownikowi: id_pracownik,id_sprzet\n");
                string s = Console.ReadLine();
                string[] temp = s.Split(",".ToCharArray());
                Insert.ExecuteUpadte(option, temp[0], temp[1]);
            }


        }

        public static void PrintInsertMenu()
        {
            Console.Clear();
            Console.WriteLine(
                @"
####################################
#                                  #
#        MENU - Wstawianie         #
# dane oddzielamy ',' bez spacji   #
####################################
#                                  #
#  Wstaw:                          #
# 1.  Pracownika                   #
# 2.  Telefon                      #
# 3.  Laptop                       #
# 4.  Samochod                     #
# 5.  Laptop dla pracownika        #
# 6.  Telefon  dla pracownika      # 
# 7.  Samochod  dla pracownika     # 
#                                  # 
#                                  #
#                                  #
#                                  #
####################################
"
            );
        }

        public static void PrintDeleteMenu()
        {
            Console.Clear();
            Console.WriteLine(
                @"
####################################
#                                  #
#        MENU - Usuwanie           #  
# Usuwanie za pomoca Id            #
####################################
#                                  #
#  Usun :                          #
# 1.  Pracownika                   #
# 2.  Telefon                      #
# 3.  Laptop                       #
# 4.  Samochod                     #
#                                  #
#                                  #
#                                  #
####################################
"
            );
        }

        public static void HandleDeleteMenu()
        {
            int option;
            int.TryParse(Console.ReadLine(), out option);
            string data = "";
            if (option > 0 && option < 5)
            {
                Console.WriteLine("Usun podajac id");
                data = Console.ReadLine();
            }
            Delete.ExecuteDelete(option, data);
        }
        
    }
}
