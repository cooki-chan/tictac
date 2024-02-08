extends ItemList


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	var stylebox_flat := StyleBoxFlat.new()
	stylebox_flat.border_width_top = 5
	
	add_stylebox_override("normal", stylebox_flat)


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
