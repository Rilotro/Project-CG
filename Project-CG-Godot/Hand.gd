extends MarginContainer

@export var CDB = [];
@export var CardCount = 4;
@export var Card = load("res://Card.tscn");
@export var separation: PackedScene;
var rng = RandomNumberGenerator.new();
var too_many = false;
var CardsinHand = 0;

func _ready():
	for i in CardCount:
		CDB.append(load(str("res://CardDataBase/Card", i+1, ".tscn")));


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if(Input.is_action_just_pressed("TestCards")):
		Draw();


func Draw():
	var tempCard = Card.instantiate();
	tempCard.add_child(CDB[rng.randi_range(0, 3)].instantiate());
	$CardArea/CardGrid.add_child(tempCard);
	CardsinHand += 1;
	tempCard.name = str("Card", CardsinHand);
	
	match CardsinHand:
		1:
			$CardArea/CardGrid/LeftGap.custom_minimum_size = Vector2(287.5, 0);
		2:
			$CardArea/CardGrid/LeftGap.custom_minimum_size = Vector2(215.625, 0);
		3:
			$CardArea/CardGrid/LeftGap.custom_minimum_size = Vector2(143.75, 0);
		4:
			$CardArea/CardGrid/LeftGap.custom_minimum_size = Vector2(71.875, 0);
		5:
			$CardArea/CardGrid/LeftGap.custom_minimum_size = Vector2(0, 0);
		6:
			too_many = true;
			for i in $CardArea/CardGrid.get_child_count():
				if "CardGap" in $CardArea/CardGrid.get_child(i).name:
					$CardArea/CardGrid.get_child(i).queue_free();
		_:
			print("too many!");

	if too_many == false:
		var tempsep = separation.instantiate()
		tempsep.name = str("CardGap", CardsinHand);
		$CardArea/CardGrid.add_child(tempsep);
		tempsep.custom_minimum_size = Vector2(18.75, 0);
