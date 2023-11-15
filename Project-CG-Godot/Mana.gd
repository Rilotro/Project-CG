extends Node2D

@export var mana = 0;

# Called when the node enters the scene tree for the first time.
func _ready():
	Resize();


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func ChangeMana(Mana):
	mana -= Mana;
	if(mana < 0):
		mana = 0;
	Resize();

func Resize():
	if(mana >= 10):
		get_child(1).scale = Vector2(150, 25);
	else:
		get_child(1).scale = Vector2(150*mana/10, 25);
	get_child(1).position = Vector2((get_child(1).scale.x - 150)/2, 0);
	$MarginContainer/CenterContainer/Label.text = str(mana);
