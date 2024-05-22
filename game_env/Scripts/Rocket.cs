using System;
using System.Collections;
using Godot;
public class Rocket : Sprite{
    [Signal] public delegate void crashed(int damage);
    [Signal] public delegate void rDied(int yPos);

    private int speed = Global.missileSpeed;
    private bool FromOpponent;
    private bool sentToOpponent = false;
    private bool pierce;

    public Rocket(bool fromOp, float x, float y, bool Pierce){
        FromOpponent = fromOp;
        Position = new Vector2(x,y);
        Texture = GD.Load<Texture>("res://Rocket.png");
        pierce = Pierce;
    }
    public override void _Ready(){
        Connect("rDied", GetNode<Node>("/root/Control/network_manager"), "rocketTransport");
    }
    public override void _Process(float delta){
        if(Global.IsServer){
            MoveLocalX(speed * (FromOpponent?-1:1));
            
        } else {
            MoveLocalX(speed * (FromOpponent?1:-1));
        } 
        checkCollision();
         if(Position.x >= OS.WindowSize.x - Scale.x * Texture.GetWidth()/2 || Position.x <= Scale.x * Texture.GetWidth()/2){
            if(!FromOpponent && !sentToOpponent){
                EmitSignal("rDied", Position.y, Global.IsServer);
                QueueFree();
                sentToOpponent = true;
            }
        }
    }
    private void checkCollision(){
        ArrayList list = Bay.activeShips;
        try{
            foreach(Ship ship in list){
                float dist = Position.x + Texture.GetWidth() - ship.Position.x;
                if(((dist <= Texture.GetWidth() && dist >= 0) || ((Position.x - ship.Position.x) < Texture.GetWidth() && (Position.x - ship.Position.x) > 0)) && Math.Abs(Position.y - ship.Position.y) <= 20){
                    ship.EmitSignal("crashed",ship.Position.x, ship.Position.y, 0);
                    if(!pierce) QueueFree();
                    else pierce = false;
                }
            }
        }catch(Exception){}
    }
}