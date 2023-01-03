using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtension
{
    public static List<T> ShuffleCopy<T>(this List<T> list)
    {
        List<T> listCopy = new List<T>(list);

        listCopy.Shuffle();

        return listCopy;
    }
}
