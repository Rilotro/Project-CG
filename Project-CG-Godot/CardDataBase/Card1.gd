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

func Activated(Target, BDMG):
	print("activated1");
	get_node("/root").get_child(0).ManaSpent(mana);
	get_node("/root").get_child(0).GainCooldown(1, cooldown);
	Target.Damaged(10+BDMG);
	self.get_parent().get_parent().FreeCard(self);
