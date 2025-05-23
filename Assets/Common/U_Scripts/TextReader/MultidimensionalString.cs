using UnityEngine;
using System.Collections;

[System.Serializable]
public class MultidimensionalString
{
    [TextArea(2, 5)]
    public string[] stringArray = new string[0];

    public string this[int index] {
        get {
            return stringArray[index];
        }

        set { 
            stringArray[index] = value; 
        }
    }

    public int Length {
        get {
            return stringArray.Length;
        }
    }

    public long LongLength {
        get {
            return stringArray.LongLength;
        }
    }
}
