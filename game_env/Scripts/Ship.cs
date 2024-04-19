using System;
using System.Linq.Expressions;
using Godot;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Collections;
using System.Threading;
public class Ship : Sprite, ICloneable{
    //connects to dave died
    [Signal] public delegate void died(int yPos, int type);
    [Signal] public delegate void _core_damaged(int damage);
    [Signal] public delegate void crashed(int damage);
    private int Type;
    private LambdaExpression Method;
    private int speed;
    private bool FromOpponent = false;
    private int lane;
    private bool sentToOpponent = false;
    static public int speed1 = 10;
    static public int speed2 = 15;
    static public int speed3 = 8;
    static public int speed4 = 10;
    static public int speed5 = 5;

    public Ship(int type, int Lane, LambdaExpression method, bool fromOp){
        Type = type;
        Method = method;
        lane = Lane;
        FromOpponent = fromOp;
        switch (type){
            case 1:
                speed = speed1;
                break;
            case 2:
                speed = speed2;
                break;
            case 3:
                speed = speed3;
                break;
            case 4:
                speed = speed4;
                break;
            case 5:
                speed = speed5;
                break;
        }
    }

    public Ship(int type, LambdaExpression method, bool fromOpponent){
        FromOpponent = fromOpponent;
        Type = type;
        Method = method;
        switch (type){
            case 1:
                speed = speed1;
                break;
            case 2:
                speed = speed2;
                break;
            case 3:
                speed = speed3;
                break;
            case 4:
                speed = speed4;
                break;
            case 5:
                speed = speed5;
                break;
        }
    }
    public override void _Ready(){
        Texture = GD.Load<Texture>("res://game_env/Ships/Ship" + Type + ".png");
        Connect("died", GetNode<Node>("../network_manager"), "_dave_died");
        Connect("crashed", this, "crashedShip");
        if(Global.IsServer){
            if(!FromOpponent)Texture = GD.Load<Texture>("res://game_env/RightFacingShips/Ship" + Type + ".png");
            if(FromOpponent)Texture = GD.Load<Texture>("res://game_env/LeftFacingShips/Ship" + Type + ".png");
        } else {
           if(!FromOpponent)Texture = GD.Load<Texture>("res://game_env/LeftFacingShips/Ship" + Type + ".png");
           if(FromOpponent)Texture = GD.Load<Texture>("res://game_env/RightFacingShips/Ship" + Type + ".png");
        }
        if(Type == 5){
            Sprite shield = new Sprite();
            shield.Texture = GD.Load<Texture>("res://game_env/shield.png");
            shield.Position = Global.IsServer?new Vector2(0, 0):new Vector2(0, 0);
            this.AddChild(shield);
        }
    }


//variable movement add
 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
        if(Global.IsServer){
            MoveLocalX(speed * (FromOpponent?-1:1));
            
        } else {
            MoveLocalX(speed * (FromOpponent?1:-1));
        } 
        checkCollision(this);
        if(Position.x >= OS.WindowSize.x - this.Scale.x * Texture.GetWidth()/2 || Position.x <= this.Scale.x * Texture.GetWidth()/2){
            if(!FromOpponent && !sentToOpponent){
                EmitSignal("died", Position.y, Type);
                Debug.Print("ship has been sent");
                Bay.activeShips.Remove(this);
                sentToOpponent = true;
            }
        }
        if(Position.x >= OS.WindowSize.x + this.Scale.x * Texture.GetWidth()/2 || Position.x <= -1 * this.Scale.x * Texture.GetWidth()/2){
            QueueFree();
        }
        if(Global.IsServer){
            if(Position.x - this.Scale.x * Texture.GetWidth()/2 <= 500){
                if(FromOpponent){
                    Debug.Print("Taken Damage OMG :OOOOOOOO!!!!");
                    Global.Health -= 500;
                    QueueFree();
                }
            }
        } else {
            if(Position.x + this.Scale.x * Texture.GetWidth()/2 >= 1420){
                if(FromOpponent){
                    Debug.Print("Taken Damage OMG :OOOOOOOO!!!!");
                    Global.Health -= 500;
                    QueueFree();

                }
            }
        }
    }
    public int getType(){
        return Type;
    }

    public object Clone(){
        return new Ship(Type, lane, Method, FromOpponent);
    }
    private static void checkCollision(Ship local){
        ArrayList list = Bay.activeShips;
        try{
            foreach(Ship ship in list){
                if(ship.GetInstanceId() != local.GetInstanceId()){
                    float dist = local.Position.x + local.Texture.GetWidth() - ship.Position.x;
                    if(((dist <= local.Texture.GetWidth() && dist >= 0) || ((local.Position.x - ship.Position.x) < local.Texture.GetWidth() && (local.Position.x - ship.Position.x) > 0)) && Math.Abs(local.Position.y - ship.Position.y) <= 20){
                        ship.EmitSignal("crashed",ship.Position.x, ship.Position.y);
                        local.EmitSignal("crashed",local.Position.x, local.Position.y);
                    }
                }
            }
        }catch(Exception){}
    }
    private void crashedShip(int x, int y){
        GD.Print("crashed");
        QueueFree();
        Bay.activeShips.Remove(this);
    }
}
