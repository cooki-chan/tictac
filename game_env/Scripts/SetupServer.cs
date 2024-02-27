using Godot;
using System;

public class SetupServer : Control
{
    public void _on_Leave_pressed(){
        GetTree().NetworkPeer = null;
        GetTree().ChangeScene("res://game_env/Scenes/MainMenu.tscn");
    }
}
