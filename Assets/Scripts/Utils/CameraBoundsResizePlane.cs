using UnityEngine;

public class CameraBoundsResizePlane
{
    private const int PLANE_MESH_SIZE = 10; //This will never change unless you replace unitys default plane with another one

    Camera mainCamera;

    Vector3 viewPort;
    Vector3 bottomLeft;
    Vector3 topRight;

    #region Public
    public Vector3 GetPlaneScale(Vector3 transformPosition)
    {
        mainCamera = Camera.main;

        //Only considering distance in y direction as other dimensions would need to be handled as position offset
        float distance = mainCamera.transform.position.y - transformPosition.y;

        viewPort.Set(0, 0, distance);
        bottomLeft = mainCamera.ViewportToWorldPoint(viewPort);

        viewPort.Set(1, 1, distance);
        topRight = mainCamera.ViewportToWorldPoint(viewPort);

        Vector3 scale = topRight - bottomLeft;
        scale.y = 1;

        return new Vector3(scale.x, scale.y, scale.z) / PLANE_MESH_SIZE;
    }

    #endregion
}
