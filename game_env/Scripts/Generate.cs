using Godot;
using System;
//generate button
public class Generate : Button{
    private Generator generator;
    public void onGeneratePressed(){
        GetNode<ItemList>("/root/Control/Ships").Visible = false;
        GetNode<ItemList>("/root/Control/Generators").Visible = true;
    }
    public override void _Process(float delta){}  

    public bool build(int [] sub){
        if(generator is Generator)
            return Generator.subtract(sub);
        return false;
    }  
}
    /* 
        circ = GD.Load<Texture>("res://icon.png");
        generator = new Generator("elec"); 
        generator.Texture = circ;
        AddChild(generator);
        generator.MoveLocalY(300);
        generator.MoveLocalX(200);
    */