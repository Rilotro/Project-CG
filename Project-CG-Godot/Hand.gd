extends MarginContainer

@export var CDB = [];
@export var CardCount = 4;
@export var Card = load("res://Card.tscn");
@export var separation: PackedScene;
var rng = RandomNumberGenerator.new();

func _ready():
	for i in CardCount:
		CDB.append(load(str("res://CardDataBase/Card", i+1, ".tscn")));


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if(Input.is_action_just_pressed("TestCards")):
		var tempCard = Card.instantiate();
		tempCard.add_child(CDB[rng.randi_range(0, 3)].instantiate());
		$CardArea/CardGrid.add_child(tempCard);
		
		match $CardArea/CardGrid.get_child_count()/2:
			1:
				$CardArea/CardGrid/LeftGap.custom_minimum_size = Vector2(287.5, 0);
			2:
				$CardArea/CardGrid/LeftGap.custom_minimum_size = Vector2(215.625, 0);
			3:
				$CardArea/CardGrid/LeftGap.custom_minimum_size = Vector2(143.75, 0);
			4:
				$CardArea/CardGrid/LeftGap.custom_minimum_size = Vector2(71.875, 0);
			_:
				$CardArea/CardGrid/LeftGap.custom_minimum_size = Vector2(0, 0);
		
		var tempsep = separation.instantiate()
		tempsep.name = str("CardGap", $CardArea/CardGrid.get_child_count()/2)
		$CardArea/CardGrid.add_child(tempsep);
		tempsep.custom_minimum_size = Vector2(18.75, 0);
