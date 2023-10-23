extends MarginContainer

@export var CDB = [];
@export var CardCount = 4;
@export var Card = load("res://Card.tscn");
var rng = RandomNumberGenerator.new();

func _ready():
	for i in CardCount:
		CDB.append(load(str("res://CardDataBase/Card", i+1, ".tscn")));


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if(Input.is_action_just_pressed("TestCards")):
		var tempCard = Card.instantiate();
		add_child(tempCard);
		tempCard.add_child(CDB[rng.randi_range(0, 3)].instantiate());
