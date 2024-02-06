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
    [Signal] public delegate void summon_dave(int yPos);
    [Signal] public delegate void summon_evad(int yPos);

    public override void _Ready(){
        debugOut = GetNode<Label>("debug");
        name_input = GetNode<TextEdit>("name_input");
        join_code_label = GetNode<Label>("host/join_label");
        join_code_in = GetNode<TextEdit>("join/join_in");

        GD.Randomize();
    }

    public void _on_host_button_down(){  
        string ip = ""; 
        foreach(String i in IP.GetLocalAddresses()){
            String temp = i.Substring(0,3);
            if(string.Equals(temp,"192") || string.Equals(temp,"172") || string.Equals(i.Substring(0,2),"10")){
                ip = i;
                break;
            }
        }
        debug("SERVER IP:" + ip);
        int port = (int)GD.RandRange(1025, 65536);
        GD.Print(port);
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
        string ipRaw = decodeIp(join_code_in.Text);
        string ip = ipRaw.Split(":")[0];
        int port = Convert.ToInt32(ipRaw.Split(":")[1]);
        debug("ATTEMPTING TO CONNECT TO:" + ip+ ":" + port);
        peer.CreateClient(ip, port);
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
        RpcId(id, "greetings", name_input.Text);
    }

    void connected_to_server(int id){
        RpcId(id, "greetings", name_input.Text);
    }

    void _player_disconnected(int id){
        Debug.Print("Opponent Disconnect: " + id);
    }

    void _on_disconnect_button_down(){
        GetTree().NetworkPeer = null;
    }

//Dave Things -------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void _dave_died(int yPos, int type){
        Debug.Print("Dave has been sent to the enemy");
        Rpc("summonDave", yPos, type);
    }

//RPC Functions -------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    [Remote]
    void greetings(String name){
        debug("Opponent Name: " + name);
        debug("Opponent RPC ID: " + GetTree().GetRpcSenderId().ToString());
    }   

    [Remote]
    void summonDave(int yPos, int type){
        debug("New Dave Summoned @ y=" + yPos + " with type: " + type);
        Control control = GetNode<Control>("/root/Control");
        Ship ship = new Ship(type,null);
        ulong objID = ship.GetInstanceId();
        ship = (Ship) GD.InstanceFromId(objID);
        Ship newship = (Ship)ship.Clone();
        newship.Position = new Vector2(0,yPos);
        control.AddChild(newship); 
        GD.Print("Ship hasth been sumoned");
        // EmitSignal("summon_evad", (float)yPos);
    }
//Helper Functions ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void debug(String msg){
        debugOut.Text += "\n" + msg;
    }

    void printLabel(Label outp, String msg){
        outp.Text = msg;
    }

    //https://www.dcode.fr/base-n-convert

    string encodeIp(String ip, int port){
        string output = "";
        string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        String[] strings = ip.Split(".");
        for(int i = 0; i < strings.Length; i++){
            strings[i] = String.Format("{0:000}", Convert.ToInt32(strings[i]));
        }
        string base10 = string.Join("", strings) + String.Format("{0:00000}", port); //TODO: CHANGE BACK TO PORT!!!!!
        long value = Convert.ToInt64(base10);
        while (value > 0){
            output = Chars[(int)(value % 62)] + output; 
            value /= 62;
        }
        return output;
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
