using Godot;
using System;
using System.CodeDom.Compiler;

public class Field : ColorRect{
    public static RectClickField [,]  fields = new RectClickField[5,5];
    public override void _Ready(){
        ColorRect rect = GetNode<ColorRect>("/root/Control/Village");
        for(int i = 0; i < 5; i++)
            for(int j = 0; j < 5; j++)
                fields[i,j] = new RectClickField((int)(j / 5.0 * rect.RectSize.x), (int)(i / 5.0 * rect.RectSize.y), (int) (0.2 * rect.RectSize.x),(int) (0.2 * rect.RectSize.y));
        // for(int i = 0; i < 5; i++)
        //     for(int j = 0; j < 5; j++)
        //         GD.Print(fields[i,j].posX + " " + fields[i,j].posY);
    }
    public void EPressed(){
        GetNode<ColorRect>("/root/Control/Village").AddChild(new Generator("elec"));
    }
    public void CFPressed(){
        GetNode<ColorRect>("/root/Control/Village").AddChild(new Generator("carbs"));
    }
    public void SPressed(){
        GetNode<ColorRect>("/root/Control/Village").AddChild(new Generator("mill"));
    }
    public void DDPressed(){
        GetNode<ColorRect>("/root/Control/Village").AddChild(new Generator("cobble minion"));
    }
    /*
    public override void _Draw(){
        for(int i = 0; i < 5; i++){
            for(int j = 0; j < 5; j++){
                RectClickField temp = fields[i,j];
                //DrawLine(new Vector2(temp.posX, temp.posY), new Vector2(temp.posX + temp.width, temp.posY + temp.height), new Color(0,0,0));
                DrawPolygon(new Vector2 [] {new Vector2(temp.posX, temp.posY), new Vector2(temp.posX + temp.width, temp.posY), new Vector2(temp.posX + temp.width, temp.posY + temp.height), new Vector2(temp.posX, temp.posY + temp.height)}, new Color [] {new Color(0,0,0)});
            }
        }
        ColorRect rect = GetNode<ColorRect>("/root/Control/Village");
    }*/
}
