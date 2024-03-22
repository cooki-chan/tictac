using System;
using Godot;
using System.Collections.Generic;
public class SparseMatrix <anyType> : Node2D{
    private LinkedList<Cell<anyType>> list;
    private int numRow, numCols;
    public string blank = "~";
    public SparseMatrix (int r, int c){
        list = new LinkedList<Cell<anyType>>();
        numRow = r;
        numCols = c;
    }
    public anyType get(int r, int c) {
        foreach(Cell<anyType> C in list){
            if(C.isPosition(r,c)) return C.a;
        }
        return default(anyType);
    }
    public anyType set(int r, int c, object x) {
        LinkedList<Cell<anyType>>.Enumerator Enum = list.GetEnumerator();
        while (Enum.MoveNext()) {
            Cell<anyType> res = Enum.Current;
            if(res.isPosition(r,c)){
                res = new Cell<anyType>(r, c, x);
                return res.a;
            }
        }
        return default(anyType);
    }
    //test need to try
    public void main(){
         SparseMatrix<string> sm = new SparseMatrix<string>(5,5);
         sm.add(2, 1, "A");
         sm.add(0, 4, "B");
         sm.add(3, 3, "C");
         string temp1 = sm.get(2, 1);			//get the element at row 2, col 1
         string temp2 = sm.set(0, 4, "D");	//change the element at row 0, col 4 to a D, return the old value
         string temp3 = sm.remove(3, 3);		//remove the element at row 3, col 3 and return its value
         
         Console.WriteLine(sm);					//show the contents of the sparse matrix
      
         /*
      	should display something like:
      									0 1 2 3 4
      	- - - - D		or		 0         D
      	- - - - -             1                  
      	- A - - - 				 2   A      
      	- - - - -				 3          
      	
      	*/
         
         Console.WriteLine(temp1);		//should be A
         Console.WriteLine(temp2);		//should be B
         Console.WriteLine(temp3);		//should be C
    }
    public bool add(int r, int c, object x) {
        if (list.Count == 0){
            list.AddLast(new Cell<anyType>(r, c, x));
            return true;
        }
        LinkedList<Cell<anyType>>.Enumerator Enum = list.GetEnumerator();
        while (Enum.MoveNext()) {
            Cell<anyType> res = Enum.Current;
            if(res.isGreater(r,c)){
                list.AddLast(new Cell<anyType>());


                // for (int j = list.Count-1; j > i; j--){
                //      list.set(j, list.get(j - 1));
                // }


                var el = list.Last;
                while (!el.Equals(res)) {
                    // el.Value = el.Previous.Value;
                    el = el.Previous;
                }


                res = new Cell<anyType>(r, c, x);
                return true;
            }
        }
        list.AddFirst(new Cell<anyType>(r,c,x));
        return false;
    }
    public anyType remove(int r, int c) {
        LinkedList<Cell<anyType>>.Enumerator Enum = list.GetEnumerator();
        while (Enum.MoveNext()) {
            Cell<anyType> res = Enum.Current;
            if(res.isPosition(r,c)){
                list.Remove(res);
                return res.a;
            }
        }
        return default(anyType);
    }
    public int size() { return list.Count;}
    public int numRows() { return numRow;}
    public int numColumns() {return numCols;}
    public int getKey(Cell<anyType> C){return C.r * numCols + C.c;}
    public String toString(){
        String array = "";
        int last = 0;
        foreach (Cell<anyType> C in list) {
            while(last < getKey(C)){
                array += blank;
                last++;
            }
            array += C.toString();
        }
        for(int i = array.Length; i<numCols*numRow; i++) array += blank;
        for(int i = array.Length-1; i>=numCols; i--){
            if(i%numCols == 0 && i>0) array = array.Substring(0,i) + "\n" + array.Substring(i);
        }
        for(int i = array.Length-1; i>=0; i--){
            array = array.Substring(0,i)+ " " + array.Substring(i);
        }
        return array;
    }
    public bool contains(anyType e){
        foreach(Cell<anyType> C in list) if(C.Equals(e))return true;
        return false;
    }
    public int[] getLocation(anyType e){
        foreach(Cell<anyType> C in list) if(C.Equals(e))return new int[]{C.r, C.c};
        return null;
    }
    public object[,] toArray(){
        object [,] objects = new object[numRow, numCols];
        foreach(Cell<anyType> C in list) objects[C.r,C.c] = C;
        return objects;
    }
    public bool isEmpty(){
        return list.Count == 0;
    }
    public void clear(){
        list = new LinkedList<Cell<anyType>>();
    }
    public void setBlank(String s){
        blank = s;
    }
}
