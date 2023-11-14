using Godot;
using System;
using System.Diagnostics;

public class network_mananger : Node
{
 // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private Label debugOut;
    private TextEdit name_input;
    private Label join_code_label;
    private TextEdit join_code_in;
    
    NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();

    public override void _Ready()
    {
        debugOut = GetNode<Label>("debug");

        name_input = GetNode<TextEdit>("name_input");

        join_code_label = GetNode<Label>("host/join_label");

        join_code_in = GetNode<TextEdit>("join/join_in");


        GD.Randomize();
    }
    public void _on_host_button_down(){  
        printLabel(join_code_label, (string)IP.GetLocalAddresses()[5]); //why this is 5 i don't know, but it works
        int port = (int)GD.RandRange(1025, 65536);
        peer.CreateServer(port, 1);
        GetTree().NetworkPeer = peer;

        GetTree().Connect("network_peer_connected", this, "_player_connected");
        GetTree().Connect("network_peer_disconnected", this, "_player_disconnected");
        GetTree().Connect("connected_to_server", this, "_connected_ok");
        GetTree().Connect("connection_failed", this, "_connected_fail");
        GetTree().Connect("server_disconnected", this, "_server_disconnected");
    }

    public void _on_join_button_down(){
        string ip = join_code_in.Text;
        peer.CreateClient(ip, 973);
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
        debug("Opponent Name: " + name);
        debug("Opponent RPC ID: " + GetTree().GetRpcSenderId().ToString());
    }   


//Helper Functions
    void debug(String msg){
        debugOut.Text += "\n" + msg;
    }

    void printLabel(Label outp, String msg){
        outp.Text = msg;
    }

    String encodeIp(String ip, int port){
        String[] strings= ip.Split(".");
        string base10 = string.Join("", strings) + Convert.ToString(port);
        int base10Int = Convert.ToInt32(base10);
        string  base16 = Convert.ToString(base10, 16);
    }
}
