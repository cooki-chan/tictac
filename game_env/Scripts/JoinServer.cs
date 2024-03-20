using Godot;
using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Text;

public class JoinServer : Control
{
    NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();
    private TextEdit join_code_in;
    public override void _Ready(){
        join_code_in = GetNode<TextEdit>("join_in");

    }
    public void _on_Leave_pressed(){
        GetTree().ChangeScene("res://game_env/Scenes/MainMenu.tscn");
    }

    public void _connected_ok(int id){
        GetTree().ChangeScene("res://game_env/Scenes/RightScene(Client).tscn");
        RpcId(id, "greetings");
    }
        public void network_peer_connected(int id){
        RpcId(id, "greetings");
    }
    [Remote]
    void greetings(){
        GetTree().ChangeScene("res://game_env/Scenes/RightScene(Client).tscn");
    }   

        void _player_connected(int id){
        RpcId(id, "greetings");
    }

    public void _on_join_pressed(){
        string test= join_code_in.Text;
        string ipRaw = decodeIp(test);
        string ip = ipRaw.Split(":")[0];
        int port = Convert.ToInt32(ipRaw.Split(":")[1]);
        Debug.Print(ipRaw);
        try{
            peer = new NetworkedMultiplayerENet();
            peer.CreateClient(ip, port);
            GetTree().NetworkPeer = peer;
            Global.IsServer = false;
            Debug.Print("Connected");
            GetTree().ChangeScene("res://game_env/Scenes/RightScene(Client).tscn");
        } catch(Exception e){
            Debug.Print(e.ToString());
        }
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
