-- A feladatok megoldására elkészített SQL parancsokat illessze be a feladat sorszáma utáni üres sorba!
-- 15. feladat:
  insert into gp (id, nev, helyszin)
  values('1920.05.01', 'Belga', 'Belgium');

-- ***
-- 16. feladat:
update gp
   set nev = 'Német'
where nev = 'Némte';

-- ***
-- 17. feladat:
SELECT
  pilota.nev,
  pilota.nemzet,
  YEAR(pilota.szuldat) AS `szuletesi ev`
FROM pilota
WHERE pilota.nev LIKE '% hill'
ORDER BY `szuletesi ev`;

-- ***
-- 18. feladat:
SELECT DISTINCT
  pilota.nev
FROM eredmeny
  INNER JOIN pilota
    ON eredmeny.pilotaId = pilota.id
WHERE eredmeny.helyezes = 1
AND pilota.nev LIKE 'p%';

-- ***
-- 19. feladat:
SELECT
  YEAR(gp.id) - YEAR(pilota.szuldat) AS kor  
FROM eredmeny
  INNER JOIN pilota
    ON eredmeny.pilotaId = pilota.id
  INNER JOIN gp
    ON eredmeny.gpId = gp.id
WHERE pilota.nev = 'Juan-Manuel Fangio'
ORDER BY gp.id
limit 1;

-- ***
-- 20. feladat:
SELECT
  eredmeny.hiba
FROM eredmeny
WHERE eredmeny.csapat LIKE '%Ferrari%'
AND eredmeny.hiba IS NOT NULL
GROUP BY eredmeny.hiba
ORDER BY COUNT(eredmeny.id) DESC
Limit 3;

-- ***
-- 21. feladat:
Alter table eredmeny 
    add privat bit;

/*update eredmeny
    set privat = 1 
where csapat is null;

update eredmeny
    set privat = 0
where csapat is not null;*/

update eredmeny
    set privat = case when csapat is null then 1 else 0 END;



-- ***
-- 22. feladat:

Select distinct helyszin from gp 
where helyszin not in  (
                            SELECT
                              gp.helyszin
                            FROM gp
                            WHERE gp.id <= ( SELECT MIN(gp.id) AS datum FROM gp WHERE gp.nev = 'Magyar'  )
                       );


-- ***
-- 23. feladat:
SELECT
  eredmeny.helyezes,
  pilota.nev,
  eredmeny.csapat
FROM eredmeny
  INNER JOIN gp
    ON eredmeny.gpId = gp.id
  INNER JOIN pilota
    ON eredmeny.pilotaId = pilota.id
WHERE gp.helyszin = 'Monaco'
AND YEAR(gp.id) = 1958
AND eredmeny.helyezes IN (1, 2, 3);
-- ***
