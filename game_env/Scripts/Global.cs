using Godot;
using System;
using System.Numerics;

public class Global : Node
{
    public static bool IsServer = false;
    public static int Health = 314159265; //TODO: CHANGE THIS!!!

    public static int[] RedUpgrades = new int [] {0,0,0};
    public static int[] YellowUpgrades = new int [] {0,0,0};
    public static int[] OrangeUpgrades = new int [] {0,0,0};
    public static int[] PurpleUpgrades = new int [] {0,0,0};
    public static int[] BlueUpgrades = new int [] {0,0,0};
    private Ship this_ship_does_not_exist = new Ship(0, false);

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

purpl (lazr)
- lazar dps		- lazar dps 		- carge cannon
- ship damage		- ship damage		- wipe all ships in a lane (no base damage)
- base damage		- base damage 		- shoot through ships (still blocked by shield)

blu  (turtle shel)
- upgrade shield health - upgrade shield health	- two shields (doubles defensive abilities) (ship can no longer pass through)
- upgrade ship health   - upgrade speed		- double shield size (halves def abilities)
- upgrade speed		- upgrade cost 		- upgrade cost

*/