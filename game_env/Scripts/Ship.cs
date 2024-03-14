using System;
using System.CodeDom;
using System.Collections;
using System.Linq.Expressions;
using System.Threading;
using Godot;
public class Ship : Sprite, ICloneable{
    //connects to dave died
    [Signal] public delegate void died(int yPos, int type);
    [Signal] public delegate void crashed(int xPos, int yPos);
    private int Type;
    private LambdaExpression Method;
    private int speed;
    private bool FromOpponent = false;
    private int lane;
    public System.Threading.Thread thread;
    public Ship(int type, int Lane, LambdaExpression method){
        Type = type;
        Method = method;
        lane = Lane;
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
        thread = new System.Threading.Thread(new ThreadStart(() => checkCollision(this)));
    }

    public Ship(int type, LambdaExpression method, bool fromOpponent){
        FromOpponent = fromOpponent;
        Type = type;
        Method = method;
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
            case 5:
                speed = 5;
                break;
        }
    }
    public override void _Ready(){
        Texture = GD.Load<Texture>("res://game_env/Ships/Ship" + Type + ".png");
        Connect("died", GetNode<Node>("../network_manager"), "_dave_died");
        Connect("crashed", this, "crashedShip");
        thread.Start();
    }
//variable movement add
 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
        MoveLocalX(speed); 
        if(Position.x > OS.WindowSize.x){
            if(!FromOpponent){
                EmitSignal("died", Position.y, Type);
            }
           QueueFree();
           Bay.activeShips.Remove(this);
        }
        
    }
    public int getType(){
        return Type;
    }

    public object Clone(){
        return new Ship(Type, lane, Method);
    }
    private static void checkCollision(Ship local){
        ArrayList list = Bay.activeShips;
        foreach(Ship ship in list){
            if(ship.GetInstanceId() != local.GetInstanceId()){
                if(ship.Position.y == local.Position.y && ship.Position.x < (local.Position.x + local.Texture.GetWidth()) && ship.Position.x > local.Position.x){
                    ship.EmitSignal("crashed",ship.Position.x, ship.Position.y);
                    local.EmitSignal("crahsed",local.Position.x, local.Position.y);

                }
            }
        }
    }
    private void crashedShip(int x, int y){
        GD.Print("crashed");
        Bay.activeShips.Remove(this);
    }
    public void sleep(int i){
        System.Threading.Thread.Sleep(i);
    }
}
