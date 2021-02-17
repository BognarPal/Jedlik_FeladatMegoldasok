from pybricks.ev3devices import (Motor)
from pybricks.parameters import Stop, Direction

from threading import Thread
import sys

class SmallMotor:

    def __init__(self, ev3, port):
        self.ev3 = ev3
        self.motor = Motor(port, Direction.COUNTERCLOCKWISE)
        self.motor.reset_angle(0)

    def reset(self):
        self.motor.run_until_stalled(100)
        self.motor.run_angle(800, -300)
        self.motor.reset_angle(0)

    def moveTo(self, angle, speed = 800, wait = False):
        print(self.motor.angle())
        self.motor.run_target(speed, angle, Stop.HOLD, wait)
        print(self.motor.angle())
    
    def move(self, speed = 20):
        self.motor.run(speed)

    def brake(self):
        self.motor.brake()