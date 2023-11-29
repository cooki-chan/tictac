using Godot;
using System;
using System.CodeDom;

public class go : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    // public override void _Ready()
    // {
        
    // }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
     MoveLocalX(5);
     GD.Print(Position);
     //if(Position.x >= (1920 + Texture.GetSize().x/2)){
    if(Position.x >= OS.WindowSize.x + this.Scale.x * Texture.GetWidth()/2){
        GD.Print("Dave Hath Thus Bein Slain...");
        QueueFree();
     }

 }
}
