extends Control

var Cname = "Card4";
var mana = 2;
var cooldown = 30;
var text = "Play: Gain 1 strength, then deal 2 damage 2 times";

# Called when the node enters the scene tree for the first time.
func _ready():
	get_parent().assign_info(self);


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func Activated(Target):
	print("activated4");
	Target.Damaged(2);
	Target.Damaged(2);
	self.get_parent().get_parent().get_parent().get_parent().FreeCard(self);
