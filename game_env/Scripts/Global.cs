using Godot;
using System;
using System.Numerics;

public class Global : Node
{
    public static bool IsServer = false;
    public static int Health = 1; //TODO: CHANGE THIS!!!

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
        upgrades[path]++;


    }

    private static void refresshUpgrades(){
        //red
        if(RedUpgrades[TOP_PATH] >= 1){
            RedSpeed += 1;
        }
        if(RedUpgrades[TOP_PATH] >= 2){
            RedSpeed += 2;
        }
        if(RedUpgrades[TOP_PATH] >= 3){
            transferAbility = true;
        }

        if(RedUpgrades[MID_PATH] >= 1){
            RedHealth += 100;
        }
        if(RedUpgrades[MID_PATH] >= 2){
            RedHealth += 100;
        }
        if(RedUpgrades[MID_PATH] >= 3){
            transferAbility = true;
        }

        if(RedUpgrades[BOT_PATH] >= 1){
            RedCost -= 1;
        }
        if(RedUpgrades[BOT_PATH] >= 2){
            RedCost -= 1;
        }
        if(RedUpgrades[BOT_PATH] >= 3){
            RedCost -= 1;
        }

        //yellow
        if(YellowUpgrades[TOP_PATH] >= 1){
            YellowSpeed += 1;
        }
        if(YellowUpgrades[TOP_PATH] >= 2){
            YellowSpeed += 1;
        }
        if(YellowUpgrades[TOP_PATH] >= 3){
            YellowSpeed += 1;
        }

        if(YellowUpgrades[MID_PATH] >= 1){
            YellowHealth += 100;
        }
        if(YellowUpgrades[MID_PATH] >= 2){
            dashAbility = true;
        }
        if(YellowUpgrades[MID_PATH] >= 3){
            dashSpeed += 30;
        }

        if(YellowUpgrades[BOT_PATH] >= 1){
            YellowCost -= 1;
        }
        if(YellowUpgrades[BOT_PATH] >= 2){
            YellowCost -= 1;
        }
        if(YellowUpgrades[BOT_PATH] >= 3){
            YellowCost -= 1;
        }

        //orange
        if(OrangeUpgrades[TOP_PATH] >= 1){
            missileDamage += 10;
        }
        if(OrangeUpgrades[TOP_PATH] >= 2){
            missileDamage += 10;
        }
        if(OrangeUpgrades[TOP_PATH] >= 3){
            missilePercing = true;
        }

        if(OrangeUpgrades[MID_PATH] >= 1){
            missileSpawnSpeed -= 100;
        }
        if(OrangeUpgrades[MID_PATH] >= 2){
            missileSpeed += 5;
        }
        if(OrangeUpgrades[MID_PATH] >= 3){
            missileSpeed += 5;
        }

        if(OrangeUpgrades[BOT_PATH] >= 1){
            OrangeHealth += 10;
        }
        if(OrangeUpgrades[BOT_PATH] >= 2){
            OrangeCost -= 1;
        }
        if(OrangeUpgrades[BOT_PATH] >= 3){
            OrangeCost -= 1;
        }


        //Blue
        if(BlueUpgrades[TOP_PATH] >= 1){
            shieldHealth += 10;
        }
        if(BlueUpgrades[TOP_PATH] >= 2){
            shieldHealth += 10;
        }
        if(BlueUpgrades[TOP_PATH] >= 3){
            doubleShieldAbility = true;
        }

        if(BlueUpgrades[MID_PATH] >= 1){
            BlueHealth += 10;
        }
        if(BlueUpgrades[MID_PATH] >= 2){
            BlueSpeed += 10;
        }
        if(BlueUpgrades[MID_PATH] >= 3){
            shieldSizeUpgrade = true;
        }

        if(BlueUpgrades[BOT_PATH] >= 1){
            BlueSpeed += 10;
        }
        if(BlueUpgrades[BOT_PATH] >= 2){
            BlueCost -= 1;
        }
        if(BlueUpgrades[BOT_PATH] >= 3){
            BlueCost -= 1;
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
	
yellow (speed)
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
