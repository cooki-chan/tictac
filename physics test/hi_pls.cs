using Godot;
using System;

public class hi_pls : StaticBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello, world!");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void _on_StaticBody2D_mouse_entered(){
		GD.Print("yahello!");
	}
}
