using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IListExtension
{
    /// <summary>
    /// Shuffle the elements in a list
    /// </summary>
    public static void Shuffle(this IList list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            // Notes: The int version of Random.Range() is min inclusive but max exclusive
            int indexToSwap = Random.Range(0, list.Count);

            Swap(list, i, indexToSwap);
        }
    }

    public static T RandomOne<T>(this IList<T> list)
    {
        if (list.Count == 0)
        {
            return default;
        }

        return list[Random.Range(0, list.Count)];
    }

    public static List<T> ChooseSet<T>(this IList<T> list, int numRequired)
    {
        List<T> result = new List<T>();

        int numToChoose = numRequired;

        for (int numLeft = list.Count; numLeft > 0; numLeft--)
        {
            float prob = (float)numToChoose / (float)numLeft;

            if (Random.value <= prob)
            {
                numToChoose--;
                result.Add(list[numLeft - 1]);

                if (numToChoose <= 0)
                {
                    break;
                }
            }
        }

        return result;
    }

    /// <summary>
	/// Swap two elements in a list
	/// </summary>
	private static void Swap(IList list, int index1, int index2)
    {
        object temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
    }
}