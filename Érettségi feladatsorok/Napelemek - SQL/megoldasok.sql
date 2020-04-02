-- A feladatok megoldására elkészített SQL parancsokat illessze be a feladat sorszáma után!


-- 1. feladat:
CREATE DATABASE napsutes
	CHARACTER SET utf8
	COLLATE utf8_hungarian_ci;

-- 3. feladat:
update regiok
   set regioNev = "Észak-Írország"
where regioNev = "Észak Írország";

-- 4. feladat:
SELECT
  COUNT(meresek.id) AS rekordszam,
  AVG(meresek.perc) AS atlag
FROM meresek;

-- 5. feladat:
SELECT
  meresek.ev,
  SUM(meresek.perc / 60) AS orak
FROM meresek
  INNER JOIN regiok
    ON meresek.regioId = regiok.id
WHERE regiok.regioNev = 'Anglia'
AND meresek.ev BETWEEN 1990 AND 2000
GROUP BY meresek.ev
ORDER BY meresek.ev DESC;

-- 6. feladat:
SELECT
  meresek.ev,
  meresek.perc,
  regiok.regioNev AS terulet
FROM meresek
  INNER JOIN regiok
    ON meresek.regioId = regiok.id
WHERE meresek.ho = 2
AND meresek.perc > 6000
ORDER BY meresek.perc DESC