from pybricks.ev3devices import (ColorSensor)
from pybricks.parameters import Port, Color

class RGBColor:

    def __init__(self, port):
        self.sensor = ColorSensor(port)
        # print(self.sensor.rgb())

    def rgb(self):
        return self.sensor.rgb()


    def getColor(self):
        rgb = self.sensor.rgb()
        if rgb[2] > rgb[0] and rgb[2] > rgb[1]:
            return 'blue'
        elif rgb[1] > rgb[0] and rgb[1] > rgb[2]:
            return 'green'
        else:
            if rgb[0] == 0:
                return 'green'

            elif rgb[0] <= 5:
                if rgb[1] + rgb[2] == 0:
                    return 'red'
                else:
                    return 'yellow'

            elif rgb[0] <= 8:
                if rgb[1] + rgb[2] <= 2:
                    return 'red'
                else:
                    return 'yellow'

            elif rgb[0] <= 12:
                if rgb[1] + rgb[2] <= 4:
                    return 'red'
                else:
                    return 'yellow'

            elif rgb[0] <= 17:
                if rgb[1] + rgb[2] <= 8:
                    return 'red'
                else:
                    return 'yellow'

            elif rgb[0] <= 24:
                if rgb[1] + rgb[2] <= 13:
                    return 'red'
                else:
                    return 'yellow'

            elif rgb[0] <= 30:
                if rgb[1] + rgb[2] <= 20:
                    return 'red'
                else:
                    return 'yellow'

            elif rgb[0] <= 36:
                if rgb[1] + rgb[2] <= 25:
                    return 'red'
                else:
                    return 'yellow'                    
            
            elif rgb[0] <= 46:
                if rgb[1] + rgb[2] <= 30:
                    return 'red'
                else:
                    return 'yellow'                                        

            elif rgb[0] <= 60:
                if rgb[1] + rgb[2] <= 40:
                    return 'red'
                else:
                    return 'yellow'   

            elif rgb[0] <= 70:
                if rgb[1] + rgb[2] <= 50:
                    return 'red'
                else:
                    return 'yellow'   

            else:
                return yellow

    def getReflection(self):
        return self.sensor.reflection()

    def isBlackReflection(self):
        return self.getReflection() <= 20
    
    def isWhiteReflection(self):
        return self.getReflection() >= 70

    def isNotBlackReflection(self):
        return not self.isBlackReflection()
    
    def isNotWhiteReflection(self):
        return not self.isWhiteReflection()
