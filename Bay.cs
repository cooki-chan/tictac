using Godot;
using System;
public class Bay : ColorRect{
   private static Sprite temp;
   private static Ship ship;
   private int bayNum;
   private Control control;
   public bool inBounds(int x1, int y1){
      return RectPosition.x<=x1 && x1<=(RectPosition.x + RectSize.x) && RectPosition.y<=y1 && y1<=(RectPosition.y + RectSize.y);
   }
   public override void _Ready(){
      control = GetNode<Control>("/root/Control");
   }
   public void onButtonPressed(){
      temp = new Sprite{Texture = GD.Load<Texture>("res://Ship1.png")};
      temp.MoveLocalY(RectPosition.y+71);
      temp.MoveLocalX(RectPosition.x+125);
      ship = new Ship(1,null);
   }
   public void on2Pressed(){
      temp = new Sprite{Texture = GD.Load<Texture>("res://Ship2.png")};
      temp.MoveLocalY(RectPosition.y+71);
      temp.MoveLocalX(RectPosition.x+125);
      ship = new Ship(2,null);
   }
   public void on5Pressed(){
      temp = new Sprite{Texture = GD.Load<Texture>("res://Ship5.png")};
      temp.MoveLocalY(RectPosition.y+71);
      temp.MoveLocalX(RectPosition.x+125);
      ship = new Ship(5,null);
   }
   public void onMouseEnter(){
      if(temp != null){
         control.AddChild(temp);
      }
   }
   public void onMouseExit(){
      if(control.GetChildCount() !=0)
         control.RemoveChild(temp);
   }
    public override void _Process(float delta){
      Generate temp = GetNode<Generate>("/root/Control/Generate");
      if(Input.IsActionJustPressed("click") && ship != null){
         if(inBounds((int)GetViewport().GetMousePosition().x, (int)GetViewport().GetMousePosition().y)){
            GD.Print(RectPosition.y);
            switch (ship.getType()){
                case 1: 
                  if(temp.build(10))//attempts to build a ship for cost of 10
                     genShip();
                  break;
                case 2:
                  if(temp.build(15))//attempts to build a ship for cost of 15
                     genShip();
                  break;
               case 5:
                  if(temp.build(20))//attempts to build a ship for cost of 20
                     genShip();
                  break;
            }
         }
      }
   }
   public void genShip(){
      ulong objID = ship.GetInstanceId();
      ship = (Ship) GD.InstanceFromId(objID);
      Ship newship = (Ship)ship.Clone();
      newship.MoveLocalY(RectPosition.y+71);
      newship.MoveLocalX(RectPosition.x+125);
      control.AddChild(newship); 
      GD.Print("Ship hasth been sumoned");
      ship = null;
      temp.Texture = null;
   }
}
