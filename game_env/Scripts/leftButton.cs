using Godot;
using System;

public class leftButton : Node
{
    public void _on_Left_button_down(){
        GetTree().ChangeScene("res://game_env/LeftScene(Server).tscn");
   }
}
