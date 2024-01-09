using System;
using System.Linq.Expressions;
using Godot;
public class Ship : Sprite, ICloneable{
    private int Type;
    private LambdaExpression Method;
    private int speed;
    public Ship(int type, LambdaExpression method){
        Type = type;
        Method = method;
        switch (type){
            case 1:
                speed = 10;
                break;
            case 2:
                speed = 15;
                break;
            case 5:
                speed = 5;
                break;
        }
    }
    public override void _Ready(){
        Texture = GD.Load<Texture>("res://Ship" + Type + ".png");
    }
//variable movement add
 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
        MoveLocalX(speed); 
        if(Position.x > OS.WindowSize.x)
           QueueFree();
    }
    public int getType(){
        return Type;
    }

    public object Clone()
    {
        return new Ship(Type, Method);
    }
}
