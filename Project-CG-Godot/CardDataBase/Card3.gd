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

func Activated(Target):
	print("activated3");
	get_node("/root").get_child(0).ManaSpent(mana);
	Target.Damaged(7);
	Target.Damaged(7);
	Target.Damaged(7);
	self.get_parent().get_parent().FreeCard(self);
