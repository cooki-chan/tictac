using Godot;
using System;

public class move_try1 : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}


	private int _speed = 400;
	private float _angularSpeed = Mathf.Pi;
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		var direction = 0;
		if (Input.IsActionPressed("ui_left"))
		{
			direction = -1;
		}
		if (Input.IsActionPressed("ui_right"))
		{
			direction = 1;
		}

		Rotation += _angularSpeed * direction * (float)delta;

		var velocity = Vector2.Zero;
		if (Input.IsActionPressed("ui_up"))
		{
			velocity = Vector2.Up.Rotated(Rotation) * _speed;
		}
		if (Input.IsActionPressed("ui_down"))
		{
			velocity = -1 * Vector2.Up.Rotated(Rotation) * _speed;
		}

		Position += velocity * (float)delta;
	}
}
