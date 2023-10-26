extends MarginContainer

var CDB;
var entered = false;
var active = false;

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if((Input.is_action_just_pressed("Click"))&&(entered == true)):
		activate();
	
func assign_info(CardDataBase):
	CDB = CardDataBase;
	if(CDB != null):
		$Bars/InfoBox/Cooldown/CenterContainer/Label.text = str(CDB.cooldown);
		$Bars/InfoBox/Mana/CenterContainer/Label.text = str(CDB.mana);
		$Bars/TextBox/TextArea/CenterContainer/Label.text = CDB.text;
	else:
		print("Error: Script Control Unit has not been added!");


func _on_area_2d_mouse_entered():
	entered = true;


func _on_area_2d_mouse_exited():
	entered = false;

func activate():
	for i in self.get_parent().get_child_count():
		if(("PlayCard" in self.get_parent().get_child(i).name) && (get_parent().get_child(i).active == true)):
			get_parent().get_child(i).deactivate();

	$Border.modulate = Color(0.1, 0.25, 1);
	active = true;

func deactivate():
	$Border.modulate = Color(1, 1, 1);
	active = false;

func Activated(Target):
	$Control.Activated(Target);
