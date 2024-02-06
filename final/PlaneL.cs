using Godot;
using System;
using System.CodeDom;

public class PlaneL : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Signal] public delegate void edged(int xPos, string type);
    int movement = -10;
    // Called when the node enters the scene tree for the first time.


    public override void _Ready()
    {
        Connect("edged", GetNode<Node>("/root/send/network_mananger"), "_dave_died");
    }

    public void editSpeed(int newSpeed){
        movement = newSpeed;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
            MoveLocalX(movement);

    if(Position.x <= this.Scale.x * Texture.GetWidth()/2 * -1){
        GD.Print("Dave Hath Thus Bein Slain...");
        EmitSignal("edged", Position.y, "DAVE THE GOLIATH");
        QueueFree();
        }

    }


}
