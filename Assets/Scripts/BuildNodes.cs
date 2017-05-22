using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildNodes : MonoBehaviour {

    public static Transform[] points;

    void Awake()
    {
        points = new Transform[transform.childCount];
        for (var i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
