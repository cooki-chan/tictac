using Godot;
using System;

public class SetupServer : Control
{

    private Label join_code_label;
    NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();
    public override void _Ready(){
        GD.Randomize();
        join_code_label = GetNode<Label>("CodeLabel");
        peer = new NetworkedMultiplayerENet();
        GetTree().Connect("network_peer_connected", this, "_player_connected");

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

        join_code_label.Text = encodeIp(ip, port);
    }
    public void _on_Copy_Code_pressed(){
        OS.SetClipboard(join_code_label.Text);
    }
    public void _on_Leave_pressed(){
        GetTree().NetworkPeer = null;
        GetTree().ChangeScene("res://game_env/Scenes/MainMenu.tscn");
    }

    public void network_peer_connected(int id){
        GetTree().ChangeScene("res://game_env/Scenes/LeftScene(Server).tscn");
        RpcId(id, "greetings");
    }
     public void _connected_ok(int id){
        GetTree().ChangeScene("res://game_env/Scenes/LeftScene(Server).tscn");
        RpcId(id, "greetings");
    }
    [Remote]
    void greetings(){
        GetTree().ChangeScene("res://game_env/Scenes/RightScene(Client).tscn");
    }  

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


}
