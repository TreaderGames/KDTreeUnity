using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class NeighbourFinder : MonoBehaviour
{
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

    #region Callback
    private void HandleSpawnDone(object arg)
    {
        List<System.Numerics.Vector3> pointList = (List<System.Numerics.Vector3>)arg;
        KdTree kdTree = new KdTree(3, pointList.ToArray());

        UnityEngine.Vector3 currentPosition = transform.position;

        float[] vectorFloatArr = new float[3];
        vectorFloatArr[0] = currentPosition.x;
        vectorFloatArr[1] = currentPosition.y;
        vectorFloatArr[2] = currentPosition.z;

        float[] nearestPoint = kdTree.Search(null, vectorFloatArr).point;
        UnityEngine.Vector3 nearestPointVector = new UnityEngine.Vector3(nearestPoint[0], nearestPoint[1], nearestPoint[2]);

        Debug.Log("Curr pos: " + currentPosition + " nearest Pos: " + nearestPointVector);
    }
    #endregion
}
