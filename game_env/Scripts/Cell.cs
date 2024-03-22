using System;
using System.Collections.Generic;

public class Cell<anyType> {
    public int r, c;
    public anyType a;

    public Cell<anyType> Previous { get; internal set; }

    public Cell(int row, int column, Object a1){
        r = row;
        c = column;
        a = (anyType) a1;
    }
    public Cell(){}
    public anyType get(){
        return a;
    }
    public anyType set(anyType a1){
        anyType temp = a;
        a = a1;
        return temp;
    }
    public bool isPosition(int row, int column){
        return r == row && c == column;
    }
    public bool isGreater(int row, int column){return row>r || (row==r && column>c);}
    public String toString(){
        return ""+a;
    }

    public static explicit operator Cell<anyType>(LinkedListNode<Cell<anyType>> v)
    {
        throw new NotImplementedException();
    }
}
