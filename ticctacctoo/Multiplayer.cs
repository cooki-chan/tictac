using Godot;
using System;
using System.Diagnostics;

public class Multiplayer : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private Label debugOut;
    private TextEdit name_input;
    private Label ip_label;
    private Label port_label;
    private TextEdit ip_in;
    private TextEdit port_in;
    private Label opponent_label;
    private Label opponent_id_label;
    private Label turn_label;
    private Boolean currentTurn;
    
    NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();

    public override void _Ready()
    {
        debugOut = GetNode<Label>("debug");

        name_input = GetNode<TextEdit>("name_input");

        ip_label = GetNode<Label>("host/ip_label");
        port_label = GetNode<Label>("host/port_label");

        ip_in = GetNode<TextEdit>("join/ip_in");
        port_in = GetNode<TextEdit>("join/port_in");

        opponent_label = GetNode<Label>("title/opponent_label");
        opponent_id_label = GetNode<Label>("title/opponent_id_label");
        turn_label = GetNode<Label>("title/turn_label");

        GD.Randomize();
    }
    public void _on_host_button_down(){  
        printLabel(ip_label, (string)IP.GetLocalAddresses()[5]); //why this is 5 i don't know, but it works
        int port = (int)GD.RandRange(1025, 65536);
        printLabel(port_label, port.ToString());
        peer.CreateServer(port, 1);
        GetTree().NetworkPeer = peer;

        GetTree().Connect("network_peer_connected", this, "_player_connected");
        GetTree().Connect("network_peer_disconnected", this, "_player_disconnected");
        GetTree().Connect("connected_to_server", this, "_connected_ok");
        GetTree().Connect("connection_failed", this, "_connected_fail");
        GetTree().Connect("server_disconnected", this, "_server_disconnected");
    }

    public void _on_join_button_down(){
        string ip = ip_in.Text;
        int port = int.Parse(port_in.Text);
        peer.CreateClient(ip, port);
        GetTree().NetworkPeer = peer;

        GetTree().Connect("network_peer_connected", this, "_player_connected");
        GetTree().Connect("network_peer_disconnected", this, "_player_disconnected");
        GetTree().Connect("connected_to_server", this, "_connected_ok");
        GetTree().Connect("connection_failed", this, "_connected_fail");
        GetTree().Connect("server_disconnected", this, "_server_disconnected");
    }

    void _player_connected(int id){
        debug("Opponent Connect: " + id);
        if(GetTree().IsNetworkServer()){
            //if server goes first, send false 
            if(GD.Randf() < 0.5){
                RpcId(id, "greetings", name_input.Text, false);
            } else { //send true
                RpcId(id, "greetings", name_input.Text, true);
            }
        }
    }

    void _player_disconnected(int id){
        Debug.Print("Opponent Disconnect: " + id);
    }

    void _on_disconnect_button_down(){
        GetTree().NetworkPeer = null;
    }

//RPC Functions
    [Remote]
    void greetings(String name, bool first){
        printLabel(opponent_label, name);
        printLabel(opponent_id_label, GetTree().GetRpcSenderId().ToString());
        currentTurn = first;
    }   


//Helper Functions
    void debug(String msg){
        debugOut.Text += "\n" + msg;
    }

    void printLabel(Label outp, String msg){
        outp.Text = msg;
    }
    
}
