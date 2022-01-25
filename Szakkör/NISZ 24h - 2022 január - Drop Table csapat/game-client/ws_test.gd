extends CanvasLayer

var client = null


func _process(_delta):
	if client:
		client.poll()
		if client.get_peer(1).is_connected_to_host():
			client.get_peer(1).put_packet("hello from godot".to_utf8())


func _connection_established(protocol):
	putlog("connection established (%s)" % protocol)


func _connection_closed():
	putlog("connection closed")


func _connection_error():
	putlog("connection error")


func _data_received():
	putlog("data received: %s" % client.get_peer(1).get_packet())


func putlog(msg):
	$Center/Panel/Box/Log.add_item(msg)


func _on_Button_pressed():
	client = WebSocketClient.new()
	
	client.connect("connection_established", self, "_connection_established")
	client.connect("connection_closed", self, "_connection_closed")
	client.connect("connection_error", self, "_connection_error")
	client.connect("data_received", self, "_data_received")
	
	client.connect_to_url($Center/Panel/Box/Address.text)
	client.get_peer(1).set_write_mode(WebSocketPeer.WRITE_MODE_TEXT)
