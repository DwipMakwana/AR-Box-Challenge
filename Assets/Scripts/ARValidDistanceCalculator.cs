using UnityEngine;

public class ARValidDistanceCalculator : MonoBehaviour
{

    public float GetValidDistance()
    {
        //float objectLength = transform.GetComponent<PlaceObjectInAR>().objectToSpawn.GetComponent<BoxCollider>().size.x;
        //float objectWidth = transform.GetComponent<PlaceObjectInAR>().objectToSpawn.GetComponent<BoxCollider>().size.z;

        //float validDistance = objectLength > objectWidth ? objectLength : objectWidth;

        //return (2 / validDistance);
        //return validDistance;
        return 0.5f;
        
    }
}
