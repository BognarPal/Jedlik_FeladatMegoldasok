-- A feladatok megoldására elkészített SQL parancsokat illessze be a feladat sorszáma után!


-- 10. feladat:
CREATE DATABASE halozat
CHARACTER SET utf8
COLLATE utf8_hungarian_ci;

-- 12. feladat:
INSERT into megallok(id, nev) VALUES (198, 'Kőbányai garázs');

-- 13. feladat:
UPDATE jaratok SET elsoAjtos = false WHERE id = 20;

-- 14. feladat:
SELECT jaratszam
FROM jaratok
where elsoAjtos = TRUE;

-- 15. feladat:
SELECT nev
FROM megallok
WHERE nev LIKE '%sétány'
order BY nev;


-- 16. feladat:

SELECT
  halozat.sorszam,
  megallok.nev AS megallo
FROM halozat
  INNER JOIN megallok
    ON halozat.megallo = megallok.id
  INNER JOIN jaratok
    ON halozat.jarat = jaratok.id
WHERE jaratok.jaratSzam = 'CITY'
AND halozat.irany = 'A'
ORDER BY halozat.sorszam;

-- 17 feladat
SELECT
  megallok.nev AS megallo,
  COUNT(jaratok.id) AS jaratokSzama
FROM halozat
  INNER JOIN megallok
    ON halozat.megallo = megallok.id
  INNER JOIN jaratok
    ON halozat.jarat = jaratok.id
GROUP BY megallok.nev
HAVING COUNT(jaratok.id) >= 3;