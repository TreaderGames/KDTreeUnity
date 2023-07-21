using UnityEngine;

public class GridInitializer : MonoBehaviour
{
    [SerializeField] Vector3 padding;
    Vector2 gridSize;

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
        Vector2 gridSize = new Vector2(floor.localScale.x * StaticVariables.PLANE_MESH_SIZE - padding.x, floor.localScale.z * StaticVariables.PLANE_MESH_SIZE - padding.z);

        positionOffset += padding;

        Debug.Log("Grid position offset: " + positionOffset);
        GridTilesBuilder.Instance.InitTiles(positionOffset, gridSize);
        EventController.TriggerEvent(EventID.EVENT_GRID_READY);
    }
    #endregion
}
