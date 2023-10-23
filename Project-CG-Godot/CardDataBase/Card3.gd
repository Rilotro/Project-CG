extends Control

var Cname = "Card3";
var mana = 3;
var cooldown = 60;
var text = "Play: Deal 7 damage 3 times";

# Called when the node enters the scene tree for the first time.
func _ready():
	get_parent().assign_info(self);


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
