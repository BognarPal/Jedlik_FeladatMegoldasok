from versenyzo import Versenyzo

versenyzok: list[Versenyzo] = []

f = open('Selejtezo2012.txt', 'r', encoding='utf-8')
f.readline()
for sor in f:
    versenyzok.append( Versenyzo(sor) )
f.close()

print(f'5. feladat: Versenyzők száma a selejtezőben: {len(versenyzok)} fő')

db = 0
for v in versenyzok:
    # if v.dobasok[0] > 78 or v.dobasok[1] > 78:
    if v.dobasok[2] == -2:
        db += 1

print(f'6. feladat: 78,00 méter feletti eredménnyel továbbjutott: {db} fő')

legjobb: Versenyzo = versenyzok[0]
for v in versenyzok[1:]:
    if legjobb.Eredmeny() < v.Eredmeny():
        legjobb = v

print('9. feladat: A selejtező nyertese:')
print(f'\tNév: {legjobb.nev}')
print(f'\tCsoport: {legjobb.csoport}')
print(f'\tNemzet: {legjobb.Nemzet()}')
print(f'\tNemzet kód: {legjobb.Kod()}')
print(f'\tSorozat: {legjobb.sorozat}')
print(f'\tEredmény: {legjobb.Eredmeny()}')

f = open('Dontos2012.txt', 'w', encoding='utf-8')
f.write('Helyezés;Név;Csoport;Nemzet;NemzetKód;Sorozat;Eredmény\n')
for i in range(1, 13):
    legjobb: Versenyzo = versenyzok[0]
    for v in versenyzok[1:]:
        if legjobb.Eredmeny() < v.Eredmeny():
            legjobb = v

    f.write(f'{i};{legjobb.nev};{legjobb.csoport};{legjobb.Nemzet()};{legjobb.Kod()};{legjobb.sorozat};{legjobb.Eredmeny()}\n')

    versenyzok.remove(legjobb)
f.close()