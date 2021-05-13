A feladatok megoldására elkészített SQL parancsokat illessze be a feladat sorszáma után!
***
14. feladat
CREATE DATABASE kettlebell
CHARACTER SET utf8
COLLATE utf8_hungarian_ci;
***
16. feladat
SELECT COUNT(id) AS `20_kilos_felszerelesek_szama`
FROM felszereles
WHERE suly = 20

***
17. feladat
SELECT
  haviberlet.honap,
  haviberlet.ar
FROM haviberlet
  INNER JOIN vendeg
    ON haviberlet.vendegId = vendeg.id
WHERE vendeg.nev = 'Tóth Levente'
***
18. feladat
UPDATE kategoria 
  set nev = 'Fém kettlebell'
where id = 1

***
19. feladat
SELECT
  felszereles.nev,
  felszereles.suly,
  SUM(kolcsonzes.idotartam) AS szumma_nap
FROM kolcsonzes
  INNER JOIN felszereles
    ON kolcsonzes.felszerelesId = felszereles.id
GROUP BY felszereles.nev,
         felszereles.suly
ORDER BY szumma_nap DESC
LIMIT 5
***
20. feladat
SELECT
  vendeg.nev,
  COUNT(kolcsonzes.id) AS visszahozando_db
FROM kolcsonzes
  INNER JOIN vendeg
    ON kolcsonzes.vendegId = vendeg.id
WHERE kolcsonzes.visszahozta = false
GROUP BY vendeg.nev
ORDER BY visszahozando_db DESC
limit 1


