using Godot;
using System;

public class Button : Godot.Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    private bool test = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _on_Button_pressed(){
        GD.Print("SSSSSSSSSSSA");
    }

    
}
