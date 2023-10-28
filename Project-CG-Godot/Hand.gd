extends HBoxContainer

@export var CardSize = 125;
@export var CDB = [];
@export var CardCount = 4;
@export var Card: PackedScene;
#@export var separation: PackedScene;
var rng = RandomNumberGenerator.new();
var too_many = false;
var CardsinHand = 0;

var reposition = false;
var repoTimer = -1;

func _ready():
	for i in CardCount:
		CDB.append(load(str("res://CardDataBase/Card", i+1, ".tscn")));


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if((repoTimer > 0)&&(reposition == true)):
		repoTimer -= delta;
	elif((repoTimer <= 0)&&(reposition == true)):
		reposition = false;
		Position();

func Draw():
	var tempCard = Card.instantiate();
	tempCard.add_child(CDB[rng.randi_range(0, 3)].instantiate());
	add_child(tempCard);
	CardsinHand += 1;
	tempCard.name = str("PlayCard", CardsinHand);
	Position();

func Position():
	print(get_child_count());
	if(CardsinHand == 0):
		print("no cards!");
	elif(CardsinHand <= 5):
		var CardGap: float = CardSize + 18.75;
		get_child(1).position = Vector2((700-CardSize-CardGap*(CardsinHand-1))/2, 2.5);
		for i in CardsinHand-1:
			get_child(i+2).position = Vector2(get_child(i+1).position.x + CardGap, 2.5);
	else:
		var CardGap: float = (700-CardSize)/(CardsinHand-1);
		for i in CardsinHand:
			get_child(i+1).position = Vector2(CardGap*i, 2.5);

func FreeCard(CCU):
	CCU.get_parent().queue_free();
	CardsinHand -= 1;
	repoTimer = 0.25;
	reposition = true;
