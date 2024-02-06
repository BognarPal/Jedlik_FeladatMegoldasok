from dolgozo import Dolgozo

dolgozok: list[Dolgozo] = []

def main():
    beolvasas()
    dolgozok_szama()
    nev = nev_bekerese()
    kereses(nev)
    legjobban_kereso_dolgozo()
    atlagnal_rosszabbul_keresok()
    fizetes_emeles()

def beolvasas():
    f = open('fizetesek.csv', 'r', encoding='UTF-8')
    f.readline()
    for sor in f:
        dolgozok.append(Dolgozo(sor))
    f.close()

def dolgozok_szama():
    print(f'2. feladat: A cégnél {len(dolgozok)} munkavállaló dolgozik.')
    
def nev_bekerese() -> str:
    return input('3. feladat: Dolgozó neve: ')

def kereses(nev: str):
    i = 0
    while i < len(dolgozok) and dolgozok[i].nev != nev:
        i += 1
    if i < len(dolgozok):
        print(f'4. feladat: A dolgozó fizetése: {dolgozok[i].fizetes} Ft.')
    else:
        print('4. feladat: Ilyen nevű dolgozó nincs a cégnél.')


def legjobban_kereso_dolgozo():
    max: Dolgozo = dolgozok[0]
    for d in dolgozok[1:]:
        if d.fizetes > max.fizetes:
            max = d
    print(f'5. feladat: A legjobban kereső dolgozó: {max.nev}, fizetése: {max.fizetes} Ft.')

def atlagnal_rosszabbul_keresok():
    osszeg = 0
    for d in dolgozok:
        osszeg += d.fizetes
    atlag = osszeg / len(dolgozok)

    print(f'6. feladat: Az átlagnál ({atlag:.0f} Ft) kevesebbet kereső dolgozók:')
    for d in dolgozok:
        if d.fizetes < atlag:
            print(f'\t{d.nev}')

def fizetes_emeles():
    f = open('fizetesek_uj.csv', 'w', encoding='utf-8')
    f.write("Név;Új fizetése\n")
    for d in dolgozok:
        f.write(f'{d.nev};{d.fizetes * 1.2:.0f}\n')
    f.close()

main()