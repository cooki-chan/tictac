using Godot;
using System;

public class rightButton : Node
{
   public void _on_Right_button_down(){
        GetTree().ChangeScene("res://game_env/RightScene(Client).tscn");
   }
}
