extends Control

var Cname = "Card2";
var mana = 0;
var cooldown = 60;
var text = "Play: Deal 3 damage and draw 1";

# Called when the node enters the scene tree for the first time.
func _ready():
	get_parent().assign_info(self);


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass