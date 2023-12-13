using System;
using Godot;
public class Ship3 : Sprite{

    public override void _Ready(){
        Texture = GD.Load<Texture>("res://Ship3.png");
    }
//variable movement add
 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
        MoveLocalX(15); 
        if(Position.x > OS.WindowSize.x)
           QueueFree();
    }
}
