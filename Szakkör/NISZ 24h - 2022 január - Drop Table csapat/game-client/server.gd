extends Node


signal connected()
signal disconnected()
signal error()
signal received(dict)


onready var socket := WebSocketClient.new()


func send(packet: Dictionary):
	socket.get_peer(1).put_packet(JSON.print(packet).to_utf8())


func _ready():
	socket.connect("connection_established", self, "_connected")
	socket.connect("connection_closed", self, "_disconnected")
	socket.connect("connection_error", self, "_error")
	socket.connect("data_received", self, "_received")
	socket.connect_to_url("ws://localhost:3000")
	socket.get_peer(1).set_write_mode(WebSocketPeer.WRITE_MODE_TEXT)


func _process(_delta):
	socket.poll()


func _connected(_protocol):
	emit_signal("connected")


func _disconnected():
	emit_signal("disconnected")


func _error():
	emit_signal("error")


func _received():
	var json = socket.get_peer(1).get_packet().get_string_from_utf8()
	var dict = JSON.parse(json).result
	emit_signal("received", dict)
