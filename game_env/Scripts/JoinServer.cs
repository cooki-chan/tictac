using Godot;
using System;

public class JoinServer : Control
{
    public void _on_Leave_pressed(){
        GetTree().ChangeScene("res://game_env/Scenes/MainMenu.tscn");
    }
}
