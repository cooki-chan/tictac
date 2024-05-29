using System;
using Godot;
using System.Diagnostics;
using System.Collections;
using System.CodeDom;
public class Global : Node
{
    public static bool IsServer = false;
    public static int Health = 314159265; //TODO: CHANGE THIS!!!

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
    static public int RedHealth = 5;
    static public int YellowHealth = 7;
    static public int OrangeHealth = 4;
    static public int PurpleHealth = 2;
    static public int BlueHealth = 2;


    //Cost Reference
    static public int RedCost = 10;
    static public int YellowCost = 10;
    static public int OrangeCost = 10;
    static public int PurpleCost = 10;
    static public int BlueCost = 10;

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

    private static void refreshUpgrades(){
        //red
        if(RedUpgrades[TOP_PATH] >= 1){
            RedSpeed = 6;
        }
        if(RedUpgrades[TOP_PATH] >= 2){
            RedSpeed = 8;
        }
        if(RedUpgrades[TOP_PATH] >= 3){
            transferAbility = true;
        }

        if(RedUpgrades[MID_PATH] >= 1){
            RedHealth = 15;
        }
        if(RedUpgrades[MID_PATH] >= 2){
            RedHealth = 25;
        }
        if(RedUpgrades[MID_PATH] >= 3){
            transferAbility = true;
        }

        if(RedUpgrades[BOT_PATH] >= 1){
            RedCost = 9;
        }
        if(RedUpgrades[BOT_PATH] >= 2){
            RedCost = 7;
        }
        if(RedUpgrades[BOT_PATH] >= 3){
            RedCost = 5;
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
            YellowHealth = 8;
        }
        if(YellowUpgrades[MID_PATH] >= 2){
            dashAbility = true;
        }
        if(YellowUpgrades[MID_PATH] >= 3){
            dashSpeed = 30;
        }

        if(YellowUpgrades[BOT_PATH] >= 1){
            YellowCost = 9;
        }
        if(YellowUpgrades[BOT_PATH] >= 2){
            YellowCost = 7;
        }
        if(YellowUpgrades[BOT_PATH] >= 3){
            YellowCost = 5;
        }

        //orange
        if(OrangeUpgrades[TOP_PATH] >= 1){
            missileDamage = 20;
        }
        if(OrangeUpgrades[TOP_PATH] >= 2){
            missileDamage = 30;
        }
        if(OrangeUpgrades[TOP_PATH] >= 3){
            missilePercing = true;
        }

        if(OrangeUpgrades[MID_PATH] >= 1){
            missileSpawnSpeed = 1800;
        }
        if(OrangeUpgrades[MID_PATH] >= 2){
            missileSpeed = 15;
        }
        if(OrangeUpgrades[MID_PATH] >= 3){
            missileSpeed = 20;
        }

        if(OrangeUpgrades[BOT_PATH] >= 1){
            OrangeHealth = 6;
        }
        if(OrangeUpgrades[BOT_PATH] >= 2){
            OrangeCost = 9;
        }
        if(OrangeUpgrades[BOT_PATH] >= 3){
            OrangeCost = 8;
        }


        //Blue
        if(BlueUpgrades[TOP_PATH] >= 1){
            shieldHealth = 15;
        }
        if(BlueUpgrades[TOP_PATH] >= 2){
            shieldHealth = 20;
        }
        if(BlueUpgrades[TOP_PATH] >= 3){
            doubleShieldAbility = true;
        }

        if(BlueUpgrades[MID_PATH] >= 1){
            BlueHealth = 15;
        }
        if(BlueUpgrades[MID_PATH] >= 2){
            BlueSpeed = 20;
        }
        if(BlueUpgrades[MID_PATH] >= 3){
            shieldSizeUpgrade = true;
        }

        if(BlueUpgrades[BOT_PATH] >= 1){
            BlueSpeed = 9;
        }
        if(BlueUpgrades[BOT_PATH] >= 2){
            BlueCost = 9;
        }
        if(BlueUpgrades[BOT_PATH] >= 3){
            BlueCost = 8;
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
                red2.Text = "Base Damage Upgrade #1";
            }
            if(RedUpgrades[MID_PATH] >= 2){
                red2.Text = "Health Upgrade #2";
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
            yellow1.Text = "Ability: Random Lane Switch";
        }
        if(YellowUpgrades[TOP_PATH] >= 3){
            yellow1.Text = "Upgrade Path Finished!";
            yellow1.Disabled = true;
        }

        if(YellowUpgrades[MID_PATH] >= 0){
            yellow2.Text = "Health Upgrade #1";
        }
        if(YellowUpgrades[MID_PATH] >= 1){
            yellow2.Text = "Base Damage Upgrade #1";
        }
        if(YellowUpgrades[MID_PATH] >= 2){
            yellow2.Text = "Health Upgrade #2";
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
            orange1.Text = "Speed Upgrade #1";
        }
        if(OrangeUpgrades[TOP_PATH] >= 1){
            orange1.Text = "Speed Upgrade #2";
        }
        if(OrangeUpgrades[TOP_PATH] >= 2){
            orange1.Text = "Ability: Random Lane Switch";
        }
        if(OrangeUpgrades[TOP_PATH] >= 3){
            orange1.Text = "Upgrade Path Finished!";
            orange1.Disabled = true;
        }

        if(OrangeUpgrades[MID_PATH] >= 0){
            orange2.Text = "Health Upgrade #1";
        }
        if(OrangeUpgrades[MID_PATH] >= 1){
            orange2.Text = "Base Damage Upgrade #1";
        }
        if(OrangeUpgrades[MID_PATH] >= 2){
            orange2.Text = "Health Upgrade #2";
        }
        if(OrangeUpgrades[MID_PATH] >= 3){
            orange2.Text = "Upgrade Path Finished!";
            orange2.Disabled = true;
        }

        if(OrangeUpgrades[BOT_PATH] >= 0){
            orange3.Text = "Cost Upgrade #1";
        }
        if(OrangeUpgrades[BOT_PATH] >= 1){
            orange3.Text = "Cost Upgrade #2";
        }
        if(OrangeUpgrades[BOT_PATH] >= 2){
            orange3.Text = "Cost Upgrade #3";
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
            blue1.Text = "Speed Upgrade #1";
        }
        if(BlueUpgrades[TOP_PATH] >= 1){
            blue1.Text = "Speed Upgrade #2";
        }
        if(BlueUpgrades[TOP_PATH] >= 2){
            blue1.Text = "Ability: Random Lane Switch";
        }
        if(BlueUpgrades[TOP_PATH] >= 3){
            blue1.Text = "Upgrade Path Finished!";
            blue1.Disabled = true;
        }

        if(BlueUpgrades[MID_PATH] >= 0){
            blue2.Text = "Health Upgrade #1";
        }
        if(BlueUpgrades[MID_PATH] >= 1){
            blue2.Text = "Base Damage Upgrade #1";
        }
        if(BlueUpgrades[MID_PATH] >= 2){
            blue2.Text = "Health Upgrade #2";
        }
        if(BlueUpgrades[MID_PATH] >= 3){
            blue2.Text = "Upgrade Path Finished!";
            blue2.Disabled = true;
        }

        if(BlueUpgrades[BOT_PATH] >= 0){
            blue3.Text = "Cost Upgrade #1";
        }
        if(BlueUpgrades[BOT_PATH] >= 1){
            blue3.Text = "Cost Upgrade #2";
        }
        if(BlueUpgrades[BOT_PATH] >= 2){
            blue3.Text = "Cost Upgrade #3";
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
