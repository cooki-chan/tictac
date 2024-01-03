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
    public override void _Ready()
    {
        dave_face = GD.Load<Texture>("res://dave.jpg");
        evad_face = GD.Load<Texture>("res://evad.jpg");
        dave = (Sprite)GetNode<Sprite>("../Dave");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)    {
        if(Input.IsActionJustPressed("click")){//Left Button
            summon_dave(GetViewport().GetMousePosition().y, true);
        }
    }

    void summon_dave(float ypos, bool goingLeft){
        int bound = 850;
        if(GetViewport().GetMousePosition().y <= bound- dave.Scale.y * dave_face.GetWidth()/2){
            dave_script = GD.Load<Script>("res://final/Plane.cs");
            if(!GetTree().IsNetworkServer()){
                dave_script.Set("goLeft", goingLeft);
            }

            Sprite newdave = (Sprite)dave.Duplicate(); // Create a new Sprite2D.
            // newdave.Position = new Vector2(0, ypos);
            newdave.Position = GetViewport().GetMousePosition();

            ulong objId = newdave.GetInstanceId();
            newdave.SetScript(dave_script);
            newdave = (Sprite)GD.InstanceFromId(objId);

            AddChild(newdave);
            GD.Print("dave hasth been sumoned");
        }
    }

    void summon_evad(float ypos, bool goingLeft){
        int bound = 850;
        if(GetViewport().GetMousePosition().y <= bound- dave.Scale.y * dave_face.GetWidth()/2){
            dave_script = GD.Load<Script>("res://final/Plane.cs");
            if(!GetTree().IsNetworkServer()){
                dave_script.Set("goLeft", goingLeft);
            }

            Sprite newdave = (Sprite)dave.Duplicate(); // Create a new Sprite2D.
            newdave.Position = new Vector2(0, ypos);

            ulong objId = newdave.GetInstanceId();
            newdave.SetScript(dave_script);
            newdave = (Sprite)GD.InstanceFromId(objId);

            AddChild(newdave);
            GD.Print("dave hasth been sumoned");
        }
    }

 }

