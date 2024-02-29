using Godot;
using System;
public class Generate : Button{
    public void onGeneratePressed(){
        GetNode<ItemList>("/root/Control/Ships").Visible = false;
        GetNode<ItemList>("/root/Control/Generators").Visible = true;
    }
}