using Godot;
using System;

public class Field : ColorRect{
    public override void _Ready(){
        SparseMatrix<string> sparseMatrix = new SparseMatrix<string>(8,8);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
