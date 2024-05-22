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
    private bool rocketsPierce = false;
    static public int speed1 = 5;
    static public int speed2 = 7;
    static public int speed3 = 4;
    static public int speed4 = 2;
    static public int speed5 = 2;
    private string method;
    private Sprite lazer;
    private int rocketCooldown = 1;
    private Timer speedtimer;
    private System.Timers.Timer rocketTimer, switchTimer;
    private int shipHP;
    public Ship(int type, bool fromOpponent){
        FromOpponent = fromOpponent;
        Type = type;
        ShowOnTop = true;
        ZIndex = 1;
        switch (type){
            case 1:
                speed = speed1;
                method = null;
                shipHP = 5; //change to global.shipHP or whatever its called
                break;
            case 2:
                speed = speed2;
                method = "boost";
                shipHP = 5; //change to global.shipHP or whatever its called
                break;
            case 3:
                speed = speed3;
                method = "rockets";
                if(Global.OrangeUpgrades[0] == 2) rocketsPierce = true;
                shipHP = 5; //change to global.shipHP or whatever its called
                break;
            case 4:
                speed = speed4;
                method = "laser";
                shipHP = 5; //change to global.shipHP or whatever its called
                break;
            case 5:
                speed = speed5;
                shipHP = 5; //change to global.shipHP or whatever its called
                break;
            default:
                speed = speed5;
                method = null;
                break;
        }
    }

    public override void _Ready(){
        Connect("died", GetNode<Node>("../network_manager"), "_dave_died");
        Connect("crashed", this, "crashedShip");
        if(!(Type == 6)){
            Texture = GD.Load<Texture>("res://game_env/Ships/Ship" + Type + ".png");
            if(Global.IsServer){
                if(!FromOpponent)Texture = GD.Load<Texture>("res://game_env/RightFacingShips/Ship" + Type + ".png");
                if(FromOpponent)Texture = GD.Load<Texture>("res://game_env/LeftFacingShips/Ship" + Type + ".png");
            } else {
                if(!FromOpponent)Texture = GD.Load<Texture>("res://game_env/LeftFacingShips/Ship" + Type + ".png");
                if(FromOpponent)Texture = GD.Load<Texture>("res://game_env/RightFacingShips/Ship" + Type + ".png");
            }
        }
        if(FromOpponent == true){
            GetNode<Bay>("../Bay1").addShip(this);
        }
        if(method != null) typeof(Ship).GetMethod(method).Invoke(this, null);
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
            if(Type < 5 && !FromOpponent && !sentToOpponent){
                EmitSignal("died", Position.y, Type);
                Debug.Print("ship has been sent");
                sentToOpponent = true;
            }
        } 
        if(Position.x >= OS.WindowSize.x + this.Scale.x * Texture.GetWidth()/2 || Position.x <= -1 * this.Scale.x * Texture.GetWidth()/2){
            QueueFree();
            Bay.activeShips.Remove(this);
        }
        if(Global.IsServer){
            if(Position.x - this.Scale.x * Texture.GetWidth()/2 <= 500){
                if(FromOpponent){
                    Debug.Print("Taken Damage OMG :OOOOOOOO!!!!");
                    Global.Health -= 500;
                    QueueFree();
                    Bay.activeShips.Remove(this);
                }
            }
        } else {
            if(Position.x + this.Scale.x * Texture.GetWidth()/2 >= 1420){
                if(FromOpponent){
                    Debug.Print("Taken Damage OMG :OOOOOOOO!!!!");
                    Global.Health -= 500;
                    QueueFree();
                    Bay.activeShips.Remove(this);
                }
            }
        }
        if(Type == 4)
            resizeLaser();
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
                        ship.EmitSignal("crashed",ship.Position.x, ship.Position.y, ship.Type);
                        local.EmitSignal("crashed",local.Position.x, local.Position.y, ship.Type);
                    }
                }
            }
        }catch(Exception e){Debug.Print(e.ToString());}
    }
    private void crashedShip(int x, int y, int type){
        switch(type){
            case 0:
                shipHP -= 1; //rocket dmg do not touch
                break;
            case 1:
                shipHP -= 1; //shipdmg from global
                break;
            case 2:
                shipHP -= 1; //shipdmg from global
                break;
            case 3: 
                shipHP -= 1; //shipdmg from global
                break;
            case 4:
                shipHP -= 1; //shipdmg from global
                break;
            case 5:
                shipHP -= 1; //shipdmg from global
                break;
            case 6:
                shipHP -= 1; //shipdmg from global
                break;
        }
        if(shipHP <= 0){
            QueueFree();
            Bay.activeShips.Remove(this);
        }
    }

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// Abilities!
    /// 

    /******************************************************************************
tree
red (general use)
- up speed 		- up speed 		- able to switch lanes in your area
- up health 		- up base damage 	- up health
- cost 			- cost 			- cost
	
yellow (speed)
- up general speed 	- up general speed 	- up speed (wow)
- up health 		- dash 			- dash speed
- lower cost 		- lower cost 		- lower cost

orange (missl)
- missl damg 		- missl damage 		- percing 
- missl spawn speed 	- misll speed speed 	- misl speed speed
- ship health 		- ship cost 		- ship cost

purpl (lazr)
- lazar dps		- lazar dps 		- carge cannon
- ship damage		- ship damage		- wipe all ships in a lane (no base damage)
- base damage		- base damage 		- shoot through ships (still blocked by shield)

blu  (turtle shel)
- upgrade shield health - upgrade shield health	- two shields (doubles defensive abilities) (ship can no longer pass through)
- upgrade ship health   - upgrade speed		- double shield size (halves def abilities)
- upgrade speed		- upgrade cost 		- upgrade cost

*/
    //YELLOW ABILITY
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
    //RED ABILITY
    public void swapLane(){
        if(FromOpponent){
            switchTimer = new System.Timers.Timer(2000);
            switchTimer.Elapsed += switchLane;
            switchTimer.Start();
        }
    }
    public void switchLane(object sender, System.Timers.ElapsedEventArgs e){
        float newY = Position.y + (new Random().Next(-1,1) * 160);
        while(newY < 11 || newY > 700)
             newY = Position.y + (new Random().Next(-1,1) * 160);
        Position = new Vector2(Position.x,newY);
    }

    //ORANGE ABILITY
    public void rockets(){
        rocketTimer = new System.Timers.Timer(2000);
        rocketTimer.Elapsed += FireRockets;
        rocketTimer.Start();
    }

    private void FireRockets(object sender, System.Timers.ElapsedEventArgs e){
        Rocket rocket = new Rocket(FromOpponent, Position.x + (50 * (Global.IsServer?1:-1)), Position.y, rocketsPierce);
        GetParent().AddChild(rocket);
    }

    //PURPLE ABILITY
    public void laser(){
        lazer = new Sprite{
            Texture = GD.Load<Texture>("res://Lazer.png"),
            Position = new Vector2(Position.x+(Global.IsServer?200:-200), Position.y),
            ZIndex = 0
        };
        GetParent().AddChild(lazer);
    }
    public void resizeLaser(){
        ArrayList shipsInLine = new ArrayList();
        foreach(Ship ship in Bay.activeShips)
            if((ship.Position.y + ship.Texture.GetHeight()) > (lazer.Position.y + lazer.Texture.GetHeight()) && ship.Position.y < lazer.Position.y) shipsInLine.Add(ship);
        Ship nearest = null;
        
        if(shipsInLine.ToArray().Length > 0) 
            nearest = (Ship)shipsInLine.ToArray()[0]; 
        if(nearest != null){
            foreach(Ship ship in shipsInLine)
                if(ship.Position.x < nearest.Position.x) nearest = ship;
            lazer.Scale = new Vector2((nearest.Position.x - lazer.Position.x)/500,lazer.Scale.y);
        } else{
            //0.004 adds 1 pixel to each side. dont ask me how i found it, i probably dont remember. 
            //If you want to change it, only change in multiples of 0.004
            //also bump up the 2nd turnarary by the same ratio
            lazer.Scale = new Vector2(lazer.Scale.x + (float)0.016,1);
            lazer.MoveLocalX(speed * (Global.IsServer?1:-1) + (Global.IsServer?4:-4));
        }
    }

    
    //BLUE ABILITY
    public void shield(){
        if(Type == 5 && FromOpponent != true){
            Ship shield = new Ship(6, false){
                Texture = GD.Load<Texture>("res://game_env/shield.png")
            };
            if (Global.IsServer){
                shield.Position = FromOpponent?new Vector2(Position.x-Texture.GetWidth()-6, Position.y):new Vector2(Position.x+Texture.GetWidth()+6, Position.y);
            } else {
                shield.Position = FromOpponent?new Vector2(Position.x+Texture.GetWidth()+6, Position.y):new Vector2(Position.x-Texture.GetWidth()-6, Position.y);
            } 
            GetNode<Bay>("../Bay1").addShield(shield);
            GetParent().AddChild(shield);
            if(Global.BlueUpgrades[0] == 2){
                Ship shield2 = new Ship(6,false){
                   Texture = GD.Load<Texture>("res://game_env/shield.png")
                };
                if (Global.IsServer){
                shield2.Position = FromOpponent?new Vector2(shield.Position.x-Texture.GetWidth()-6, Position.y):new Vector2(shield.Position.x+Texture.GetWidth()+6, Position.y);
                } else {
                    shield2.Position = FromOpponent?new Vector2(shield.Position.x+Texture.GetWidth()+6, Position.y):new Vector2(shield.Position.x-Texture.GetWidth()-6, Position.y);
                } 
                 GetParent().AddChild(shield2);
            }
        }
    }

}