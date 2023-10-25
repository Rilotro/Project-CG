extends MarginContainer

var currentHP;
@export var MAXHP = 100;

# Called when the node enters the scene tree for the first time.
func _ready():
	currentHP = MAXHP;
	$HealthText.ChangeHP(currentHP, MAXHP);
	$HealthBar.ChangeBar(currentHP, MAXHP);


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if(Input.is_action_just_pressed("TestCards")):
		ReceiveDMG(10);

func ReceiveDMG(DMG):
	currentHP -= DMG;
	if(currentHP > MAXHP):
		currentHP = MAXHP;
	elif(currentHP < 0):
		currentHP = 0;
	$HealthText.ChangeHP(currentHP, MAXHP);
	$HealthBar.ChangeBar(currentHP, MAXHP);
