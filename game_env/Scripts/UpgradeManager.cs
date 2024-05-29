using Godot;
using System;
using System.Collections;
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
        Global.upgrade(color, path);    
    }

    public override void _Ready()
    {
        GetNode<Global>("../../Global").refreshButtonNames();
        foreach(Godot.Sprite color in this.GetChildren()){
            foreach(Godot.Button button in color.GetChildren()){
                Godot.Collections.Array array = new Godot.Collections.Array(){button};  
                button.Connect("pressed", this, "PLEASE", array);
            }
        }
    }

    public void PLEASE(Godot.Button button){
        int color = -1;
        int path = Convert.ToInt32(button.Name)-1;

        switch(button.GetParent().Name){
            case "Red":
                color = RED;
                Global.RedUpgradePoints--;
                GetNode<Label>("/Red/redPointLabel").Text = "Red Upgrade Points: " + Convert.ToString(Global.RedUpgradePoints); 
                break;
            case "Yellow":
                color = YELLOW;
                Global.YellowUpgradePoints--;
                GetNode<Label>("/Yellow/yellowPointLabel").Text = "Yellow Upgrade Points: " + Convert.ToString(Global.YellowUpgradePoints); 
                break;
            case "Orange":
                color = ORANGE;
                Global.OrangeUpgradePoints--;
                GetNode<Label>("/Orange/orangePointLabel").Text = "Orange Upgrade Points: " + Convert.ToString(Global.OrangeUpgradePoints); 
                break;
            case "Blue":
                color = BLUE;
                Global.BlueUpgradePoints--;
                GetNode<Label>("/Blue/bluePointLabel").Text = "Blue Upgrade Points: " + Convert.ToString(Global.BlueUpgradePoints); 
                break;
            
        }

        Global.upgrade(color, path);
        GetNode<Global>("../../Global").refreshButtonNames();
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
