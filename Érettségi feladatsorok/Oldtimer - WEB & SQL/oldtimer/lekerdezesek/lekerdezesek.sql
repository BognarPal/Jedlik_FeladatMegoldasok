A feladatok megoldására elkészített SQL parancsokat illessze be a feladat sorszáma után!
***
15. feladat
CREATE DATABASE oldtimer
CHARACTER SET utf8
COLLATE utf8_hungarian_ci;
***
16. feladat
SELECT
  COUNT(autok.id) AS `sportautok-szama`
FROM autok
WHERE autok.kategoriaId = 4;

***
17. feladat
SELECT
  autok.nev,
  autok.szin
FROM autok
  INNER JOIN kategoriak
    ON autok.kategoriaId = kategoriak.id
WHERE kategoriak.nev = 'Limuzin'
ORDER BY autok.nev;
***
18. feladat
INSERT INTO autok(rendszam, szin, nev, evjarat, ar, kategoriaId)
VALUES ('OT44-01', 'Fekete-piros', 'GMV Vandura Szupercsapat kiadás', 1983, 18000, 3);

***
19. feladat
SELECT
  autok.nev,
  SUM(berlesek.mennyiseg) AS mennyiseg
FROM berlesek
  INNER JOIN autok
    ON berlesek.autoId = autok.id
GROUP BY autok.nev
ORDER BY mennyiseg DESC
LIMIT 5;

***
20. feladat
SELECT
  SUM((berlesek.mennyiseg * autok.ar) + berlesek.biztositas) AS `osszes-bevetel`,
  MAX(berlesek.biztositas / ((berlesek.mennyiseg * autok.ar) + berlesek.biztositas) * 100) AS `max-biztositas-arany`
FROM berlesek
  INNER JOIN autok
    ON berlesek.autoId = autok.id;
