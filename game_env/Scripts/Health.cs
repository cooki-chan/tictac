using Godot;
using System;

public class Health : Label
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";



//  // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
     this.Text = "" + Global.Health;
 }
}
