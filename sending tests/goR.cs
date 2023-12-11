using Godot;
using System;
using System.CodeDom;

public class goR : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Signal] public delegate void edged(int xPos, string type);
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        this.Connect("edged", GetNode<Node>("/root/send/network_mananger"), "_dave_died");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

        MoveLocalX(10);

        
        GD.Print(Position);
        //if(Position.x >= (1920 + Texture.GetSize().x/2)){
    if(Position.x >= OS.WindowSize.x + this.Scale.x * Texture.GetWidth()/2){
        GD.Print("Dave Hath Thus Bein Slain...");
        EmitSignal("edged", Position.x, "DAVE THE GOLIATH");
        QueueFree();
        }

    }


}
