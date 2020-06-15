
Use FIRMA

-- Usuwanie Tabela asocjacyjna prac_sprzet--
IF OBJECT_ID('prac_sprzet') IS NOT NULL
	DROP TABLE prac_sprzet;
	
--Usuwanie tabeli pracownik--
IF OBJECT_ID('pracownik') IS NOT NULL
	DROP TABLE pracownik;
	
--Usuwanie tabeli laptop--
IF OBJECT_ID('laptop') IS NOT NULL
	DROP TABLE laptop;
	
--Usuwanie tabeli samochod--
IF OBJECT_ID('samochod') IS NOT NULL
	DROP TABLE samochod;
	


--Usuwanie tabeli telefon--
IF OBJECT_ID('telefon') IS NOT NULL
	DROP TABLE telefon;
	



