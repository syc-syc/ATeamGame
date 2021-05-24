using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shuffle<T>
{
    public static T[] ShuffleTheList(T[] tartgetArray)  //打乱目标数组
    {
        for (int i = 0; i < tartgetArray.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, tartgetArray.Length);
            {
                T temp;
                temp = tartgetArray[i];
                tartgetArray[i] = tartgetArray[randomIndex];
                tartgetArray[randomIndex] = temp;
            }
        }
        return tartgetArray;
    }
}
