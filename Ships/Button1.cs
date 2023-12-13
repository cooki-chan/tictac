using Godot;
using System;

public class Button1 : Button{
    
    // Called when the node enters the scene tree for the first time.
    Ship1 ship;
    public override void _Ready(){}

 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){}
    public void onButtonPressed(){
        Generate temp = GetNode<Generate>("/root/Control/Generate");
        if(temp.build(10)){ 
            ship = new Ship1();
            ulong objID = ship.GetInstanceId();
            ship = (Ship1) GD.InstanceFromId(objID);
            ship.MoveLocalY(300);
            ship.MoveLocalX(200);  
            GetNode<Control>("/root/Control").AddChild(ship); 
            GD.Print("Ship hasth been sumoned");
        }
    }
}
