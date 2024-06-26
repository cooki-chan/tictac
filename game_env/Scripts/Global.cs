using System;
using Godot;
using System.Diagnostics;
using System.Collections;
using System.CodeDom;
public class Global : Node
{
    public static bool IsServer = false;
    public static int Health = 100; //TODO: CHANGE THIS!!!

    static public int RedUpgradePoints = 4;
    static public int YellowUpgradePoints = 4;
    public static int OrangeUpgradePoints = 4;
    public static int BlueUpgradePoints = 4;
    

    //Speed Reference
    static public int RedSpeed = 5;
    static public int YellowSpeed = 7;
    static public int OrangeSpeed = 4;
    static public int PurpleSpeed = 2;
    static public int BlueSpeed = 2;


        //Health Reference
    static public int RedHealth = 10;
    static public int YellowHealth = 5;
    static public int OrangeHealth = 10;
    static public int PurpleHealth = 2;
    static public int BlueHealth = 20;


    //Cost Reference
    static public int[] RedCost = {20,10,10,0};
    static public int[] YellowCost = {10,20,20,0};
    static public int[] OrangeCost = {20,30,10,0};
    static public int[] PurpleCost = {0,0,0,0};
    static public int[] BlueCost = {10, 40,10, 10};

    //Ship vs Ship Damage Ref
    static public int RedShipDamage = 10;
    static public int YellowShipDamage = 10;
    static public int OrangeShipDamage = 10;
    static public int PurpleShipDamage = 10;
    static public int BlueShipDamage = 10;


    //Per Ship Upgrades
    //Red
    static public bool transferAbility = false;


    //Yellow
    static public bool dashAbility = false;
    static public int dashSpeed = 20;


    //Orange
    static public bool missilePercing = false;
    static public int missileDamage = 10;
    static public int missileSpeed = 10;
    static public int missileSpawnSpeed = 2000;


    //Purple
    //haha get de-yeeted lol


    //Blue
    static public int shieldHealth = 10;
    static public bool doubleShieldAbility = false;
    static public bool shieldSizeUpgrade = false;


    //private reference
    private static int[] RedUpgrades = new int [] {0,0,0};
    private static int[] YellowUpgrades = new int [] {0,0,0};
    private static int[] OrangeUpgrades = new int [] {0,0,0};
    private static int[] PurpleUpgrades = new int [] {0,0,0};
    private static int[] BlueUpgrades = new int [] {0,0,0};

    private const int RED = 0;
    private const int YELLOW = 1;
    private const int ORANGE = 2;
    private const int PURPLE = 3;
    private const int BLUE = 4;

    private const int TOP_PATH = 0;
    private const int MID_PATH = 1;
    private const int BOT_PATH = 2;

    

    private static int[, , , ] upgradeCosts = {
            {//red
                {//top
                    {0,50,0,50},//1
                    {50,100,50,100},
                    {500, 50, 200, 200}
                },
                {//mid
                    {50,50,0,10},
                    {100,0,100, 30},
                    {200, 10, 400, 100}
                },
                {//low
                    {50,50,50,0},
                    {100,10, 50, 100},
                    {300,20,300,100}
                }
            },
            {
                {
                    {
                        30, 70, 20, 40
                    },
                    {
                        60,30, 20, 100
                    },
                    {
                        300, 700, 20, 10
                    }
                },
                {
                    {
                        40, 30, 15, 17
                    },
                    {
                        100, 50, 0, 65
                    },
                    {100, 50, 70, 500}
                },
                {
                    {35, 20, 10, 5},
                    {200, 20, 70, 130},
                    {50, 400, 60, 78}
                }
            },
            {
                {
                    {
                        30, 40, 10, 5
                    },
                    {100, 30, 90, 70},
                    {300, 500, 0, 80}
                },
                {
                    {5, 80, 30, 2},{200, 0, 0, 10},{300, 0, 250, 65}
                },
                {
                    {40, 10, 7, 9},{0, 70, 60, 130},{30, 90, 600, 0}
                }
            },
            {
                {
                    {20, 30, 10, 40},{80, 60, 0, 90},{500, 300, 0, 100}
                },
                {
                    {28, 59, 21, 32},{73, 24, 91, 134},{300, 200, 273, 239}
                },
                {
                    {41, 24, 24, 24},{56, 71, 89, 154},{528, 90, 67, 167}
                }
            }

        };

public static void updateHealth(Label healthNode){
    healthNode.Text = "" + Health;
}

    public static void upgrade(int color, int path){
        int[] upgrades = new int [] {0,0,0};

        switch (color)
        {
            case RED:
                upgrades = Global.RedUpgrades;
                break;
            case YELLOW:
                upgrades = Global.YellowUpgrades;
                break;
            case ORANGE:
                upgrades = Global.OrangeUpgrades;
                break;
            case PURPLE:
                upgrades = Global.PurpleUpgrades;
                break;
            case BLUE:
                upgrades = Global.BlueUpgrades;
                break;
        }
        if(upgrades[path] < 3){
            upgrades[path]++;
        }
        
        refreshUpgrades();
        foreach(int i in upgrades){
            GD.Print(i);
        }

    }

    public static bool upgradable(int color, int path){
        int[] temp = new int[]{};
        switch(color){
            case RED:
                temp = new int[]{upgradeCosts[0,path,RedUpgrades[path],0],upgradeCosts[0,path,RedUpgrades[path],2],upgradeCosts[0,path,RedUpgrades[path],2],upgradeCosts[0,path,RedUpgrades[path],3]};
                break;
            case YELLOW:
                temp = new int[]{upgradeCosts[1,path,RedUpgrades[path],0],upgradeCosts[1,path,RedUpgrades[path],2],upgradeCosts[1,path,RedUpgrades[path],2],upgradeCosts[1,path,RedUpgrades[path],3]};
                break;
            case ORANGE:
                temp = new int[]{upgradeCosts[2,path,RedUpgrades[path],0],upgradeCosts[2,path,RedUpgrades[path],2],upgradeCosts[2,path,RedUpgrades[path],2],upgradeCosts[2,path,RedUpgrades[path],3]};
                break;
            case BLUE:
                temp = new int[]{upgradeCosts[3,path,RedUpgrades[path],0],upgradeCosts[3,path,RedUpgrades[path],2],upgradeCosts[3,path,RedUpgrades[path],2],upgradeCosts[3,path,RedUpgrades[path],3]};
                break;
        }
        GD.Print(color);
        if(Generator.build(temp)){
                return true;
            }
            return false;
    }

    private static void refreshUpgrades(){
        //red
        if(RedUpgrades[TOP_PATH] >= 1){
            RedSpeed = 6;
        }
        if(RedUpgrades[TOP_PATH] >= 2){
            RedSpeed = 7;
        }
        if(RedUpgrades[TOP_PATH] >= 3){
            transferAbility = true;
        }

        if(RedUpgrades[MID_PATH] >= 1){
            RedHealth = 12;
        }
        if(RedUpgrades[MID_PATH] >= 2){
            RedHealth = 20;
        }
        if(RedUpgrades[MID_PATH] >= 3){
            RedHealth = 25;
        }

        if(RedUpgrades[BOT_PATH] >= 1){
            RedCost = new int[] {18,9,9,0};
        }
        if(RedUpgrades[BOT_PATH] >= 2){
            RedCost = new int[] {14,7,7,0};
        }
        if(RedUpgrades[BOT_PATH] >= 3){
            RedCost = new int[] {10,5,5,0};
        }

        //orange
        if(YellowUpgrades[TOP_PATH] >= 1){
            YellowSpeed = 8;
        }
        if(YellowUpgrades[TOP_PATH] >= 2){
            YellowSpeed = 10;
        }
        if(YellowUpgrades[TOP_PATH] >= 3){
            YellowSpeed = 13;
        }

        if(YellowUpgrades[MID_PATH] >= 1){
            YellowHealth = 7;
        }
        if(YellowUpgrades[MID_PATH] >= 2){
            dashAbility = true;
        }
        if(YellowUpgrades[MID_PATH] >= 3){
            dashSpeed = 24;
        }

        if(YellowUpgrades[BOT_PATH] >= 1){
            YellowCost = new int[] {8,16,16,0};
        }
        if(YellowUpgrades[BOT_PATH] >= 2){
            YellowCost = new int[] {6,13,13,0};
        }
        if(YellowUpgrades[BOT_PATH] >= 3){
            YellowCost = new int[] {4,9,9,0};
        }

        //orange
        if(OrangeUpgrades[TOP_PATH] >= 1){
            missileDamage = 11;
        }
        if(OrangeUpgrades[TOP_PATH] >= 2){
            missileDamage = 12;
        }
        if(OrangeUpgrades[TOP_PATH] >= 3){
            missilePercing = true;
        }

        if(OrangeUpgrades[MID_PATH] >= 1){
            missileSpawnSpeed = 1800;
        }
        if(OrangeUpgrades[MID_PATH] >= 2){
            missileSpawnSpeed = 1440;
        }
        if(OrangeUpgrades[MID_PATH] >= 3){
            missileSpeed = 12;
        }

        if(OrangeUpgrades[BOT_PATH] >= 1){
            OrangeHealth = 11;
        }
        if(OrangeUpgrades[BOT_PATH] >= 2){
            OrangeCost = new int[] {16,24,8,0};
        }
        if(OrangeUpgrades[BOT_PATH] >= 3){
            OrangeCost = new int[] {11,17,6,0};
        }


        //Blue
        if(BlueUpgrades[TOP_PATH] >= 1){
            shieldHealth = 20;
        }
        if(BlueUpgrades[TOP_PATH] >= 2){
            shieldHealth = 30;
        }
        if(BlueUpgrades[TOP_PATH] >= 3){
            doubleShieldAbility = true;
        }

        if(BlueUpgrades[MID_PATH] >= 1){
            BlueHealth = 15;
        }
        if(BlueUpgrades[MID_PATH] >= 2){
            BlueSpeed = 3;
        }
        if(BlueUpgrades[MID_PATH] >= 3){
            BlueSpeed = 5;
        }

        if(BlueUpgrades[BOT_PATH] >= 1){
            BlueSpeed = 4;
        }
        if(BlueUpgrades[BOT_PATH] >= 2){
            BlueCost = new int[] {8, 38,8, 8};
        }
        if(BlueUpgrades[BOT_PATH] >= 3){
            BlueCost = new int[] {6, 27,6, 6};
        }
    }

        public void refreshButtonNames(){ //TODO COPY AND PASTE THIS MOFO
        Godot.Button red1 = GetNode<Godot.Button>("/root/Control/Control/UpgradeManager/Red/1");
        Godot.Button red2 = GetNode<Godot.Button>("../Control/Control/UpgradeManager/Red/2");
        Godot.Button red3 = GetNode<Godot.Button>("../Control/Control/UpgradeManager/Red/3");
        //red
        if(RedUpgrades[TOP_PATH] + RedUpgrades[MID_PATH] + RedUpgrades[BOT_PATH] < 4){
            if(RedUpgrades[TOP_PATH] >= 0){
                red1.Text = "Speed Upgrade #1";
            }
            if(RedUpgrades[TOP_PATH] >= 1){
                red1.Text = "Speed Upgrade #2";
            }
            if(RedUpgrades[TOP_PATH] >= 2){
                red1.Text = "Ability: Random Lane Switch";
            }
            if(RedUpgrades[TOP_PATH] >= 3){
                red1.Text = "Upgrade Path Finished!";
                red1.Disabled = true;
            }

            if(RedUpgrades[MID_PATH] >= 0){
                red2.Text = "Health Upgrade #1";
            }
            if(RedUpgrades[MID_PATH] >= 1){
                red2.Text = "Health Upgrade #2";
            }
            if(RedUpgrades[MID_PATH] >= 2){
                red2.Text = "Health Upgrade #3";
            }
            if(RedUpgrades[MID_PATH] >= 3){
                red2.Text = "Upgrade Path Finished!";
                red2.Disabled = true;
            }

            if(RedUpgrades[BOT_PATH] >= 0){
                red3.Text = "Cost Upgrade #1";
            }
            if(RedUpgrades[BOT_PATH] >= 1){
                red3.Text = "Cost Upgrade #2";
            }
            if(RedUpgrades[BOT_PATH] >= 2){
                red3.Text = "Cost Upgrade #3";
            }
            if(RedUpgrades[BOT_PATH] >= 3){
                red3.Text = "Upgrade Path Finished!";
                red3.Disabled = true;
            }
        }
        else{
            red1.Disabled = true;
            red1.Text = "Out of Upgrade Points!";
            red2.Disabled = true;
            red2.Text = "Out of Upgrade Points!";
            red3.Disabled = true;
            red3.Text = "Out of Upgrade Points!";
        }

        Godot.Button yellow1 = GetNode<Godot.Button>("../Control/Control/UpgradeManager/Yellow/1");
        Godot.Button yellow2 = GetNode<Godot.Button>("../Control/Control/UpgradeManager/Yellow/2");
        Godot.Button yellow3 = GetNode<Godot.Button>("../Control/Control/UpgradeManager/Yellow/3");

        //yellow
        if(YellowUpgrades[TOP_PATH] + YellowUpgrades[MID_PATH] + YellowUpgrades[BOT_PATH] < 4){
        if(YellowUpgrades[TOP_PATH] >= 0){
            yellow1.Text = "Speed Upgrade #1";
        }
        if(YellowUpgrades[TOP_PATH] >= 1){
            yellow1.Text = "Speed Upgrade #2";
        }
        if(YellowUpgrades[TOP_PATH] >= 2){
            yellow1.Text = "Speed Upgrade #3";
        }
        if(YellowUpgrades[TOP_PATH] >= 3){
            yellow1.Text = "Upgrade Path Finished!";
            yellow1.Disabled = true;
        }

        if(YellowUpgrades[MID_PATH] >= 0){
            yellow2.Text = "Health Upgrade #1";
        }
        if(YellowUpgrades[MID_PATH] >= 1){
            yellow2.Text = "Ability: Dash";
        }
        if(YellowUpgrades[MID_PATH] >= 2){
            yellow2.Text = "Ability Upgrade: Dash Speed";
        }
        if(YellowUpgrades[MID_PATH] >= 3){
            yellow2.Text = "Upgrade Path Finished!";
            yellow2.Disabled = true;
        }

        if(YellowUpgrades[BOT_PATH] >= 0){
            yellow3.Text = "Cost Upgrade #1";
        }
        if(YellowUpgrades[BOT_PATH] >= 1){
            yellow3.Text = "Cost Upgrade #2";
        }
        if(YellowUpgrades[BOT_PATH] >= 2){
            yellow3.Text = "Cost Upgrade #3";
        }
        if(YellowUpgrades[BOT_PATH] >= 3){
            yellow3.Text = "Upgrade Path Finished!";
            yellow3.Disabled = true;
        }
        }else{
            yellow1.Disabled = true;
            yellow1.Text = "Out of Upgrade Points!";
            yellow2.Disabled = true;
            yellow2.Text = "Out of Upgrade Points!";
            yellow3.Disabled = true;
            yellow3.Text = "Out of Upgrade Points!";
        }


        //orange
        Godot.Button orange1 = GetNode<Godot.Button>("../Control/Control/UpgradeManager/Orange/1");
        Godot.Button orange2 = GetNode<Godot.Button>("../Control/Control/UpgradeManager/Orange/2");
        Godot.Button orange3 = GetNode<Godot.Button>("../Control/Control/UpgradeManager/Orange/3");

        //orange
        if(OrangeUpgrades[TOP_PATH] + OrangeUpgrades[MID_PATH] + OrangeUpgrades[BOT_PATH] < 4){
        if(OrangeUpgrades[TOP_PATH] >= 0){
            orange1.Text = "Ability Upgrade: Missile Damage";
        }
        if(OrangeUpgrades[TOP_PATH] >= 1){
            orange1.Text = "Ability Upgrade: Missile Damage #2";
        }
        if(OrangeUpgrades[TOP_PATH] >= 2){
            orange1.Text = "Ability: Missile Piercing";
        }
        if(OrangeUpgrades[TOP_PATH] >= 3){
            orange1.Text = "Upgrade Path Finished!";
            orange1.Disabled = true;
        }

        if(OrangeUpgrades[MID_PATH] >= 0){
            orange2.Text = "Ability Upgrade: Missile Spawn Rate";
        }
        if(OrangeUpgrades[MID_PATH] >= 1){
            orange2.Text = "Ability Upgrade: Missile Spawn Rate 2";;
        }
        if(OrangeUpgrades[MID_PATH] >= 2){
            orange2.Text = "Ability Upgrade: Missile Spawn Rate 3";;
        }
        if(OrangeUpgrades[MID_PATH] >= 3){
            orange2.Text = "Upgrade Path Finished!";
            orange2.Disabled = true;
        }

        if(OrangeUpgrades[BOT_PATH] >= 0){
            orange3.Text = "Health Upgrade";
        }
        if(OrangeUpgrades[BOT_PATH] >= 1){
            orange3.Text = "Cost Upgrade #1";
        }
        if(OrangeUpgrades[BOT_PATH] >= 2){
            orange3.Text = "Cost Upgrade #2";
        }
        if(OrangeUpgrades[BOT_PATH] >= 3){
            orange3.Text = "Upgrade Path Finished!";
            orange3.Disabled = true;
        }
        }else{
            orange1.Disabled = true;
            orange1.Text = "Out of Upgrade Points!";
            orange2.Disabled = true;
            orange2.Text = "Out of Upgrade Points!";
            orange3.Disabled = true;
            orange3.Text = "Out of Upgrade Points!";
        }


        //Blue
        Godot.Button blue1 = GetNode<Godot.Button>("../Control/Control/UpgradeManager/Blue/1");
        Godot.Button blue2 = GetNode<Godot.Button>("../Control/Control/UpgradeManager/Blue/2");
        Godot.Button blue3 = GetNode<Godot.Button>("../Control/Control/UpgradeManager/Blue/3");

        //blue
        if(BlueUpgrades[TOP_PATH] + BlueUpgrades[MID_PATH] + BlueUpgrades[BOT_PATH] < 4){
        if(BlueUpgrades[TOP_PATH] >= 0){
            blue1.Text = "Ability Upgrade: Shield Health";
        }
        if(BlueUpgrades[TOP_PATH] >= 1){
            blue1.Text = "Ability Upgrade: Shield Health";
        }
        if(BlueUpgrades[TOP_PATH] >= 2){
            blue1.Text = "Ability: Stacked Shields";
        }
        if(BlueUpgrades[TOP_PATH] >= 3){
            blue1.Text = "Upgrade Path Finished!";
            blue1.Disabled = true;
        }

        if(BlueUpgrades[MID_PATH] >= 0){
            blue2.Text = "Ship Health Upgrade";
        }
        if(BlueUpgrades[MID_PATH] >= 1){
            blue2.Text = "Speed Upgrade #2";
        }
        if(BlueUpgrades[MID_PATH] >= 2){
            blue2.Text = "Speed Upgrade #2";
        }
        if(BlueUpgrades[MID_PATH] >= 3){
            blue2.Text = "Upgrade Path Finished!";
            blue2.Disabled = true;
        }

        if(BlueUpgrades[BOT_PATH] >= 0){
            blue3.Text = "Speed Upgrade";
        }
        if(BlueUpgrades[BOT_PATH] >= 1){
            blue3.Text = "Cost Upgrade #1";
        }
        if(BlueUpgrades[BOT_PATH] >= 2){
            blue3.Text = "Cost Upgrade #2";
        }
        if(BlueUpgrades[BOT_PATH] >= 3){
            blue3.Text = "Upgrade Path Finished!";
            blue3.Disabled = true;
        }
        }else{
            blue1.Disabled = true;
            blue1.Text = "Out of Upgrade Points!";
            blue2.Disabled = true;
            blue2.Text = "Out of Upgrade Points!";
            blue3.Disabled = true;
            blue3.Text = "Out of Upgrade Points!";
        }


    }
    public static bool isDefeated(){
        return Health <= 0;
    }
    /******************************************************************************
tree
red (general use)
- up speed 		- up speed 		- able to switch lanes in your area
- up health 		- up base damage 	- up health
- cost 			- cost 			- cost
	
orange (speed)
- up general speed 	- up general speed 	- up speed (wow)
- up health 		- dash 			- dash speed
- lower cost 		- lower cost 		- lower cost

orange (missl)
- missl damg 		- missl damage 		- percing 
- missl spawn speed 	- misll speed speed 	- misl speed speed
- ship health 		- ship cost 		- ship cost

blu  (turtle shel)
- upgrade shield health - upgrade shield health	- two shields (doubles defensive abilities) (ship can no longer pass through)
- upgrade ship health   - upgrade speed		- double shield size (halves def abilities)
- upgrade speed		- upgrade cost 		- upgrade cost

*/

}
