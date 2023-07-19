using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    #region Unity
    // Start is called before the first frame update
    void Start()
    {
        ResizePlane();
    }
    #endregion

    #region Private
    private void ResizePlane()
    {
        CameraBoundsResizePlane cameraBoundsResizePlane = new CameraBoundsResizePlane();
        transform.localScale = cameraBoundsResizePlane.GetPlaneScale(transform.position);

        EventController.TriggerEvent(EventID.EVENT_FLOOR_READY, transform);
        Debug.Log("extents : " + transform.GetComponent<MeshFilter>().sharedMesh.bounds.extents);
    }
    #endregion
}
