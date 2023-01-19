using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARController : MonoBehaviour
{
    public GameObject Model;
    public GameObject SpawnedModel;
    public GameObject Canvas;
    public Button ChangeButton;
    public ARRaycastManager RaycastManager;
    public ARAnchorManager AnchorManager;
    public ARPlaneManager PlaneManager;
    public PlacementIndicator placementIndicator;
    public bool isSpawned = false;
    public int modelNo = 1;

    private void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isSpawned)
        {
            List<ARRaycastHit> Touches = new List<ARRaycastHit>();

            RaycastManager.Raycast(Input.GetTouch(0).position, Touches, TrackableType.Planes);

            if(Touches.Count > 0)
            {
                Canvas.transform.GetChild(1).transform.gameObject.SetActive(false);
                Canvas.transform.GetChild(2).transform.gameObject.SetActive(false);

                SpawnedModel = Instantiate(Model, Touches[0].pose.position, Touches[0].pose.rotation);
				SpawnedModel.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
				
                isSpawned = true;

                foreach (var plane in PlaneManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }

                PlaneManager.enabled= false;
            }
        }
    }

    public void ModelChange()
    {
        if (isSpawned)
        {
            modelNo = modelNo == 1 ? 2 : 1;

            SpawnedModel.transform.GetChild(0).gameObject.SetActive(modelNo == 1);
            SpawnedModel.transform.GetChild(1).gameObject.SetActive(modelNo == 2);
        }
    }
}
