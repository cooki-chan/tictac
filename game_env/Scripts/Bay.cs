using Godot;
using System;
using System.Collections;
using System.Numerics;
public class Bay : ColorRect{
   private static Sprite temp;
   private static Ship ship;
   public static ArrayList activeShips = new ArrayList();
   private int bayNum;
   private Control control;
   private String SpritePath;
   public bool inBounds(int x1, int y1){
      return RectPosition.x<=x1 && x1<=(RectPosition.x + RectSize.x) && RectPosition.y<=y1 && y1<=(RectPosition.y + RectSize.y);
   }
   public Bay(){
      String name = Name;
      //bayNum = Convert.ToInt32(Name.Substring(Name.Length));
      
   }
   public override void _Ready(){
      control = GetNode<Control>("/root/Control");
   }
   public void onButtonPressed(){
      temp = new Sprite{Texture = GD.Load<Texture>(SpritePath + "Ship1.png")};
      ship = new Ship(1, bayNum, null, false);
   }
   public void on2Pressed(){
      temp = new Sprite{Texture = GD.Load<Texture>(SpritePath + "Ship2.png")};
      ship = new Ship(2, bayNum, null, false);
   }
   public void on3Pressed(){
      temp = new Sprite{Texture = GD.Load<Texture>(SpritePath + "Ship3.png")};
      ship = new Ship(3, bayNum, null, false);
   }
   public void on4Pressed(){
      temp = new Sprite{Texture = GD.Load<Texture>(SpritePath + "Ship4.png")};
      ship = new Ship(4, bayNum, null, false);
   }
   public void on5Pressed(){
      temp = new Sprite{Texture = GD.Load<Texture>(SpritePath + "Ship5.png")};
      ship = new Ship(5, bayNum, null, false);
   }
   public void onMouseEnter(){
      if(temp != null){
         temp.Position = new Godot.Vector2(RectPosition.x+125,RectPosition.y+71);
         control.AddChild(temp);
      }
   }
   public void onMouseExit(){
      if(temp != null && control.GetChildCount() != 0 && control.IsAParentOf(temp)){
         control.RemoveChild(temp);
         temp.Position = new Godot.Vector2(temp.Position.x,RectPosition.y-71);
      }
   }
    public override void _Process(float delta){
      if(Global.IsServer){
            SpritePath = "res://game_env/RightFacingShips/";
        } else {
            SpritePath = "res://game_env/LeftFacingShips/";
        }
      if(Input.IsActionJustPressed("click") && ship != null){
         temp.Texture = null;
         if(inBounds((int)GetViewport().GetMousePosition().x, (int)GetViewport().GetMousePosition().y)){
            switch (ship.getType()){
               case 1: 
                  if(Generator.build(new int [] {0,0,0,0})) // order is Electronics, Carbon Fiber, Steel, DaveDollarsTM
                     genShip();
                  break;
               case 2:
                  if(Generator.build(new int [] {0,0,0,0}))
                     genShip();
                  break;
               case 3:
                  if(Generator.build(new int [] {0,0,0,0}))
                     genShip();
                  break;
               case 4:
                  if(Generator.build(new int [] {0,0,0,0}))
                     genShip();
                  break;
               case 5:
                  if(Generator.build(new int [] {0,0,0,0}))
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
      activeShips.Add(newship);
      ship = null;
      temp.Texture = null;
   }
}
