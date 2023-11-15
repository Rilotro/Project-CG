extends Node2D

@onready var Hand = $VBoxContainer/CardArea/Hand;
@onready var Mana = $VBoxContainer/PlayArea/VBoxContainer/Mana;
@onready var Player = $VBoxContainer/PlayArea/VBoxContainer/PlayerArea/Player;
@onready var Enemy = $VBoxContainer/PlayArea/VBoxContainer/EnemyArea/Enemy;
@onready var CBar = $VBoxContainer/HBoxContainer/CooldownBar;

var RSTime = 0;
var RS = false;

# Called when the node enters the scene tree for the first time.
func _ready():
	CBar.CreateEvent(1, 0, "PlayerEvent", Color(0, 1, 0, 1));
	CBar.CreateEvent(2, 1, "EnemyEvent", Color(1, 0, 0, 1));
	Hand.RoundStart();


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if(RSTime > 0):
		RSTime -= delta;
	elif(RS == true):
		RS = false;
		CBar.RoundSystem();

func Clicked(Target):
	for i in Hand.get_child_count():
		if(("PlayCard" in Hand.get_child(i).name)&&(Hand.get_child(i).active == true)):
			Hand.get_child(i).Activated(Target, Player.strength);

func ManaSpent(Spent):
	Mana.ChangeMana(Spent);

func DrawButton():
	Hand.RoundEnd();
	RSTime = 2;
	RS = true;
	#Enemy.RoundStart();

func getMana():
	return Mana.mana;

func EnemyAttack(DMG):
	Player.Damaged(DMG);

func EnemyRoundEnd():
	CBar.GainCooldown(2, 60);
	RSTime = 2;
	RS = true;
	#Hand.RoundStart();

func Draw():
	Hand.Draw();

func GainCooldown(id, time):
	CBar.GainCooldown(id, time);

func PlayerGainStrength(Gain):
	Player.GainStrength(Gain);

func RoundStart(id):
	if(id == 1):
		Hand.RoundStart();
	elif(id == 2):
		Enemy.RoundStart();

