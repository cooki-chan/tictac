using Godot;
using System;

public class Button5 : Button{
    // Called when the node enters the scene tree for the first time.
    Ship5 ship;
    public override void _Ready(){}

 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){}
    public void onButtonPressed(){
        Generate temp = GetNode<Generate>("/root/Control/Generate");
        if(temp.build(10)){ 
            ship = new Ship5();
            ulong objID = ship.GetInstanceId();
            ship = (Ship5) GD.InstanceFromId(objID);
            ship.MoveLocalY(300);
            ship.MoveLocalX(200);  
            GetNode<Control>("/root/Control").AddChild(ship); 
            GD.Print("Ship hasth been sumoned");
        }
    }
}
