using Godot;
using System;
using System.Diagnostics;

public class Cros : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    private Boolean vis = true;

    public void _on_Button_pressed(){
        GD.Print("cros: clicked");
        if(vis){
            Visible = !Visible;
        }
    }

    public void _on_XO_Button_toggled(bool visibility){
        GD.Print("cros: switched");
        vis = !visibility;
        Visible = false;
    }
}
