using System;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject targetBallTemplate;
    [SerializeField] int targetBallCount;

    [SerializeField] GameObject whiteBallTemplate;
    [Range(0,99)] [SerializeField] int whitePercentage;
    [SerializeField] Vector3 sphereOffset;

    int whiteBallCount;
    int maxRange;
    int totalCount;

    List<int> avilableIndex = new List<int>();
    List<System.Numerics.Vector3> whiteBallPoints = new List<System.Numerics.Vector3>();


    #region Unity
    private void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_GRID_READY, HandleGridReady);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_GRID_READY, HandleGridReady);
    }

    #endregion

    #region Private
    private void UpdateCount()
    {
        totalCount = GridTilesBuilder.Instance.GetTilesCount();

        whiteBallCount = Mathf.FloorToInt((whitePercentage / 100f) * totalCount);
        if((targetBallCount + whiteBallCount) > totalCount)
        {
            Debug.LogError("Too many target balls");
        }

        maxRange = (totalCount);
        Debug.Log("WhiteBall Count: " + whiteBallCount + " BlueBall Count: " + targetBallCount);
    }

    private void ResetAvilableList()
    {
        for (int i = 0; i < totalCount; i++)
        {
            avilableIndex.Add(i);
        }
    }

    private void SpawnSpheres(bool isWhite)
    {
        int randomIndex;
        int index;
        int count = isWhite ? whiteBallCount : targetBallCount;
        if (isWhite)
        {
            whiteBallPoints.Clear();
        }

        for (int i = 1; i <= count; i++)
        {
            randomIndex = UnityEngine.Random.Range(1, maxRange);
            index = avilableIndex[randomIndex];
            avilableIndex.RemoveAt(randomIndex);

            SpawnAt(isWhite, GridTilesBuilder.Instance.GetPositionForTile(index));
            maxRange--;
        }
    }

    private void SpawnAt(bool isWhite, Vector3 position)
    {
        GameObject currentSphere;
        GameObject template = isWhite ? whiteBallTemplate : targetBallTemplate;
        currentSphere = Instantiate(template, transform);
        currentSphere.transform.localPosition = position + sphereOffset;
        currentSphere.transform.localScale = currentSphere.transform.localScale * GridTilesBuilder.Instance.pObjectScale;

        if(isWhite)
        {
            whiteBallPoints.Add(Utilities.ParseUnityToNumericsVector(currentSphere.transform.localPosition));
        }
    }
    #endregion

    #region Callback
    private void HandleGridReady(object arg)
    {
        UpdateCount();
        ResetAvilableList();
        SpawnSpheres(true);
        SpawnSpheres(false);

        EventController.TriggerEvent(EventID.EVENT_SPAWN_DONE, whiteBallPoints);
    }
    #endregion
}
