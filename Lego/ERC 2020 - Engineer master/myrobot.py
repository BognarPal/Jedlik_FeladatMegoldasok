from pybricks.ev3devices import (Motor)
from pybricks.parameters import Stop, Direction, Button
from threading import Thread
import time

from rgbColor import RGBColor

class MyRobot:

    def __init__(self, ev3, leftMotorPort, rightMotorPort, leftColorSensorPort, rightColorSensorPort):
        self.ev3 = ev3
        self.leftMotor = Motor(leftMotorPort)
        self.rightMotor = Motor(rightMotorPort)
        self.rightMotor.control.limits(800, 500, 100)
        self.leftMotor.control.limits(800, 500, 100)

        if (leftColorSensorPort != None):
            self.leftColorSensor = RGBColor(leftColorSensorPort)
            print(self.leftColorSensor.getReflection())
        if (rightColorSensorPort != None):
            self.rightColorSensor = RGBColor(rightColorSensorPort)
            print(self.rightColorSensor.getReflection())

        ev3.speaker.set_speech_options('hu', 'f2', 200)


    def beep(self, hangmagassag = 600, duration = 100):
        self.ev3.speaker.beep(frequency=hangmagassag, duration=duration)
   
    def forwardAndStop(self, speed, angle):
        self.leftMotor.run_angle(speed, angle, Stop.HOLD, False)
        self.rightMotor.run_angle(speed, angle, Stop.HOLD, True)

    def forward(self, speed, angle):
        leftAngle = self.leftMotor.angle()
        rightAngle = self.rightMotor.angle()        
        self.leftMotor.run(speed)
        self.rightMotor.run(speed)
        if (speed > 0):
            while (((self.leftMotor.angle() - leftAngle) + (self.rightMotor.angle() - rightAngle)) / 2 < angle):
                pass
        else:
            while (((leftAngle - self.leftMotor.angle()) + (rightAngle - self.rightMotor.angle())) / 2 < angle):
                pass

    def brake(self):
        self.leftMotor.brake()
        self.rightMotor.brake()

    def leftAngle(self):
        return self.leftMotor.angle()

    def rightAngle(self):
        return self.rightMotor.angle()
    
    def resetAngle(self):
        self.leftMotor.reset_angle(0)
        self.rightMotor.reset_angle(0)

    def turnLeft(self, speed):
        self.leftMotor.run_angle(speed, -148, Stop.HOLD, False)
        self.rightMotor.run_angle(speed, 148, Stop.HOLD, True)
        

    def turnRight(self, speed):
        self.leftMotor.run_angle(speed, 148, Stop.HOLD, False)
        self.rightMotor.run_angle(speed, -148, Stop.HOLD, True)

    def turnLeftWithRightMotor(self, speed):
        self.rightMotor.run_angle(speed, 296, Stop.HOLD, True)

    def turnLeftWithLeftMotor(self, speed):
        self.leftMotor.run_angle(speed, -296, Stop.HOLD, True)

    def turnRightWithRightMotor(self, speed):
        self.rightMotor.run_angle(speed, -296, Stop.HOLD, True)

    def turnRightWithLeftMotor(self, speed):
        self.leftMotor.run_angle(speed, 296, Stop.HOLD, True)

    def forwardWhile(self, speed, leftMotorConditionFunc, rightMotorConditionFunc):
        self.leftMotor.run(speed)
        self.rightMotor.run(speed)
        while (leftMotorConditionFunc() and rightMotorConditionFunc()):
            pass
        if (not leftMotorConditionFunc()):
            self.leftMotor.brake()
            while (rightMotorConditionFunc()):
                pass
            self.rightMotor.brake()
        else:
            self.rightMotor.brake()
            while (leftMotorConditionFunc()):
                pass
            self.leftMotor.brake()

    def say(self, text):
        return Thread(target=self.ev3.speaker.say(text))
        
    def alignToWhite(self, speed, whiteThreshold):
        self.leftMotor.run(speed)
        self.rightMotor.run(speed)
        time.sleep(0.1)
        while (self.leftMotor.speed() != 0 or self.rightMotor.speed() != 0):
            if (self.leftColorSensor.getReflection() >  whiteThreshold):
                self.leftMotor.brake()
            if (self.rightColorSensor.getReflection() >  whiteThreshold):
                self.rightMotor.brake()

    def alignToBlack(self, speed, blackThreshold):
        self.leftMotor.run(speed)
        self.rightMotor.run(speed)
        time.sleep(0.1)
        while (self.leftMotor.speed() != 0 or self.rightMotor.speed() != 0):
            if (self.leftColorSensor.getReflection() <  blackThreshold):
                self.leftMotor.brake()
            if (self.rightColorSensor.getReflection() <  blackThreshold):
                self.rightMotor.brake()

    def alignToNotWhite(self, speed, whiteThreshold):
        self.leftMotor.run(speed)
        self.rightMotor.run(speed)
        time.sleep(0.1)
        while (self.leftMotor.speed() != 0 or self.rightMotor.speed() != 0):
            leftReflection = self.leftColorSensor.getReflection()
            rightReflection = self.rightColorSensor.getReflection()
            if ( leftReflection <  whiteThreshold):
                self.leftMotor.brake()
            if (rightReflection <  whiteThreshold):
                self.rightMotor.brake()
            # print("r = {0}, l = {1}".format(rightReflection, leftReflection))

    def measureColorSensors(self):
        while Button.CENTER not in self.ev3.buttons.pressed():
            print('left reflection: {0}, right reflection: {1}'.format(self.leftColorSensor.getReflection(), self.rightColorSensor.getReflection()))
            time.sleep(0.2)  
