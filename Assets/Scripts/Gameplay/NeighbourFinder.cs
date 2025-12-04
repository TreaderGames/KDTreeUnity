using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NeighbourFinder : MonoBehaviour
{
    [SerializeField] Material nearestSphereMat;
    [SerializeField] bool useKD;

    Dictionary<System.Numerics.Vector3, MeshRenderer> whiteBallCollection = new Dictionary<System.Numerics.Vector3, MeshRenderer>();

    #region Unity
    private void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_SPAWN_DONE, HandleSpawnDone);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_SPAWN_DONE, HandleSpawnDone);
    }

    #endregion

    #region Private

    private System.Numerics.Vector3[] PointArrFromCollection(Dictionary<System.Numerics.Vector3, MeshRenderer> collection)
    {
        System.Numerics.Vector3[] pointArr = new System.Numerics.Vector3[collection.Count];

        int index = 0;

        foreach (var item in collection)
        {
            pointArr[index] = item.Key;
            index++;
        }

        return pointArr;
    }

    #endregion

    #region Private

    private void BruteForceFind(System.Numerics.Vector3[] whiteBallVectors, System.Numerics.Vector3 currentPos)
    {
        float closestDist = System.Numerics.Vector3.Distance(whiteBallVectors[0], currentPos);
        float newDist;
        System.Numerics.Vector3 closestVector = whiteBallVectors[0];

        for (int i = 1; i < whiteBallVectors.Length; i++)
        {
            newDist = System.Numerics.Vector3.Distance(whiteBallVectors[i], currentPos);

            if (closestDist > newDist)
            {
                closestDist = newDist;
                closestVector = whiteBallVectors[i];
            }
        }

       whiteBallCollection[closestVector].material = nearestSphereMat;
    }

    #endregion


    #region Callback
    private void HandleSpawnDone(object arg)
    {
        UnityEngine.Vector3 currentPosition = transform.position;
        //List<System.Numerics.Vector3> pointList = (List<System.Numerics.Vector3>)arg;
        whiteBallCollection = (Dictionary<System.Numerics.Vector3, MeshRenderer>)arg;

        System.Numerics.Vector3[] whiteBallVectors = PointArrFromCollection(whiteBallCollection);
        System.Numerics.Vector3 currentPos = new System.Numerics.Vector3(currentPosition.x, currentPosition.y, currentPosition.z);

        KdTree kdTree = new KdTree(3, whiteBallVectors);

        float[] vectorFloatArr = new float[3];
        vectorFloatArr[0] = currentPosition.x;
        vectorFloatArr[1] = currentPosition.y;
        vectorFloatArr[2] = currentPosition.z;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0; i < 1000; i++)
        {
            if (useKD)
            {
                float[] nearestPoint = kdTree.Search(null, vectorFloatArr).point;
                UnityEngine.Vector3 nearestPointVector = new UnityEngine.Vector3(nearestPoint[0], nearestPoint[1], nearestPoint[2]);
                whiteBallCollection[new System.Numerics.Vector3(nearestPointVector.x, nearestPointVector.y, nearestPointVector.z)].material = nearestSphereMat;

                //UnityEngine.Debug.Log("Curr pos: " + currentPosition + " nearest Pos: " + nearestPointVector);
            }
            else
            {
                BruteForceFind(whiteBallVectors, currentPos);
            }
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log("Time Spent: " + stopwatch.ElapsedMilliseconds);
    }
    #endregion
}
