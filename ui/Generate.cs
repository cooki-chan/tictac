using Godot;
using System;
//generate button
public class Generate : Button
{
    private Generator generator;
    private Texture circ;
    public void onGeneratePressed(){
        circ = GD.Load<Texture>("res://circl.png");
        generator = new Generator(); 
        generator.Texture = circ;
        // ulong objID = generator.GetInstanceId();
        // generator = (Generator) GD.InstanceFromId(objID);
        AddChild(generator);
        generator.MoveLocalY(-100);
        generator.MoveLocalX(-300);
    }
    public override void _Process(float delta){}  

    public bool build(int sub){
        if(generator is Generator)
            return generator.subtract(sub);
        return false;
    }  
}
