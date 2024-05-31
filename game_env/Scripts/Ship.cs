using System;
using Godot;
using System.Diagnostics;
using System.Collections;
public class Ship : Sprite, ICloneable{
    //connects to dave died
    [Signal] public delegate void died(int yPos, int type);
    [Signal] public delegate void _core_damaged(int damage);
    [Signal] public delegate void crashed(int damage);
    [Signal] public delegate void lost();
    private int Type;
    private int speed;
    private bool FromOpponent = false;
    private bool sentToOpponent = false;
    static public int RedSpeed = Global.RedSpeed;
    static public int YellowSpeed = Global.YellowSpeed;
    static public int OrangeSpeed = Global.OrangeSpeed;
    static public int PurpleSpeed = Global.PurpleSpeed;
    static public int BlueSpeed = Global.BlueSpeed;
    private bool rocketsPierce = Global.missilePercing;
    private string method;
    private Sprite lazer;
    private Timer speedtimer;
    private System.Timers.Timer rocketTimer, switchTimer;
    private int shipHP;
    public Ship(int type, bool fromOpponent){
        FromOpponent = fromOpponent;
        Type = type;
        ShowOnTop = true;
        ZIndex = 1;
        method = null;
        switch (type){
            case 1:
                speed = Global.RedSpeed;
                if(Global.transferAbility) method = "swapLane";
                shipHP = Global.RedHealth;

                break;
            case 2:
                speed = YellowSpeed;
                if(Global.dashAbility) method = "boost";
                shipHP = Global.YellowHealth; //change to global.shipHP or whatever its called
                break;
            case 3:
                speed = OrangeSpeed;
                method = "rockets";
                shipHP =Global.OrangeHealth; //change to global.shipHP or whatever its called
                break;
            case 4:
                speed = PurpleSpeed;
                method = "laser";
                shipHP =Global.RedHealth; //change to global.shipHP or whatever its called
                break;
            case 5:
                speed = BlueSpeed;
                shipHP = Global.BlueHealth;; //change to global.shipHP or whatever its called
                break;
            default:
                speed = BlueSpeed;
                method = null;
                shipHP = Global.shieldHealth;
                break;
        }
    }

    public override void _Ready(){
        Connect("died", GetNode<Node>("../network_manager"), "_dave_died");
        Connect("crashed", this, "crashedShip");
        Connect("lost", GetNode<Node>("../network_manager"), "lost");
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
            speed = YellowSpeed;
        if(Position.x >= OS.WindowSize.x - Scale.x * Texture.GetWidth()/2 || Position.x <= Scale.x * Texture.GetWidth()/2){
            if(Type < 5 && !FromOpponent && !sentToOpponent){
                EmitSignal("died", Position.y, Type);
                Debug.Print("ship has been sent");
                sentToOpponent = true;
            }
        } 
        if(Position.x >= OS.WindowSize.x + Scale.x * Texture.GetWidth()/2 || Position.x <= -1 * Scale.x * Texture.GetWidth()/2){
            QueueFree();
            Bay.activeShips.Remove(this);
        }
        if(Global.IsServer){
            if(Position.x - Scale.x * Texture.GetWidth()/2 <= 500){
                if(FromOpponent){
                    Debug.Print("Taken Damage OMG :OOOOOOOO!!!!");
                    Global.Health -= shipHP;
                    Global.updateHealth(GetNode<Label>("../health"));
                    if(Global.isDefeated()){
                        GetTree().ChangeScene("res://game_env/Scenes/LoseScene.tscn");
                        EmitSignal("lost");
                    }
                    QueueFree();
                    Bay.activeShips.Remove(this);
                }
            }
        } else {
            if(Position.x + Scale.x * Texture.GetWidth()/2 >= 1420){
                if(FromOpponent){
                    Debug.Print("Taken Damage OMG :OOOOOOOO!!!!");
                    Global.Health -= shipHP;
                    Global.updateHealth(GetNode<Label>("../health"));
                    if(Global.isDefeated()){
                        GetTree().ChangeScene("res://game_env/Scenes/LoseScene.tscn");
                        EmitSignal("lost");
                    }
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
                shipHP -= Global.RedShipDamage; //shipdmg from global
                break;
            case 2:
                shipHP -= Global.OrangeShipDamage; //shipdmg from global
                break;
            case 3: 
                shipHP -= Global.YellowShipDamage; //shipdmg from global
                break;
            case 4:
                shipHP -= Global.RedShipDamage; //shipdmg from global
                break;
            case 5:
                shipHP -= Global.BlueShipDamage; //shipdmg from global
                break;
            case 6:
                shipHP -= 100000000; //shield is insta kill lol
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
        rocketTimer = new System.Timers.Timer(Global.missileSpawnSpeed);
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
                Texture = GD.Load<Texture>(Global.IsServer?"res://shield.png":"res://shieldR.png")
            };
            if(FromOpponent){
                shield = new Ship(6, false){
                Texture = GD.Load<Texture>(Global.IsServer?"res://shieldR.png":"res://shield.png")
            };
            }

            if (Global.IsServer){
                shield.Position = FromOpponent?new Vector2(Position.x-Texture.GetWidth()-6, Position.y):new Vector2(Position.x+Texture.GetWidth()+6, Position.y);
            } else {
                shield.Position = FromOpponent?new Vector2(Position.x+Texture.GetWidth()+6, Position.y):new Vector2(Position.x-Texture.GetWidth()-6, Position.y);
            } 
            GetNode<Bay>("../Bay1").addShield(shield);
            GetParent().AddChild(shield);
            if(Global.doubleShieldAbility){
                Ship shield2 = new Ship(6,false){
                   Texture = GD.Load<Texture>("res://shield.png")
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