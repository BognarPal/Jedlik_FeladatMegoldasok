from pybricks.parameters import Stop
from threading import Thread
import time
import random

from smallmotor import SmallMotor
from myrobot import MyRobot
from htcolor import HTColor


class Erc2020:
    def __init__(self, myRobot: MyRobot, rotator: SmallMotor, arm: SmallMotor, headColorSensor: HTColor):
        self.myRobot = myRobot
        self.rotator = rotator
        self.arm = arm
        self.headColorSensor = headColorSensor

    def run(self, color):
        self.rendrakas()
        self.goToPluszfeladat()
        self.pluszfeladat()
        self.goToPohar()
        self.poharForgatas()
        self.goToPingpong()
        self.szennyesKidobas()
        self.gotoCeruza()
        self.ceruzaFelvesz(color)
        self.ceruzaHazavisz()
        


    def stopAtPencil(self, color):
        self.myRobot.alignToNotWhite(speed = 200, whiteThreshold = 75)
        if (color == 'white'):
            self.myRobot.forwardAndStop(speed = -200, angle = 80)   #160-ról indultunk
            return
        if (color == 'black'):
            self.myRobot.forwardAndStop(speed = -200, angle = 430)   # 510-ről indulunk
            return

        self.myRobot.forwardAndStop(speed = -200, angle = 230)

        for i in range(0, 3):    
            time.sleep(0.1)
            if (self.headColorSensor.getColor() == color):
                self.myRobot.forwardAndStop(speed = 200, angle = 80)
                return
            if (i == 2):
                self.myRobot.forwardAndStop(speed = 100, angle = 10)
            else:
                self.myRobot.forwardAndStop(speed = -200, angle = 70)
        
        return

    def rendrakas(self):
        self.myRobot.alignToBlack(speed=300, blackThreshold=30)
        self.arm.moveTo(angle=25, speed=100, wait=False)
        self.myRobot.forwardAndStop(speed=250, angle=160)
        self.myRobot.forwardAndStop(speed=250, angle=-75)
        self.myRobot.turnLeftWithLeftMotor(speed=150)
        self.arm.moveTo(angle=0, speed=100, wait=False)
        self.myRobot.forwardAndStop(speed=150, angle=385)

    def goToPluszfeladat(self):
        self.myRobot.forwardAndStop(speed=400, angle=-225)
        self.myRobot.turnRight(speed=200)
        self.myRobot.forwardAndStop(speed=600, angle=585)
        self.myRobot.turnLeft(speed=200)

    def pluszfeladat(self):
        self.myRobot.alignToNotWhite(speed=-200, whiteThreshold=65)
        self.rotator.moveTo(angle=-420, speed=400, wait=False)
        self.myRobot.forward(speed=500, angle=400)
        self.myRobot.forwardAndStop(speed=200, angle=230) #esetleges gyorsítás
        self.arm.moveTo(angle=50, speed=200, wait=True) 
        self.arm.move()
        self.myRobot.forwardAndStop(speed=500, angle=200) #esetleges gyorsítás
        self.myRobot.turnLeftWithRightMotor(speed=200)
        self.rotator.moveTo(angle=0, speed=400, wait=False)
        #kicsit eltolhatja a falat, esetleg egy hangyapöcöknyi igazítás lehetne
        self.myRobot.forwardAndStop(speed=150, angle=300)
        self.arm.brake();
        self.arm.moveTo(angle=20, speed=100, wait=True)
        time.sleep(0.4)
        self.arm.moveTo(angle=0, speed=100, wait = True)

    def goToPohar(self):
        # goToPluszban van benne, majd ha biztosan jó, akor törölhető
        # self.myRobot.forwardAndStop(speed=400, angle=-225)
        # self.myRobot.turnRight(speed=200)
        # self.myRobot.forwardAndStop(speed=600, angle=500)
        # self.myRobot.turnLeft(speed=200)
        # self.myRobot.forward(speed=500, angle=1300)
        #
        self.myRobot.forwardAndStop(speed=200, angle=-250)
        self.myRobot.turnRightWithRightMotor(speed=200)
        self.myRobot.forwardAndStop(speed=400, angle=700 )
        #
        self.myRobot.alignToNotWhite(speed=200, whiteThreshold=65)
        self.myRobot.forwardAndStop(speed=300, angle=-350)
        self.myRobot.turnLeftWithLeftMotor(speed=250)

    def poharForgatas(self):
        self.rotator.moveTo(angle=-420, speed=400, wait=False)
        self.myRobot.forwardAndStop(speed=300, angle=780)

        # pohár megfogása
        self.arm.moveTo(angle=50, speed=100, wait=True)
        self.arm.move()

        self.myRobot.forwardAndStop(speed=-100, angle=35)
        self.rotator.moveTo(angle=0, speed=400, wait=True)

        # pohár elengedése
        self.arm.brake()
        self.arm.moveTo(angle=15, speed=100, wait=True)
        time.sleep(0.5)
        self.arm.moveTo(angle=0, speed=100, wait=True)

    def goToPingpong(self):
        self.myRobot.forwardAndStop(speed=-500, angle=600)
        self.myRobot.leftMotor.run_angle(300, 72, Stop.HOLD, False)
        self.myRobot.rightMotor.run_angle(300, -72, Stop.HOLD, True)
        self.myRobot.forwardAndStop(speed=-500, angle=1000)
        self.myRobot.leftMotor.run_angle(300, 72, Stop.HOLD, False)
        self.myRobot.rightMotor.run_angle(300, -72, Stop.HOLD, True)
        self.myRobot.forward(speed=-500, angle=600)
        self.myRobot.alignToNotWhite(speed=-200, whiteThreshold=65)
        self.myRobot.forwardAndStop(speed=200, angle=80)
        self.myRobot.turnRight(speed=200)

    def szennyesKidobas(self):
        self.myRobot.forwardAndStop(speed = 200, angle = 160) #150
        self.arm.moveTo(angle=60, speed=100, wait=True)
        self.arm.move()
        self.myRobot.forwardAndStop(speed = 200, angle = -300)
        self.myRobot.leftMotor.run_angle(300, -190, Stop.HOLD, False)
        self.myRobot.rightMotor.run_angle(300, 190, Stop.HOLD, True)
        self.myRobot.forwardAndStop(speed=500, angle=1700)
        self.myRobot.leftMotor.run_angle(300, 42, Stop.HOLD, False)
        self.myRobot.rightMotor.run_angle(300, -42, Stop.HOLD, True)
        self.myRobot.alignToNotWhite(speed=200, whiteThreshold=65)
        self.myRobot.forwardAndStop(speed=300, angle=-350)
        self.myRobot.turnLeftWithLeftMotor(speed=200)
        self.myRobot.forwardAndStop(speed=200, angle=480) #460    480
        self.arm.brake()
        self.arm.moveTo(angle=25, speed=100, wait=True)
        time.sleep(0.5)
        self.arm.moveTo(angle=0, speed=100, wait=True)

    def gotoCeruza(self):
        self.myRobot.forwardAndStop(speed = -200, angle = 600)
        self.myRobot.turnLeftWithLeftMotor(speed=250)
        self.myRobot.alignToNotWhite(speed= -200, whiteThreshold = 65)
        self.myRobot.forwardAndStop(speed = 500, angle = 670)
        self.myRobot.turnLeftWithLeftMotor(speed=250)
        self.myRobot.forward(speed=500, angle=1200)

    def ceruzaFelvesz(self, color):
        self.myRobot.alignToNotWhite(speed=200, whiteThreshold=65)
        self.stopAtPencil(color)
        self.myRobot.turnLeftWithLeftMotor(speed = 200)
        self.myRobot.forwardAndStop(speed = 200, angle = 220) #180 kezdetben

        self.arm.moveTo(angle=57, speed=100, wait=True)
        self.arm.move()
        time.sleep(0.5)

    def ceruzaHazavisz(self):
        self.myRobot.forwardAndStop(speed = -400, angle = 500)
        self.myRobot.turnLeft(speed = 200)

        self.myRobot.alignToNotWhite(speed=-200, whiteThreshold=65)
        self.myRobot.forwardAndStop(speed=500, angle=800)
        self.myRobot.leftMotor.run_angle(300, -74, Stop.HOLD, False)
        self.myRobot.rightMotor.run_angle(300, 74, Stop.HOLD, True)
        self.myRobot.forwardAndStop(speed = 500, angle = 660 ) #570
        self.myRobot.leftMotor.run_angle(300, 74, Stop.HOLD, False)
        self.myRobot.rightMotor.run_angle(300, -74, Stop.HOLD, True)
        self.myRobot.forwardAndStop(speed = 400, angle = 500 )
        self.myRobot.alignToBlack(speed=200, blackThreshold=20)

        self.myRobot.forwardAndStop(speed=-100, angle=100)
        self.arm.brake()  
        self.arm.moveTo(angle=0, speed=100, wait=True)
        self.myRobot.forwardAndStop(speed=200, angle=230)


    def veletlenSzin(self):
        number = random.randint(1,7)
        if (number == 1):
            return ['red', 'piros']
        if (number == 2):
            return ['green', 'zöld']
        if (number == 3):
            return ['blue', 'kék']
        if (number == 4):
            return ['yellow', 'sárga']
        if (number == 5):
            return ['white', 'fehér']
        if (number == 6):
            return ['black', 'fekete']
