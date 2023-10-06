using Godot;
using System;
using System.Diagnostics;

public class MultiplayerController : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private Label label;
    private Label msgLbl;
    private TextEdit inp;

    NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();
    public override void _Ready()
    {
        label = GetNode<Label>("Label");
        msgLbl = GetNode<Label>("msgLbl");
        inp = GetNode<TextEdit>("TextEdit");
    }
    public void _on_host_button_down(){  
        peer.CreateServer(2222, 1);
        GetTree().NetworkPeer = peer;
        label.Text = "server "+ GetTree().GetNetworkUniqueId();
        Debug.Print("Servr is Server?: " + GetTree().IsNetworkServer());
        GetTree().Connect("network_peer_connected", this, "_player_connected");
        GetTree().Connect("network_peer_disconnected", this, "_player_disconnected");
        GetTree().Connect("connected_to_server", this, "_connected_ok");
        GetTree().Connect("connection_failed", this, "_connected_fail");
        GetTree().Connect("server_disconnected", this, "_server_disconnected");
    }

    public void _on_join_button_down(){
        peer.CreateClient("127.0.0.1", 2222);
        GetTree().NetworkPeer = peer;
        label.Text = "client " + GetTree().GetNetworkUniqueId();
        Debug.Print("Client is Server?: " + GetTree().IsNetworkServer());
        GetTree().Connect("network_peer_connected", this, "_player_connected");
        GetTree().Connect("network_peer_disconnected", this, "_player_disconnected");
        GetTree().Connect("connected_to_server", this, "_connected_ok");
        GetTree().Connect("connection_failed", this, "_connected_fail");
        GetTree().Connect("server_disconnected", this, "_server_disconnected");
    }

    public void _on_start_button_down(){
        Rpc("greetings", inp.Text);
    }

    String[] name = {"Johnson Magenta"};
    void _player_connected(int id){
        Debug.Print("Connect: " + id);
    }

    void _player_disconnected(int id){
        Debug.Print("Disconnect: " + id);
    }

    void _on_disconnect_button_down(){
        GetTree().NetworkPeer = null;
        label.Text = " none";
    }

    [Remote]
    void greetings(String msg){
        msgLbl.Text += "\n" + msg;
    }
}
