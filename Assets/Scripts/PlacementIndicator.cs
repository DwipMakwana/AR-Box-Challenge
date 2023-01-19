using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARValidDistanceCalculator))]
public class PlacementIndicator : MonoBehaviour
{
    ARRaycastManager aRRaycastManager;
    static List<ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();
    
    [HideInInspector]
    public Pose placementPose;

    bool placementPoseValid = false;

    float validDistance;

    [SerializeField]
    GameObject placementIndicator;


    public enum PlacementState
    {
        Valid,
        Invalid
    }
    [HideInInspector]
    public PlacementState placementState = PlacementState.Invalid;


    //Debug
    [Header("DEBUG")]
    [SerializeField]
    UnityEngine.UI.Image indicatorImage;

    
    // Start is called before the first frame update
    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();

        validDistance = GetComponent<ARValidDistanceCalculator>().GetValidDistance();        
    }

    // Update is called once per frame
    void Update()
    {

        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0f));
        try
        {
            aRRaycastManager.Raycast(screenCenter, aRRaycastHits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        }
        catch (System.Exception exception)
        {
            Debug.Log(exception);
        }


        placementPoseValid = aRRaycastHits.Count > 0;

        //Plane detection successful
        if (placementPoseValid)
        {
            placementPose = aRRaycastHits[0].pose;

            //Check distance from camera

            //Valid distance
            if(Vector3.Distance(placementPose.position, Camera.main.transform.position) > validDistance)
            {
                indicatorImage.color = Color.green;
                placementState = PlacementState.Valid;
            }
            //Invalid distance
            else
            {
                placementState = PlacementState.Invalid;
                indicatorImage.color = Color.red;
            }
            

        }
        //Plane detection failed
        else
        {
            placementState = PlacementState.Invalid;
            indicatorImage.color = Color.red;
        }

    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
            placementIndicator.SetActive(false);

    }
}
