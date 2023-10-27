extends Control

var Cname = "Card1";
var mana = 1;
var cooldown = 30;
var text = "Play: Deal 10 damage"

func _ready():
	get_parent().assign_info(self);


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func Activated(Target):
	print("activated1");
	Target.Damaged(10);
	self.get_parent().get_parent().get_parent().get_parent().FreeCard(self);
