using Godot;
using System;

public class click : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    // public override void _Ready()
    // {
        
    // }
    private Sprite dave;
    private Texture dave_face;
    private Script dave_script;
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {



     if(Input.IsMouseButtonPressed(1)){//Left Button
        dave_face = GD.Load<Texture>("res://dave.jpg");
        dave_script = GD.Load<Script>("res://the wheee folder/go.cs");

        dave = new Sprite(); // Create a new Sprite2D.
        dave.Texture = dave_face;

        ulong objId = dave.GetInstanceId();
        dave.SetScript(dave_script);
        dave = (Sprite)GD.InstanceFromId(objId);
        
        AddChild(dave);
        GD.Print("dave hasth been sumoned");
    }
     }
 }

