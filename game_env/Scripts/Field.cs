using Godot;
using System;

public class Field : ColorRect{
    public override void _Ready(){
        //SparseMatrix<string> sparseMatrix = new SparseMatrix<string>(8,8);
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
}
