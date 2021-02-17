#!/usr/bin/env pybricks-micropython
from pybricks.hubs import EV3Brick
from pybricks.ev3devices import (Motor, TouchSensor, ColorSensor,
                                 InfraredSensor, UltrasonicSensor, GyroSensor)
from pybricks.parameters import Port, Stop, Direction, Button, Color
from pybricks.tools import wait, StopWatch, DataLog
from pybricks.robotics import DriveBase
from pybricks.media.ev3dev import SoundFile, ImageFile
from pybricks.iodevices import Ev3devSensor

import time
from threading import Thread

from enums import HangMagassag
from myrobot import MyRobot
from smallmotor import SmallMotor
from rgbColor import RGBColor
from htcolor import HTColor
from erc2020 import Erc2020


# Create your objects here.
ev3 = EV3Brick()
rotator = SmallMotor(ev3, Port.B)
arm = SmallMotor(ev3, Port.C)
myRobot = MyRobot(ev3, Port.A, Port.D, Port.S1, Port.S4)
headColorSensor = HTColor(Port.S3)
print(headColorSensor.rgbw())

# gyro = GyroSensor(Port.S2)

ev3.speaker.play_file("sounds/ready.wav")
comp = Erc2020(myRobot, rotator, arm, headColorSensor)
szin = comp.veletlenSzin()
ev3.screen.draw_text(40, 50, szin[1])

while Button.DOWN not in ev3.buttons.pressed():
    pass

comp.run(szin[0])