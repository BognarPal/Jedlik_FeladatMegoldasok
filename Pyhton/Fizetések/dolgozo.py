class Dolgozo:

    def __init__(self, sor: str) -> None:
        adatok = sor.strip().split(';')
        self.nev = adatok[0]
        self.fizetes = int(adatok[1])
    