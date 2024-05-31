using Godot;
using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Text;

public class network_mananger : Node{
    NetworkedMultiplayerENet peer = new NetworkedMultiplayerENet();


    public override void _Ready(){
        GetTree().Connect("network_peer_connected", this, "_player_connected");
        GetTree().Connect("network_peer_disconnected", this, "_player_disconnected");
        GetTree().Connect("connected_to_server", this, "_connected_ok");
        GetTree().Connect("connection_failed", this, "_connected_fail");
        GetTree().Connect("server_disconnected", this, "_server_disconnected");

        GD.Randomize();
    }

    void _player_disconnected(int id){
        Debug.Print("Opponent Disconnect: " + id);
    }


    void _on_disconnect_button_down(){
        GetTree().NetworkPeer = null;
    }

    void _server_disconnected(){
        Debug.Write("oop the server died lmaoooooooo");
    }

//Dave Things -------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void _dave_died(int yPos, int type){
        Rpc("summonDave", yPos, type);
    }

//RPC Functions -------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    [Remote]
    void summonDave(int yPos, int type){
        Control control = GetNode<Control>("..");
        Ship ship = new Ship(type,true);
        ulong objID = ship.GetInstanceId();
        ship = (Ship) GD.InstanceFromId(objID);
        Ship newship = (Ship)ship.Clone();
        newship.Position = new Vector2(1000, 2000000);
        control.AddChild(newship); 
        if(Global.IsServer){
            newship.Position = new Vector2(OS.WindowSize.x + newship.Scale.x * GD.Load<Texture>("res://game_env/RightFacingShips/Ship" + newship.getType() + ".png").GetWidth()/2,yPos); //On server, enemy needs to spawn on the right
        } else {
            newship.Position = new Vector2(GD.Load<Texture>("res://game_env/RightFacingShips/Ship" + newship.getType() + ".png").GetWidth()/2 * -1 + 10,yPos); //On client, enemy needs to spawn on the left
        }
        GD.Print("Ship hasth been sumoned");
    }
    void rocketTransport(int yPos, bool IsServer){
        Rpc("summonRocket", yPos, IsServer);
    }
    void lost(){
        Rpc("lostFunc");
    }
    [Remote]
    void lostFunc(){
        GetTree().ChangeScene("res://game_env/Scenes/WinScene.tscn");
    }
    [Remote]
    void summonRocket(int yPos, bool isServer, bool pierce){
        Control control = GetNode<Control>("..");
        Rocket newRocket = new Rocket(true, isServer? 0:OS.WindowSize.x, yPos, pierce);
        control.AddChild(newRocket);
    }

    
    void sendGlobal(){
        Rpc("setOpGlobal", GetNode<Node>("/root/Global"));
    }
    [Remote]
    void setOpGlobal(Node global){

    }
}
