using Godot;
using System;

public class KRILL_URSELF : Godot.Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    PackedScene scene;
    public override void _Ready()
    {
        scene = GD.Load<PackedScene>("res://game_env/Scenes/UpgradePopUp.tscn");
    }

    public void _on_Button_pressed(){
        if(!GetNode<Node>("..").HasNode("Control")){
            GetNode<Control>("..").AddChild(scene.Instance());
        }
        else {
            CanvasItem temp = GetNode<Control>("..").GetNode<Control>("Control");
            temp.PropagateCall("Show");
            temp.Show();
        }
    }
}
