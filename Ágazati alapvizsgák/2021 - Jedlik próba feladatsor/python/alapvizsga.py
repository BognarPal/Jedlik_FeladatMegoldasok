def feladat1():
    print('1. feladat: Kisebb-nagyobb meghatározása')
    a = int(input('Kérem az első számot: '))
    b = int(input('Kérem az második számot: '))
    if a > b:
        print('A nagyobb szám {0}, a kisebb {1}.'.format(a, b))
    elif b > a:
        print('A nagyobb szám {0}, a kisebb {1}.'.format(b, a))
    else:
        print('A két szám egyenlő.')

def szokoev(ev):
    if (ev % 400 == 0):
        return True
    if (ev % 4 == 0 and ev % 100 != 0):
        return True
    return False

def feladat2():
    print('2. feladat: Szökőév listázó')
    ev1 = int(input('Kérem az egyik évszámot: '))
    ev2 = int(input('Kérem az másik évszámot: '))
    if (ev1 < ev2):
        tol = ev1
        ig = ev2
    else:
        tol = ev2
        ig = ev1

    szokoevek = []
    for ev in range(tol, ig + 1):
        if szokoev(ev):
            szokoevek.append(ev)
    
    if (len(szokoevek) == 0):
        print('Nincs szökőév a megadott tartományban!')
    else:
        str = 'Szökőévek: ' 
        for ev in szokoevek:
           str += '{0}; '.format(ev)
        print(str[:-2])

class Épület:
    nev = ''
    varos = ''
    orszag = ''
    magassag = 0.0
    emelet = 0
    epult = 0

    def __init__(self, sor):
        adatok = sor.split(';')
        self.nev = adatok[0]
        self.varos = adatok[1]
        self.orszag = adatok[2]
        self.magassag = float(adatok[3].replace(',', '.'))
        self.emelet = int(adatok[4])
        self.epult = int(adatok[5])

def feladat3():
    epuletek = []
    f = open('legmagasabb.txt', encoding='UTF-8')
    f.readline()
    sor = f.readline()
    while (sor):
        epuletek.append(Épület(sor))
        sor = f.readline()
    f.close()
    print('3.2 feladat: Épületek száma: {0} db'.format(len(epuletek)))

    osszEmelet = 0
    for e in epuletek:
        osszEmelet += e.emelet
    print('3.3 feladat: Emeletek összege: {0}'.format(osszEmelet))

    legmagasabbEpulet = epuletek[0]
    for e in epuletek:
        if (legmagasabbEpulet.magassag < e.magassag):
            legmagasabbEpulet = e
    print('3.4 feladat: A legmagasabb épület adatai')
    print('\tNév: {0}'.format(legmagasabbEpulet.nev))
    print('\tVáros: {0}'.format(legmagasabbEpulet.varos))
    print('\tOrszág: {0}'.format(legmagasabbEpulet.orszag))
    print('\tMagasság: {0}'.format(legmagasabbEpulet.magassag))
    print('\tEmeletek száma: {0}'.format(legmagasabbEpulet.emelet))
    print('\tÉpítés éve: {0}'.format(legmagasabbEpulet.epult))

    i = 0
    while i < len(epuletek) and epuletek[i].orszag != 'Olaszország':
        i += 1
    if i < len(epuletek):
        print('3.5 feladat: Van olasz épület az adatok között!')
    else:
        print('3.5 feladat: Nincs olasz épület az adatok között!')

feladat1()
feladat2()
feladat3()