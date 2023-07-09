extends Node

func _process(delta):
	if Input.is_action_just_pressed("debug_start_iteration"):
		%Simulation.StartNextIteration()
