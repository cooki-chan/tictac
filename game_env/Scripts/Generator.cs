using Godot;
using System.Drawing;
public class Generator : Sprite{
    private static int matsCount = 0;
    private int FrameCnt = 0;
    public static Point [] mats = new Point[4];
    private int type;
    public Generator(string t){
        for(int i = 0; i < 4; i++) mats[i] = new Point(0,1);
        switch (t){
            case "elec":{
                type = 0;
                break;
            }
            case "carbs":{
                type = 1;
                break;
            }
            case "mill":{
                type = 2;
                break;
            }
            case "cobble minion":{
                type = 3;
                break;
            }
        }
    }
    public override void _Ready(){

    }
//variable movement add
 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
        FrameCnt++;
	   if(FrameCnt%60 == 0){
            mats[type].X += mats[type].Y;
            Node control = GetNode<Control>("/root/Control");
            GetNode<Label>("/root/Control/Resources/Electronics/ECount").Text = ""+mats[0].X;
            GetNode<Label>("/root/Control/Resources/CarbonFiber/CFCount").Text = ""+mats[1].X;
            GetNode<Label>("/root/Control/Resources/Steel/SCount").Text = ""+mats[2].X;
            GetNode<Label>("/root/Control/Resources/DaveDollars/DDCount").Text = ""+mats[3].X;
            FrameCnt = 0;
       } 
    }
    public static bool subtract(int [] nums){
        for(int i = 0; i < 4; i++)
            if(nums[i]>mats[i].X) return false;
        for(int i = 0; i < 4; i++)
            mats[i].X -= nums[i];
        return true;
    }
    public void setInc(int locType, int amt){
        mats[locType].Y = amt;
    }
}
