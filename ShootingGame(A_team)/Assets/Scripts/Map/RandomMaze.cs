using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaze
{
    public bool[,] Maze { get; set; }
    public RandomMaze(int x, int y)
    {
        Maze = new bool[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Maze[i, j] = true;
            }
        }
    }

    public void BarrierMap(int topIndex, int bottomIndex, int rightIndex, int leftIndex)
    {
        int rowIndex, cloumnIndex;
        int top = topIndex, bottom = bottomIndex, right = rightIndex, left = leftIndex;
        if (right - left < 2 || bottom - top < 2)         //当切割到只剩一列时就return
        {
            return;
        }
        //只能在偶数行生成墙壁，为了确保有道路
        //TODO 让地图更自然
        do
        {
            cloumnIndex = UnityEngine.Random.Range(left, rightIndex);//列
            rowIndex = UnityEngine.Random.Range(top, bottomIndex);//行
        }
        while ((rowIndex + 1) % 2 != 0 || (cloumnIndex + 1) % 2 != 0) ;
          
               for (int i=left;i<right;i++)
               {
                   Maze[rowIndex,i] = false;
               }
               for (int j = top; j <bottom; j++)
               {
                   Maze[j,cloumnIndex] = false;
               }
           SetDoor(top, bottom, right, left, rowIndex, cloumnIndex);
   
         BarrierMap(top, rowIndex , cloumnIndex , left);
         BarrierMap(rowIndex+1,bottom, cloumnIndex, left);
         BarrierMap(top, rowIndex , rightIndex, cloumnIndex + 1);
         BarrierMap(rowIndex + 1, bottom, rightIndex, cloumnIndex + 1);    
    }
    void SetDoor(int topIndex, int bottomIndex, int rightIndex, int leftIndex,int rowIndex,int columnIndex)
    {
        int top,bottom,right,left;
        do
        {
            top = UnityEngine.Random.Range(topIndex, rowIndex);
            bottom = UnityEngine.Random.Range(rowIndex, bottomIndex);
            left = UnityEngine.Random.Range(leftIndex, columnIndex );
            right = UnityEngine.Random.Range(columnIndex, rightIndex);
  
        }
        while (top % 2 != 0 || bottom % 2 != 0 || left % 2 != 0 || right % 2 != 0);       
        Maze[rowIndex,right] = true;
        Maze[rowIndex, right-1] = true;
        Maze[rowIndex, left] = true;
        Maze[rowIndex, left+1] = true;
        Maze[top, columnIndex] = true;
        Maze[top+1, columnIndex] = true;
        Maze[bottom-1, columnIndex] = true;
        Maze[bottom, columnIndex] = true; 
    }


}
