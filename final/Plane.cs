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

    public int speed;
    int type;
    bool fromOp;    

    public override void _Ready()
    {
        Connect("edged", GetNode<Node>("/root/send/network_mananger"), "_dave_died");
    }

    public void init(int speed, int type, bool sendable){
        this.speed = speed;
        this.type = type;
        this.fromOp = sendable;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {


        MoveLocalX(speed);

        if(Position.x >= OS.WindowSize.x || Position.x <= 0){
            GD.Print("Dave Hath Thus Bein Slain...");
            if(fromOp){
                EmitSignal("edged", Position.y, "DAVE THE GOLIATH");
            }
        }
        if(Position.x >= OS.WindowSize.x + this.Scale.x * Texture.GetWidth()/2 || Position.x <= this.Scale.x * Texture.GetWidth()/2 * -1){
            QueueFree();
        }

    }


}
