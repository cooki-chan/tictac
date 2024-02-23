using Godot;
using System;
using System.Drawing;
public class Generator : Sprite{
    private static int matsCount = 0;
    private bool placed = false;
    private int FrameCnt = 0;
    public static Point [] mats = new Point[4];
    private int type;
    public Generator(string t){
        for(int i = 0; i < 4; i++) mats[i] = new Point(0,1);
        switch (t){
            case "elec":{
                type = 0;
                Texture = GD.Load<Texture>("res://dave.png");
                break;
            }
            case "carbs":{
                type = 1;
                Texture = GD.Load<Texture>("res://dave.png");
                break;
            }
            case "mill":{
                type = 2;
                Texture = GD.Load<Texture>("res://dave.png");
                break;
            }
            case "cobble minion":{
                type = 3;
                Texture = GD.Load<Texture>("res://dave.png");
                break;
            }
        }
        this.Scale = new Vector2((float) 0.18, (float) 0.18);
        
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
            GetNode<Label>("/root/Control/Resources/Electronics/ECount").Text = "" + mats[0].X;
            GetNode<Label>("/root/Control/Resources/CarbonFiber/CFCount").Text = "" + mats[1].X;
            GetNode<Label>("/root/Control/Resources/Steel/SCount").Text = "" + mats[2].X;
            GetNode<Label>("/root/Control/Resources/DaveDollars/DDCount").Text = ""  +mats[3].X;
            FrameCnt = 0;
        } 
        if(!placed){
            Position = new Vector2(GetLocalMousePosition().x - (this.Scale.x * Texture.GetWidth() / 2), GetLocalMousePosition().y - (this.Scale.y * Texture.GetHeight() / 2));
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
