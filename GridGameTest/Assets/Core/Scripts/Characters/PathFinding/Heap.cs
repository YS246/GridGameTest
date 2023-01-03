using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Heap<T> where T : IHeapItem<T>
{
    private T[] items;
    private int currentItemCount;

    public int Count => currentItemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
        currentItemCount = 0;
    }

    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[item.HeapIndex] = item;
        currentItemCount++;

        SortUp(item);
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        items[0] = items[currentItemCount - 1];
        items[0].HeapIndex = 0;
        currentItemCount--;

        SortDown(items[0]);

        return firstItem;
    }

    public void SortUp(T item)
    {
        if (item.HeapIndex == 0)
        {
            return;
        }

        T parentItem = GetParentItem(item);

        if (parentItem.CompareTo(item) > 0)
        {
            Swap(parentItem, item);
            SortUp(item);
        }
    }

    public void SortDown(T item)
    {
        var result = GetLowestChildItem(item);

        if (result.Exist)
        {
            Swap(item, result.ResultItem);
            SortDown(item);
        }
    }

    public void Swap(T item1, T item2)
    {
        items[item1.HeapIndex] = item2;
        items[item2.HeapIndex] = item1;

        int tempIndex1 = item1.HeapIndex;

        item1.HeapIndex = item2.HeapIndex;
        item2.HeapIndex = tempIndex1;
    }

    public bool Contains(T item)
    {
        if (item.HeapIndex < 0 || item.HeapIndex >= currentItemCount)
        {
            return false;
        }

        return items[item.HeapIndex].Equals(item);
    }

    public void UpdateItem(T item)
    {
        //items[item.HeapIndex] = item;

        SortUp(item);
    }

    public T GetParentItem(T item)
    {
        if (item.HeapIndex == 0)
        {
            return default;
        }

        int parentIndex = (item.HeapIndex - 1) / 2;

        return items[parentIndex];
    }

    public (bool Exist, T ResultItem) GetLowestChildItem(T item)
    {
        int leftChildIndex = item.HeapIndex * 2 + 1;
        int rightChildIndex = item.HeapIndex * 2 + 2;

        if (leftChildIndex < currentItemCount)
        {
            int trySwapIndex = leftChildIndex;

            if (rightChildIndex < currentItemCount)
            {
                if (items[rightChildIndex].CompareTo(items[leftChildIndex]) < 0)
                {
                    trySwapIndex = rightChildIndex;
                }
            }

            if (items[trySwapIndex].CompareTo(item) < 0)
            {
                return (true, items[trySwapIndex]);
            }
        }

        return (false, default);
    }

    public void Clear()
    {
        items = new T[items.Length];
        currentItemCount = 0;
    }
}


public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}