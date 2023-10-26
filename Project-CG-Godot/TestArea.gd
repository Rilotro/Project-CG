extends Node2D


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func Clicked(Target):
	var Hand = $VBoxContainer/CardArea/Hand/CardArea/CardGrid;
	for i in Hand.get_child_count():
		if(("PlayCard" in Hand.get_child(i).name)&&(Hand.get_child(i).active == true)):
			Hand.get_child(i).Activated(Target);
