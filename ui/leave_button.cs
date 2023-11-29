using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

public class leave_button : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    private Sprite joe;
    private Texture joe_face;
    private int count = 0;
    public void _on_summon_joe_pressed(){
        joe_face = GD.Load<Texture>("res://icon.png");
        joe = new Sprite(); 
        joe.Texture = joe_face;
        ulong objID = joe.GetInstanceId();
        joe.SetScript(GD.Load<Script>("res://ui/joeScript.cs"));
        joe = (Sprite) GD.InstanceFromId(objID);
        AddChild(joe);
        joe.MoveLocalY(-100);
        GD.Print("joe hasth been sumoned");
    }
}
 