using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static void RandomizeArray(Color[] arr)
    {
        for (var i = arr.Length - 1; i > 0; i--)
        {
            var r = Random.Range(0, i);
            var tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }

    public static void SetTransparentColor(float alphaVal, GameObject obj, Color color)
    {

        Color newColor = new Color(color.r, color.g, color.b, alphaVal);
        obj.GetComponent<Renderer>().material.SetColor("_BaseColor", newColor);

    }

}
