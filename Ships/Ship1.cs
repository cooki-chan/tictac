using System;
using Godot;
public class Ship1 : Sprite{

    public override void _Ready(){
        Texture = GD.Load<Texture>("res://Ship1.png");
    }
//variable movement add
 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
        MoveLocalX(10); 
        if(Position.x > OS.WindowSize.x)
           QueueFree();
    }
}
