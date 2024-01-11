using Godot;
using System;

public class click : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private Sprite dave;
    private Texture dave_face;
    private Script dave_script;
    private Texture evad_face;
    private Sprite evad;
    public override void _Ready()
    {
        dave_face = GD.Load<Texture>("res://dave.jpg");
        evad_face = GD.Load<Texture>("res://evad.jpg");
        dave = (Sprite)GetNode<Sprite>("../Dave");
        evad = (Sprite)GetNode<Sprite>("../Evad");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)    {
        if(Input.IsActionJustPressed("click")){//Left Button
            summon_dave(GetViewport().GetMousePosition().y);
        }
    }

    void summon_dave(float ypos){
        int bound = 850;
        if(GetViewport().GetMousePosition().y <= bound- dave.Scale.y * dave_face.GetWidth()/2){
            dave_script = GD.Load<Script>("res://final/Plane.cs");

            Sprite newdave = (Sprite)dave.Duplicate(); // Create a new Sprite2D.
            // newdave.Position = new Vector2(0, ypos);
            newdave.Position = GetViewport().GetMousePosition();

            ulong objId = newdave.GetInstanceId();
            newdave.SetScript(dave_script);
            newdave = (Sprite)GD.InstanceFromId(objId);
            newdave.Call("init", 10, 1, true);

            AddChild(newdave);
            GD.Print("dave hasth been sumoned");
        }
    }

    void summon_evad(float ypos){
        int bound = 850;
        if(GetViewport().GetMousePosition().y <= bound- dave.Scale.y * dave_face.GetWidth()/2){
            dave_script = GD.Load<Script>("res://final/Plane.cs");

            Sprite newdave = (Sprite)evad.Duplicate(); // Create a new Sprite2D.
            newdave.Position = new Vector2(1920, ypos);

            ulong objId = newdave.GetInstanceId();
            newdave.SetScript(dave_script);
            newdave = (Sprite)GD.InstanceFromId(objId);
            newdave.Call("init", -10, 2, false);

            AddChild(newdave);
            GD.Print("evad hasth been sumoned");
        }
    }

 }

