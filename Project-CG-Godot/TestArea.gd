extends Node2D

@onready var Hand = $VBoxContainer/CardArea/Hand;
@onready var Mana = $VBoxContainer/PlayArea/VBoxContainer/Mana;
@onready var Player = $VBoxContainer/PlayArea/VBoxContainer/PlayerArea/Player;

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func Clicked(Target):
	for i in Hand.get_child_count():
		if(("PlayCard" in Hand.get_child(i).name)&&(Hand.get_child(i).active == true)):
			Hand.get_child(i).Activated(Target);

func ManaSpent(Spent):
	Mana.ChangeMana(Spent);

func DrawButton():
	Mana.ChangeMana(-1);
	Hand.Draw();

func getMana():
	return Mana.mana;

func EnemyAttack(DMG):
	Player.Damaged(DMG);

