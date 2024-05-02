using System;
using Godot;
using System.Diagnostics;
using System.Collections;
public class Ship : Sprite, ICloneable{
    //connects to dave died
    [Signal] public delegate void died(int yPos, int type);
    [Signal] public delegate void _core_damaged(int damage);
    [Signal] public delegate void crashed(int damage);
    private int Type;
    private int speed;
    private bool FromOpponent = false;
    private bool sentToOpponent = false;
    static public int speed1 = 5;
    static public int speed2 = 7;
    static public int speed3 = 4;
    static public int speed4 = 5;
    static public int speed5 = 2;
    private string method;
    private int rocketCooldown = 1;
    private Timer speedtimer;
    private System.Timers.Timer rocketTimer;
    public Ship(int type, bool fromOpponent){
        FromOpponent = fromOpponent;
        Type = type;
        switch (type){
            case 1:
                speed = speed1;
                method = null;
                break;
            case 2:
                speed = speed2;
                method = "boost";
                break;
            case 3:
                speed = speed3;
                method = "rockets";
                break;
            case 4:
                speed = speed4;
                method = "laser";
                break;
            case 5:
                speed = speed5;
                method = "shield";
                break;
        }
    }
    public override void _Ready(){
        Texture = GD.Load<Texture>("res://game_env/Ships/Ship" + Type + ".png");
        Connect("died", GetNode<Node>("../network_manager"), "_dave_died");
        Connect("crashed", this, "crashedShip");
        if(Type != 1) typeof(Ship).GetMethod(method).Invoke(this, null);
        if(Global.IsServer){
            if(!FromOpponent)Texture = GD.Load<Texture>("res://game_env/RightFacingShips/Ship" + Type + ".png");
            if(FromOpponent)Texture = GD.Load<Texture>("res://game_env/LeftFacingShips/Ship" + Type + ".png");
        } else {
           if(!FromOpponent)Texture = GD.Load<Texture>("res://game_env/LeftFacingShips/Ship" + Type + ".png");
           if(FromOpponent)Texture = GD.Load<Texture>("res://game_env/RightFacingShips/Ship" + Type + ".png");
        }
        if(Type == 5){
            Sprite shield = new Sprite{
                Texture = GD.Load<Texture>("res://game_env/shield.png"),
                Position = Global.IsServer ? new Vector2(0, 0) : new Vector2(0, 0)
            };
            AddChild(shield);
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
        if(speedtimer != null && speedtimer.TimeLeft == 0)
            speed = speed2;
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
        return new Ship(Type, FromOpponent);
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
    public void boost(){
        if(FromOpponent){
            speedtimer = new Timer{
                WaitTime = 1,
                Autostart = true,
                OneShot = true
            };
            AddChild(speedtimer);
            speedtimer.Start();
            speed = (int)(speed * 1.5);
        }
    }
    public void rockets(){
        rocketTimer = new System.Timers.Timer(2000);
        rocketTimer.Elapsed += FireRockets;
        rocketTimer.Start();
    }

    private void FireRockets(object sender, System.Timers.ElapsedEventArgs e){
        Rocket rocket = new Rocket(FromOpponent, Position.x + (50 * (Global.IsServer?1:-1)), Position.y);
        GetParent().AddChild(rocket);
    }
}