using System;
using System.Linq.Expressions;
using Godot;
public class Ship : Sprite, ICloneable{
    //connects to dave died
    [Signal] public delegate void died(int yPos, int type);
    private int Type;
    private LambdaExpression Method;
    private int speed;
    private bool FromOpponent = false;
    private int lane;
    public Ship(int type, int Lane, LambdaExpression method, bool fromOp){
        Type = type;
        Method = method;
        lane = Lane;
        GD.Print(lane);
        switch (type){
            case 1:
                speed = 10;
                break;
            case 2:
                speed = 15;
                break;
            case 3:
                speed = 8;
                break;
            case 4:
                speed = 10;
                break;
            case 5:
                speed = 5;
                break;
        }
    }


    public override void _Ready(){
        if(Global.IsServer){
            Texture = GD.Load<Texture>("res://game_env/RightFacingShips/Ship" + Type + ".png");
        } else {
            Texture = GD.Load<Texture>("res://game_env/LeftFacingShips/Ship" + Type + ".png");
        }
        Connect("died", GetNode<Node>("../network_manager"), "_dave_died");
    }
//variable movement add
 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
        if(Global.IsServer){
            MoveLocalX(speed);
            
        } else {
            MoveLocalX(speed * -1);
        } 

        if(Position.x > OS.WindowSize.x || Position.x < 0){
            if(!FromOpponent){
                EmitSignal("died", Position.y, Type);
            }
        }
        if(Position.x >= OS.WindowSize.x + this.Scale.x * Texture.GetWidth()/2 || Position.x <= -1 * this.Scale.x * Texture.GetWidth()/2){
            QueueFree();
        }
    }
    public int getType(){
        return Type;
    }

    public object Clone(){
        return new Ship(Type, lane, Method, FromOpponent);
    }
}
