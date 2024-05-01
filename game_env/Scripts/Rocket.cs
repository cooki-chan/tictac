using System.Collections;
using Godot;
public class Rocket : Sprite{
    [Signal] public delegate void crashed(int damage);
    private int speed = 10;
    private bool FromOpponent;
    public Rocket(bool fromOp, float x, float y){
        FromOpponent = fromOp;
        Position = new Vector2(x,y);
        Texture = GD.Load<Texture>("res://Rocket.png");
    }
    public override void _Process(float delta){
        if(Global.IsServer){
            MoveLocalX(speed * (FromOpponent?-1:1));
            
        } else {
            MoveLocalX(speed * (FromOpponent?1:-1));
        } 
        checkCollision();
    }
    private void checkCollision(){
        ArrayList list = Bay.activeShips;
        try{
            foreach(Ship ship in list){
                float dist = Position.x + Texture.GetWidth() - ship.Position.x;
                if(((dist <= Texture.GetWidth() && dist >= 0) || ((Position.x - ship.Position.x) < Texture.GetWidth() && (Position.x - ship.Position.x) > 0)) && System.Math.Abs(Position.y - ship.Position.y) <= 20){
                    ship.EmitSignal("crashed",ship.Position.x, ship.Position.y);
                    EmitSignal("crashed",Position.x, Position.y);
                }
            }
        }catch(System.Exception){}
    }
}