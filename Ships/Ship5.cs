using System;
using Godot;
public class Ship5 : Sprite{

    public override void _Ready(){
        Texture = GD.Load<Texture>("res://Ship2.png");
    }
//variable movement add
 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
        MoveLocalX(5); 
        if(Position.x > OS.WindowSize.x)
           QueueFree();
    }
}
