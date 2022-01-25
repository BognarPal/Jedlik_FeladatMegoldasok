extends ItemList


func _ready():
	Server.connect("connected", self, "_connected")
	Server.connect("disconnected", self, "_disconnected")
	Server.connect("error", self, "_error")
	Server.connect("received", self, "_received")
	
	yield(get_tree().create_timer(1.0), "timeout")
	Server.send({"hello": "world"})


func _connected():
	add_item("Connected")


func _disconnected():
	add_item("Disonnected")


func _error():
	add_item("Error")


func _received(dict):
	add_item("Received: %s" % dict)
