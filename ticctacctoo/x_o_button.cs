using Godot;
using System;

public class x_o_button : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private Button button;
    private Sprite circle;
    private Sprite cross;
    private Sprite highlight;
    public override void _Ready()
    {
        //button = GetNode<Button>("Button");
        circle = GetNode<Sprite>("Circl");
        cross = GetNode<Sprite>("Cros");
        highlight = GetNode<Sprite>("Highlight");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _on_Button_mouse_entered(){
        highlight.Visible = true;
    }

    public void _on_Button_mouse_exited(){
        highlight.Visible = false;
    }
}
