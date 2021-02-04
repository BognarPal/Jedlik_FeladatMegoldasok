-- A feladatok megoldására elkészített SQL parancsokat illessze be a feladat sorszáma után!


-- 8. feladat:
CREATE DATABASE konyvtarak
CHARACTER SET utf8
COLLATE utf8_hungarian_ci;

-- 10. feladat:
UPDATE megyek
  SET megyeNev = 'Budapest'
WHERE megyeNev = 'BP';

-- 11. feladat:
SELECT
  konyvtarak.konyvtarNev,
  konyvtarak.irsz
FROM konyvtarak
WHERE konyvtarak.konyvtarNev LIKE '%szakkönyvtár%';

-- 12. feladat:
SELECT
  konyvtarak.konyvtarNev,
  konyvtarak.irsz,
  konyvtarak.cim
FROM konyvtarak
WHERE konyvtarak.irsz LIKE '1%'
ORDER BY konyvtarak.irsz;

-- 13. feladat:
SELECT
  telepulesek.telepNev,
  COUNT(konyvtarak.id) AS konyvtarDarab
FROM konyvtarak
  INNER JOIN telepulesek
    ON konyvtarak.irsz = telepulesek.irsz
GROUP BY telepulesek.telepNev
HAVING COUNT(konyvtarak.id) >= 7;

-- 14. feladat:
SELECT
  megyek.megyeNev,
  COUNT(telepulesek.irsz) AS telepulesDarab
FROM telepulesek
  INNER JOIN megyek
    ON telepulesek.megyeId = megyek.id
WHERE telepulesek.irsz NOT LIKE '1%'
GROUP BY megyek.megyeNev
ORDER BY telepulesDarab DESC;

