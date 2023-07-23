using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static System.Numerics.Vector3 ParseUnityToNumericsVector(Vector3 unityVector)
    {
        System.Numerics.Vector3 numericsVector = new System.Numerics.Vector3(unityVector.x, unityVector.y, unityVector.z);

        return numericsVector;
    }
}
