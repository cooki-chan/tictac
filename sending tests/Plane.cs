using Godot;
using System;
using System.CodeDom;

public class Plane : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Signal] public delegate void edged(int xPos, string type);
    // Called when the node enters the scene tree for the first time.

    public bool goLeft = false;

    public override void _Ready()
    {
        Connect("edged", GetNode<Node>("/root/send/network_mananger"), "_dave_died");
    }

    public void setGoLeft(bool goLeft){
        this.goLeft = goLeft;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

        if( goLeft){
            MoveLocalX(-10);
        } else {
            MoveLocalX(10);
        }

    if(Position.x >= OS.WindowSize.x + this.Scale.x * Texture.GetWidth()/2){
        GD.Print("Dave Hath Thus Bein Slain...");
        EmitSignal("edged", Position.y, "DAVE THE GOLIATH");
        QueueFree();
        }

    }


}
