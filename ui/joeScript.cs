using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

public class joeScript : Sprite{
    public int Speed { get; set; } = 400;
    private float _angularSpeed = Mathf.Pi;
    private float Rotation = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
		var velocity = Vector2.Zero;
		Position += velocity * (float)delta;
        MoveLocalX(50); 
    }
}
