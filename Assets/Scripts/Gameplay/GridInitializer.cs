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
        Vector2 gridSize = new Vector2(((floor.localScale.x / GridTilesBuilder.Instance.pObjectScale) * StaticVariables.PLANE_MESH_SIZE) - (padding.x / GridTilesBuilder.Instance.pObjectScale) * 2, ((floor.localScale.z / GridTilesBuilder.Instance.pObjectScale) * StaticVariables.PLANE_MESH_SIZE) - (padding.z / GridTilesBuilder.Instance.pObjectScale) * 2);
        //Vector2 gridSize = new Vector2(((floor.localScale.x / GridTilesBuilder.Instance.pObjectScale) * StaticVariables.PLANE_MESH_SIZE), ((floor.localScale.z / GridTilesBuilder.Instance.pObjectScale) * StaticVariables.PLANE_MESH_SIZE));-

        Debug.Log("z :" + (padding.z / GridTilesBuilder.Instance.pObjectScale));

        positionOffset += padding;

        Debug.Log("Grid position offset: " + positionOffset);
        GridTilesBuilder.Instance.InitTiles(positionOffset, gridSize);
        EventController.TriggerEvent(EventID.EVENT_GRID_READY);
    }
    #endregion
}
