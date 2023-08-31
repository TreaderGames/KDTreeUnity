#define DEBUG_DEFINE

using System.Collections.Generic;
using UnityEngine;

public class GridTilesBuilder : Singleton<GridTilesBuilder>
{
    [SerializeField] GameObject tileTemplate;
    [SerializeField] float objectScale;
    private Vector2 positionOffset;
    int gridHeight, gridLength;

    private Dictionary<int, Vector3> gridPositionCollection = new Dictionary<int, Vector3>();

    public float pObjectScale { get => objectScale; }

    #region Unity
    // Start is called before the first frame update
    //void Start()
    //{
    //    InitTiles();
    //}
    #endregion

    #region private

    private void SetupTiles()
    {
        int count = GetTilesCount();

        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridLength; j++)
            {
                SetupTile(j, i, count);
                count--;
            }
        }
    }

    private void SetupTile(int row, int column, int count)
    {
        Vector3 position = Vector3.zero;
        position.x = row * objectScale + positionOffset.x;
        position.z = column * objectScale + positionOffset.y;
        gridPositionCollection.Add(count, position);

#if DEBUG_DEFINE
        GameObject currentTile;
        currentTile = Instantiate(tileTemplate, transform);
        currentTile.transform.localPosition = position;
        currentTile.name = "Tile_" + count;
#endif
    }
    #endregion

    #region Public
    public void InitTiles(Vector3 offset, Vector2 gridSize)
    {
        positionOffset.x = offset.x;
        positionOffset.y = offset.z;

        gridLength = (int)gridSize.x;
        gridHeight = (int)gridSize.y;

        SetupTiles();
    }
    public int GetTilesCount()
    {
        return gridHeight * gridLength;
    }

    public Vector3 GetPositionForTile(int tileNum)
    {
        if (gridPositionCollection.ContainsKey(tileNum))
        {
            return gridPositionCollection[tileNum];
        }

        return Vector3.zero;
    }
    #endregion
}