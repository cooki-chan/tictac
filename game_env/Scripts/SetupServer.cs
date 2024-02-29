using Godot;
using System;

public class SetupServer : Control
{

    public override void _Ready(){
        GetTree().Connect("network_peer_connected", this, "_player_connected");
    }
    public void _on_Leave_pressed(){
        GetTree().NetworkPeer = null;
        GetTree().ChangeScene("res://game_env/Scenes/MainMenu.tscn");
    }

    public void _on_TESTING_BUTTON_pressed(){
        NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();
        string ip = ""; 
        foreach(String i in IP.GetLocalAddresses()){
            String temp = i.Substring(0,3);
            if(string.Equals(temp,"192") || string.Equals(temp,"172") || string.Equals(i.Substring(0,2),"10")){
                ip = i;
                break;
            }
        }
        int port = (int)GD.RandRange(1025, 65536);
        GD.Print(port);
        peer = new NetworkedMultiplayerENet();
        peer.CreateServer(port, 1);
        GetTree().NetworkPeer = peer;
        Global.IsServer = true;

        GetTree().ChangeScene("res://game_env/Scenes/LeftScene(Server).tscn");
    }
}
