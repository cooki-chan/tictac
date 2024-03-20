using Godot;
using System;

public class MainMenu : Node
{

    public void _on_StartLocal_pressed(){
        GetTree().ChangeScene("res://game_env/Scenes/SetupServer.tscn");
    }


    public void _on_JoinLocal_pressed(){
        GetTree().ChangeScene("res://game_env/Scenes/JoinServer.tscn");
    }

    public void _on_Leave_pressed(){
        GetTree().Quit();
    }
}
