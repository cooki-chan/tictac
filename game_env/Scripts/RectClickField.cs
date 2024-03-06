using System;

public class RectClickField {
    public int posX;
    public int posY;
    private int width;
    private int height;
    private int centerX;
    private int centerY;
    protected bool inField = false;
    public RectClickField(int x, int y, int w, int h){
        posX = x;
        posY = y;
        width = w;
        height = h;
    }
    public bool isInField(int mouseX, int mouseY){
        return mouseX > posX && mouseX < posX + width && mouseY > posY && mouseY < posY + height;
    }
    public String toString(){
        return "PosX: " + posX + ", PosY: " + posY;
    }
}