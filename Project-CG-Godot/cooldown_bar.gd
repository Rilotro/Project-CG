extends Control

@export var Event:PackedScene;

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func RoundSystem():
	var closest = 1;
	var sCooldown = $MarginContainer.get_child(1).time;
	for i in $MarginContainer.get_child_count()-2:
		if($MarginContainer.get_child(i+2).time < sCooldown):
			sCooldown = $MarginContainer.get_child(i+2).time;
			closest = i+2;
	for i in $MarginContainer.get_child_count()-1:
		$MarginContainer.get_child(i+1).Move(sCooldown);
	$MarginContainer.get_child(closest).RoundStart();

func GainCooldown(id, time):
	for i in $MarginContainer.get_child_count()-1:
		if($MarginContainer.get_child(i+1).id == id):
			$MarginContainer.get_child(i+1).Move(-time);

func CreateEvent(id, time, name, color):
	var tempEvent = Event.instantiate();
	$MarginContainer.add_child(tempEvent);
	tempEvent.id = id;
	tempEvent.name = name;
	tempEvent.get_node("Sprite2D").modulate = color;
	tempEvent.Move(-time);
