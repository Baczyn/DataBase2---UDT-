--Tworzenie nowej bazy dla projektu--
--CREATE DATABASE FIRMA;

USE FIRMA
GO

--Tworzenie tabeli pracownik--
CREATE TABLE pracownik(
    id INT PRIMARY KEY IDENTITY (1, 1),
    osoba dbo.Person NOT NULL,
    adres dbo.Location NOT NULL,
    nrKonta dbo.AccountNr NOT NULL,
)
--Tworzenie tabeli laptop--
CREATE TABLE laptop(
	id INT PRIMARY KEY IDENTITY (1, 1),
	laptop dbo.Laptop NOT NULL,
)

--Tworzenie tabeli samochod--
CREATE TABLE samochod(
	id INT PRIMARY KEY IDENTITY (1, 1),
	samochod dbo.Car NOT NULL,
)
--Tworzenie tabeli telefon--
CREATE TABLE telefon(
	id INT PRIMARY KEY IDENTITY (1, 1),
	telefon dbo.Phone NOT NULL,
)
--Tabela asocjacyjna prac_sprzet--
CREATE TABLE prac_sprzet(
    id_pracownik int UNIQUE NOT NULL,
    id_laptop int,
    id_telefon int ,
    id_samochod int,
    
    FOREIGN KEY (id_pracownik) REFERENCES pracownik(id) ON DELETE CASCADE,
    FOREIGN KEY (id_laptop) REFERENCES laptop(id) ON DELETE CASCADE,
    FOREIGN KEY (id_telefon) REFERENCES telefon(id) ON DELETE CASCADE,
    FOREIGN KEY (id_samochod) REFERENCES samochod(id) ON DELETE CASCADE,
)
--Tworzenie unikalnych atrybutow ale moga powtarzac sie jako NULL
CREATE UNIQUE INDEX indunique
  ON prac_sprzet(id_laptop)
  WHERE id_laptop IS NOT NULL
CREATE UNIQUE INDEX indunique1
  ON prac_sprzet(id_telefon)
  WHERE id_telefon IS NOT NULL
 CREATE UNIQUE INDEX indunique2
  ON prac_sprzet(id_samochod)
  WHERE id_samochod IS NOT NULL
