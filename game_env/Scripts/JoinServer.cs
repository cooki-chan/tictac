using Godot;
using System;

public class JoinServer : Control
{
    public void _on_Leave_pressed(){
        GetTree().ChangeScene("res://game_env/Scenes/MainMenu.tscn");
    }

    public void _connected_ok(int id){
        GetTree().ChangeScene("res://game_env/Scenes/RightScene(Client).tscn");
    }

    string decodeIp(String code){
        long outputInt = 0;
        string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        for(int i = 0; i < code.Length(); i++){
            outputInt = (outputInt * 62) + Chars.IndexOf(code[i]);
        }

        string output = Convert.ToString(outputInt);
        while(output.Length() < 17){
            output = "0" + output;
        }

        return output.Substring(0,3) + "." + output.Substring(3,3) + "." + output.Substring(6,3) + "." + output.Substring(9,3) + ":" + Convert.ToString(outputInt % 100000);
    }
}
