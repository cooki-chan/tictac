using Godot;
using System;
using System.Data.Common;
using System.Diagnostics;

public class client : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();
        peer.CreateClient("127.0.0.1", 2222);
        GetTree().NetworkPeer = peer;
        Debug.Print("Client is Server?: " + GetTree().IsNetworkServer());

        GetTree().Connect("network_peer_connected", this, "_player_connected");
        GetTree().Connect("network_peer_disconnected", this, "_player_disconnected");
    }



    String[] name = {"Johnson Magenta"};
    void _player_connected(int id){
        RpcId(id, "hello_world", name);
        Debug.Print("Connect: " + id);
    }

    void _player_disconnected(int id){
        Debug.Print("Disconnect: " + id);
    }

    void hello_world(String[] info){
        Debug.Print("Name: " + info[0]);
    }

}
