extends MeshInstance


const CITY = preload("res://City.tscn")


func _ready():
	var file = File.new()
	file.open("res://cities.json", File.READ)
	var cities = JSON.parse(file.get_as_text()).result
	
	for city in cities:
		var transform = Transform.IDENTITY
		transform.rotated()
		
		var instance = CITY.instance()
		instance.rotation_degrees = Vector3(city.coords[1], city.coords[0], 0)
		call_deferred("add_child", instance)
