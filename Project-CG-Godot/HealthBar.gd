extends Sprite2D


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func ChangeBar(currentHP, MAXHP):
	self.scale = Vector2(currentHP*140/MAXHP, 25);
	self.position = Vector2(self.scale.x/2, 12.5);
