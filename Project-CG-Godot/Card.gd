extends MarginContainer

var CDB;

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
func assign_info(CardDataBase):
	CDB = CardDataBase;
	if(CDB != null):
		$Bars/InfoBox/Cooldown/CenterContainer/Label.text = str(CDB.cooldown);
		$Bars/InfoBox/Mana/CenterContainer/Label.text = str(CDB.mana);
		$Bars/TextBox/TextArea/CenterContainer/Label.text = CDB.text;
	else:
		print("Error: Script Control Unit has not been added!");
