using UnityEngine;

public class CameraBoundsResizePlane
{

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

        return new Vector3(scale.x, scale.y, scale.z) / StaticVariables.PLANE_MESH_SIZE;
    }

    #endregion
}
