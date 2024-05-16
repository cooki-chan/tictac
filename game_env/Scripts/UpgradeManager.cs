using Godot;
using System;

public class UpgradeManager : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        foreach(Godot.Sprite color in this.GetChildren()){
            foreach(Godot.Button button in color.GetChildren()){
                Connect("pressed", this, "PLEASE", [button]);
            }
        }
    }

    public void PLEASE(Godot.Button button){
        GD.Print(button.GetParent())
    }
}
