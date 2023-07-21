using System;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject blueBallTemplate;

    [SerializeField] GameObject whiteBallTemplate;
    [Range(0,99)] [SerializeField] int whitePercentage;

    int blueBallCount, whiteBallCount;

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
        int totalCount = GridTilesBuilder.Instance.GetTilesCount();

        whiteBallCount = Mathf.FloorToInt((whitePercentage / 100f) * totalCount);
        blueBallCount = totalCount - whiteBallCount;
        Debug.Log("WhiteBall Count: " + whiteBallCount + " BlueBall Count: " + blueBallCount);
    }
    #endregion

    #region Callback
    private void HandleGridReady(object arg)
    {
        UpdateCount();
    }
    #endregion
}
