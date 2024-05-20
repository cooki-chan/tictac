using Godot;
using System;
using System.Drawing.Drawing2D;
using System.Runtime.ConstrainedExecution;

public class UpgradeManager : Node
{
    private const int RED = 0;
    private const int YELLOW = 1;
    private const int ORANGE = 2;
    private const int PURPLE = 3;
    private const int BLUE = 4;

    private const int TOP_PATH = 0;
    private const int MID_PATH = 1;
    private const int BOT_PATH = 2;
        

    private void upgradeStat(int color, int path){
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

    public override void _Ready()
    {
        foreach(Godot.Sprite color in this.GetChildren()){
            foreach(Godot.Button button in color.GetChildren()){
                Godot.Collections.Array array = new Godot.Collections.Array(){button};  
                

                button.Connect("pressed", this, "PLEASE", array);
            }
        }
    }

    public void PLEASE(Godot.Button button){
        GD.Print(button.GetParent().Name);
    }
/*
tree
red (general use)
- up speed 		- up speed 		    - able to switch lanes in your area
- up health 	- up base damage    - up health
- cost 			- cost 			    - cost
	
yellow (speed)
- up general speed 	- up general speed 	- up speed (wow)
- up health 		- dash 			    - dash speed
- lower cost 		- lower cost 		- lower cost

orange (missl)
- missl damg 		    - missl damage 	    	- percing 
- missl spawn speed 	- misll speed speed 	- misl speed speed
- ship health 	    	- ship cost 	    	- ship cost

purpl (lazr)
- lazar dps	    	- lazar dps 		- carge cannon
- ship damage		- ship damage		- wipe all ships in a lane (no base damage)
- base damage		- base damage 		- shoot through ships (still blocked by shield)

blu  (turtle shel)
- upgrade shield health - upgrade shield health	- two shields (doubles defensive abilities) (ship can no longer pass through)
- upgrade ship health   - upgrade speed	    	- double shield size (halves def abilities)
- upgrade speed	    	- upgrade cost 	    	- upgrade cost

*/
}
