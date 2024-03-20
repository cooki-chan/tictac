using Godot;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
public class Generator : Sprite{
    private bool placed = false;
    private int FrameCnt = 0;
    public static Point [] mats = new Point[4];
    private static ArrayList positions = new ArrayList();
    private int type;
    private float adjX, adjY;
    private bool inField = false;
    public Generator(string t){
        if(mats[0].IsEmpty)
            for(int i = 0; i < 4; i++) mats[i] = new Point(0,1);
        switch (t){
            case "elec":{
                type = 0;
                Texture = GD.Load<Texture>("res://TempGen.png");
                break;
            }
            case "carbs":{
                type = 1;
                Texture = GD.Load<Texture>("res://TempGen.png");
                break;
            }
            case "mill":{
                type = 2;
                Texture = GD.Load<Texture>("res://TempGen.png");
                break;
            }
            case "cobble minion":{
                type = 3;
                Texture = GD.Load<Texture>("res://TempGen.png");
                break;
            }
        }
        //this.Scale = new Vector2((float)0.18, (float)0.18);
        
        adjX = (float)(Texture.GetWidth() * 0.49);
        adjY = (float)(Texture.GetHeight() * 0.49);

    }
    //public override void _Ready(){}
 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
        FrameCnt++;
	    if(FrameCnt%60 == 0 && placed){
            mats[type].X += mats[type].Y;
            GetNode<Label>("/root/Control/Resources/Electronics/ECount").Text = "" + mats[0].X;
            GetNode<Label>("/root/Control/Resources/CarbonFiber/CFCount").Text = "" + mats[1].X;
            GetNode<Label>("/root/Control/Resources/Steel/SCount").Text = "" + mats[2].X;
            GetNode<Label>("/root/Control/Resources/DaveDollars/DDCount").Text = ""  +mats[3].X;
            FrameCnt = 0;
        } 
        if(Input.IsActionJustPressed("click") && inField && emptySpace(Position)){
            placed = true;
            positions.Add(Position);
        }
        if(!placed){
            ColorRect field = GetNode<ColorRect>("/root/Control/Village");
            Vector2 mouse = field.GetLocalMousePosition();
            RectClickField [,]  fields = Field.fields;
            for(int i = 0; i < 5; i++)
                for(int j = 0; j < 5; j++){
                    if(fields[i,j].isInField((int)mouse.x, (int)mouse.y)){
                        Position = new Vector2((int)(fields[i,j].posX + fields[i,j].width/2/*(adjX * 0.184)*/), (int)(fields[i,j].posY + fields[i,j].height /2 /*(adjY * 0.184)*/));
                        inField = true;
                    }
                }
            if(!inField) Position = new Vector2(mouse.x, (float)(mouse.y * 0.9));
        }
    }
    public static bool build(int [] nums){
        for(int i = 0; i < 4; i++)
            if(nums[i]>mats[i].X) return false;
        for(int i = 0; i < 4; i++) 
            mats[i].X -= nums[i];
        return true;
    }
    public void setInc(int locType, int amt){
        mats[locType].Y = amt;
    }
    public bool emptySpace(Vector2 pos){
        foreach(Vector2 v in positions)
            if(v.Equals(pos)) return false;
        return true;
    }
}
