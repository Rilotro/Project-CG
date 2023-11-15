extends Node2D

var id;
var time = 0;
@export var sizeOffset = 15;
@export var MAXpos = 500;
@export var FullPoint = 180;

func Move(mTime):
	time = time - mTime;
	if(time < 0):
		time = 0;
	elif(time < FullPoint):
		self.position = Vector2(MAXpos*time/FullPoint+sizeOffset, 20);
	else:
		self.position = Vector2(-MAXpos, 20);

func RoundStart():
	get_node("/root").get_child(0).RoundStart(id);
