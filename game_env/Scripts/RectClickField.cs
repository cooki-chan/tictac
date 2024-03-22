using System;
using Godot;

public class RectClickField {
    public int posX;
    public int posY;
    public int width;
    public int height;
    public RectClickField(int x, int y, int w, int h){
        posX = x;
        posY = y;
        width = w;
        height = h;
    }
    public bool isInField(int mouseX, int mouseY){
        return (mouseX > posX) && (mouseX < (posX + width)) && (mouseY > posY) && (mouseY < (posY + height));
    }
    public String toString(){
        return "PosX: " + posX + ", PosY: " + posY;
    }
}