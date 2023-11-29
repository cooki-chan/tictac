using Godot;
using System;
using System.Diagnostics;
using System.Text;

public class network_mananger : Node{
 // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    // Called when the node enters the scene tree for the first time.
    private Label debugOut;
    private TextEdit name_input;
    private Label join_code_label;
    private TextEdit join_code_in;
    NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();

    public override void _Ready(){
        debugOut = GetNode<Label>("/root/send/debug");
        name_input = GetNode<TextEdit>("/root/send/name_input");
        join_code_label = GetNode<Label>("/root/send/host/join_label");
        join_code_in = GetNode<TextEdit>("/root/send/join/join_in");

        GD.Randomize();
    }

    public void _on_host_button_down(){  
        string ip = ""; //why this is 5 i don't know, but it works
        foreach(String i in IP.GetLocalAddresses()){
            String temp = i.Substring(0,3);
            if(string.Equals(temp,"192") || string.Equals(temp,"172") || string.Equals(temp,"10")){
                ip = i;
                break;
            }
        }
        int port = (int)GD.RandRange(1025, 65536);
        printLabel(join_code_label, encodeIp(ip, port));
        peer.CreateServer(port, 1);
        GetTree().NetworkPeer = peer;

        GetTree().Connect("network_peer_connected", this, "_player_connected");
        GetTree().Connect("network_peer_disconnected", this, "_player_disconnected");
        GetTree().Connect("connected_to_server", this, "_connected_ok");
        GetTree().Connect("connection_failed", this, "_connected_fail");
        GetTree().Connect("server_disconnected", this, "_server_disconnected");
    }

    public void _on_join_button_down(){
        string ip = decodeIp(join_code_in.Text).Substring(0, join_code_in.ToString().IndexOf(":"));
        int port = Convert.ToInt32(decodeIp(join_code_in.ToString()).Substring(join_code_in.ToString().IndexOf(":")+1));
        peer.CreateClient(ip, 973);
        GetTree().NetworkPeer = peer;

        GetTree().Connect("network_peer_connected", this, "_player_connected");
        GetTree().Connect("network_peer_disconnected", this, "_player_disconnected");
        GetTree().Connect("connected_to_server", this, "_connected_ok");
        GetTree().Connect("connection_failed", this, "_connected_fail");
        GetTree().Connect("server_disconnected", this, "_server_disconnected");
    }

    public void _on_copy_button_pressed(){
        OS.SetClipboard(join_code_label.Text);
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

    string encodeIp(String ip, int port){
        GD.Print(ip + ":" + port);
        string output = "";
        string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        String[] strings = ip.Split(".");
        string base10 = string.Join("", strings) + String.Format("{0:00000}", 8080); //TODO: CHANGE BACK TO PORT!!!!!
        long value = Convert.ToInt64(base10);
        while (value > 0){
            long test = value % 62;
            output = Chars[(int)(value % 62)] + output; 
            value /= 62;
        }
        return output;
    }

    string decodeIp(String code){
        int outputInt = 1;
        string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        for(int i = code.Length()-1; i > 0; i--){
            int index = Chars.IndexOf(code[i]);
            char currLetter = code[i];
            outputInt += Chars.IndexOf(code[i]) * (int)Math.Pow(62,i-code.Length()-1);
        }
        string output = Convert.ToString(outputInt);
        while(output.Length() != 17){
            output = "0" + output;
        }
        GD.Print(output);
        return output.Substring(0,3) + "." + output.Substring(3,6) + "." + output.Substring(6,9) + "." + output.Substring(9,12) + ":" + output.Substring(12,17);
    }
}
