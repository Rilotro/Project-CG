extends Control

var entered = false;

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if((Input.is_action_just_pressed("Click"))&&(entered == true)):
		get_node("/root").get_child(0).Clicked(self);


func _on_area_2d_mouse_entered():
	entered = true;


func _on_area_2d_mouse_exited():
	entered = false;

func Damaged(DMG):
	$VBoxContainer/HealthContainer.ReceiveDMG(DMG);
	EnemyTurn();

func EnemyTurn():
	get_node("/root").get_child(0).EnemyAttack(10);
