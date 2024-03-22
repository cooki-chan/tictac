using Godot;
using System;

public class ShipTransition : Button{
    public void onPressed(){
        GetNode<ItemList>("/root/Control/Ships").Visible = true;
        GetNode<ItemList>("/root/Control/Generators").Visible = false;
    }
}
