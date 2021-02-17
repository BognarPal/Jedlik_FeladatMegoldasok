from pybricks.iodevices import Ev3devSensor

class HTColor(Ev3devSensor):
    _ev3dev_driver_name = 'ht-nxt-color-v2'
    _MODE_COLOR = 0
    _MODE_RGBW = 1
    def __init__(self, port):
        super().__init__(port)
        self.path = '/sys/class/lego-sensor/sensor' + str(self.sensor_index)

    def rgbw(self):
        return self.read('RAW')


    def getColor(self):
        rgb = self.rgbw()
        if (rgb[0] > rgb[1] and rgb[0] > rgb[2]):
            return 'red'
        if (rgb[2] / rgb[1] > 0.70):
            return 'blue'
        if (rgb[2] / rgb[0] < 0.4):
            return 'yellow'
        if (rgb[3] > 400 and rgb[1] / rgb[0] > 1.7 ):
            return 'green'
        if (rgb[3] > 400):
            return 'white'
        if (rgb[1] / rgb[0] > 1.8 ):
            return 'green'
        return 'black'


    def get_modes(self):
        # The path of the modes file.
        modes_path = self.path + '/modes'

        # Open the modes file.
        with open(modes_path, 'r') as m:

            # Read the contents.
            contents = m.read()

            # Strip the newline symbol, and split at every space symbol.
            return contents.strip().split(' ')