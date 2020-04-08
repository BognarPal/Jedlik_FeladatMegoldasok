-- A feladatok megoldására elkészített SQL parancsokat illessze be a feladat sorszáma után!


-- 1. feladat:
CREATE DATABASE Formula1
	CHARACTER SET utf8
	COLLATE utf8_general_ci;

-- 3. feladat:
update nagydijak
    set korokszama = 70
where nev like 'Hungarian%';


-- 4. feladat:
SELECT
  pilotak.vezeteknev,
  pilotak.rajtszam,
  pilotak.csapatnev,
  2019 - YEAR(pilotak.szuletesidatum) AS eletkor
FROM pilotak
ORDER BY eletkor DESC;

-- 5. feladat:
SELECT
  nagydijak.nev,
  pilotak.vezeteknev,
  pilotak.keresztnev,
  nagydijak.versenynap
FROM eredmenyek
  INNER JOIN nagydijak
    ON eredmenyek.nagydijid = nagydijak.id
  INNER JOIN pilotak
    ON eredmenyek.pilotaid = pilotak.id
WHERE eredmenyek.helyezes = 1
ORDER BY nagydijak.versenynap;

-- 6. feladat:
SELECT
  CONCAT(pilotak.vezeteknev, ' ', pilotak.keresztnev) AS nev,
  pilotak.csapatnev,
  SUM(eredmenyek.pontszam) AS osszpontszam
FROM eredmenyek
  INNER JOIN pilotak
    ON eredmenyek.pilotaid = pilotak.id
GROUP BY pilotak.vezeteknev,
         pilotak.keresztnev,
         pilotak.csapatnev
ORDER BY osszpontszam DESC
Limit 3;
