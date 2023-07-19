#define DEBUG_DEFINE

using System.Collections.Generic;
using UnityEngine;

public class GridTilesBuilder : Singleton<GridTilesBuilder>
{
    [SerializeField] GameObject tileTemplate;
    private Vector2 positionOffset;
    [SerializeField] int gridHeight, gridLength;

    private Dictionary<int, Vector3> gridPositionCollection = new Dictionary<int, Vector3>();

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
        position.x = row + positionOffset.x;
        position.z = column + positionOffset.y;
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
    public void InitTiles(Vector3 offset)
    {
        positionOffset.x = offset.x;
        positionOffset.y = offset.z;
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