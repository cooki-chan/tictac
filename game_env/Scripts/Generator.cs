using Godot;
using System;
using System.Diagnostics;
using System.Drawing;
public class Generator : Sprite{
    private static int matsCount = 0;
    private bool placed = false;
    private int FrameCnt = 0;
    public static Point [] mats = new Point[4];
    private RectClickField [,]  fields = new RectClickField[5,5];
    private int type;
    private float adjX, adjY;
    public Generator(string t){
        ColorRect rect = GD.Load<ColorRect>("Field");
        for(int i = 0; i < 4; i++) mats[i] = new Point(0,1);
        for(int i = 0; i< fields.Length; i++)
            for(int j = 0; j < fields.Length; j++)
                fields[i,j] = new RectClickField(rect.RectPosition.x * (i / 5 * rect.getWidth()), rect.RectPosition.y,);
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
        this.Scale = new Vector2((float)0.18, (float)0.18);
        
        adjX = (float)(Texture.GetWidth() * 0.49);
        //adjY = this.Scale.y * Texture.GetHeight();

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
            ColorRect field = GetNode<ColorRect>("/root/Control/Village");
            bool check = false;
            Vector2 mouse = GetViewport().GetMousePosition();
            //Debug.Print("mouse:" + mouse.ToString());
            //Debug.Print("field:" + field.RectPosition.ToString());+            
            // if(mouse.x < field.RectPosition.x){
            //     Position = new Vector2(field.RectPosition.x, Position.y);
            //     check = true;
            // }
            // if(mouse.x > field.RectPosition.x){
            //     Position = new Vector2(field.RectPosition.x + field.RectSize.x, Position.y);
            //     check = true;
            // }
            // if(mouse.y < field.RectPosition.y){
            //     Position = new Vector2(Position.x, field.RectPosition.y);
            //     check = true;
            // }
            // if(mouse.y > field.RectPosition.y){
            //     Position = new Vector2(Position.x, field.RectPosition.y + field.RectSize.y);
            //     check = true;
            // }
            // if(!check)
            Position = new Vector2(mouse.x - (float)(Texture.GetWidth() * 0.49) ,(float)(mouse.y * 0.9));
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
}
