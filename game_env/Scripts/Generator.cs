using System;
using Godot;
public class Generator : Sprite{
    private static int matsCount = 0;
    private int FrameCnt = 0;

    //variable movement add
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
        FrameCnt++;
	   if(FrameCnt%60 == 0){
            matsCount++;
            FrameCnt = 0;
            GD.Print(matsCount);
       } 
    }
    public bool subtract(int num){
        if(num>matsCount)return false;
        else matsCount -= num;
        return true;
    }
}
