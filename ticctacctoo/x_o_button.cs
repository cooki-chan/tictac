using Godot;
using System;
using System.Diagnostics;
using System.Data.SqlClient;

public class x_o_button : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private Button button;
    private Sprite circle;
    private Sprite cross;
    private Sprite highlight;
    private bool currentTurn; // true is X, false is O
    private bool canPress;
    public override void _Ready()
    {
        GD.Print(GetNode<Node>("e"));
        circle = GetNode<Sprite>("e/Circl");
        cross = GetNode<Sprite>("e/Cros");
        highlight = GetNode<Sprite>("e/Highlight");

        button.Connect("pressed", this, "_on_Button_pressed");
        button.Connect("mouse_entered", this, "_on_Button_mouse_entered");
        button.Connect("mouse_exited", this, "_on_Button_mouse_exited");
        canPress = true;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }



    public void _on_Button_mouse_entered(){
        if(canPress){
            highlight.Visible = true;
        }
        
    }

    public void _on_Button_mouse_exited(){
        highlight.Visible = false;
    }

    public void _on_Button_pressed(){
        if(currentTurn && canPress){
            cross.Visible = true;
        } else {
            circle.Visible = true;
        }
        canPress = false;
        highlight.Visible = false;
    }

    public void freeze(){
        canPress = false;
    }
}
