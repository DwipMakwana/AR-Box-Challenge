using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARController : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Canvas;
    public ARRaycastManager RaycastManager;
    public bool isSpawned = false;

    private void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isSpawned)
        {
            List<ARRaycastHit> Touches = new List<ARRaycastHit>();

            RaycastManager.Raycast(Input.GetTouch(0).position, Touches, TrackableType.Planes);

            if(Touches.Count > 0)
            {
                Canvas.SetActive(false);
                Instantiate(Portal, Touches[0].pose.position, Touches[0].pose.rotation);
                isSpawned = true;
            }
        }
    }
}
