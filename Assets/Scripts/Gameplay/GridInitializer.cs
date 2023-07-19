using UnityEngine;

public class GridInitializer : MonoBehaviour
{
    [SerializeField] Vector3 padding;

    #region Unity

    private void OnEnable()
    {
        EventController.StartListening(EventID.EVENT_FLOOR_READY, HandleFloorReady);
    }

    private void OnDisable()
    {
        EventController.StopListening(EventID.EVENT_FLOOR_READY, HandleFloorReady);
    }

    #endregion

    #region Callbacks

    private void HandleFloorReady(object arg)
    {
        Transform floor = (Transform)arg;
        Bounds floorBounds = floor.GetComponent<MeshFilter>().sharedMesh.bounds;
        Vector3 positionOffset = new Vector3(-floorBounds.extents.x * floor.localScale.x, 0, -floorBounds.extents.z * floor.localScale.z);
        positionOffset += padding;

        Debug.Log("Grid position offset: " + positionOffset);
        GridTilesBuilder.Instance.InitTiles(positionOffset);
    }
    #endregion
}
