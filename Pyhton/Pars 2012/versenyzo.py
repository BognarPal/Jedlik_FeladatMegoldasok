class Versenyzo:

    def __init__(self, sor: str):
        # sor = "Pars Krisztián;B;Magyarország (HUN);77,11;79,37;-"

        adatok: list[str] = sor.strip().split(';')  # adatok = ['Pars Krisztián', 'B', 'Magyarország (HUN)', '77,11', '79,37', '-']
        self.nev = adatok[0]
        self.csoport = adatok[1]
        self.NemzetEsKod = adatok[2]
        self.sorozat = f'{adatok[3]};{adatok[4]};{adatok[5]}'
        self.dobasok = []

        for i in range(3, 6):
            if adatok[i] == 'X':
                self.dobasok.append(-1)
            elif adatok[i] == '-':
                self.dobasok.append(-2)
            else:
                self.dobasok.append( float(adatok[i].replace(',', '.')) )
        
        #dobások lista: [77.11, 79.37, -2]
        
    def Eredmeny(self) -> float:
        eredmeny = self.dobasok[0]
        for d in self.dobasok[1:]:
            if d > eredmeny:
                eredmeny = d
        
        return eredmeny
    
    def Kod(self):
        # Egyesült Államok (USA)
        return self.NemzetEsKod[-4:-1]
    
    def Nemzet(self):
        # Egyesült Államok (USA)
        return self.NemzetEsKod[0:-6]