using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateGameobject : MonoBehaviour
{
    public GameObject Object_to_Rotate;
    public float touchSpeed = 5f;
    

    private Touch inittouch;

    private float roty = 0f; 


    // Update is called once per frame
    void Update()
    {
        if(IsPointerOverUIObject())
        {            
            return;
        }
        else
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    inittouch = touch;

                }
                if (touch.phase == TouchPhase.Moved)
                {
                    roty += Input.GetTouch(0).deltaPosition.x * touchSpeed * Mathf.Deg2Rad;

                    //roty = Mathf.Clamp(roty, -350, 350);

                    this.Object_to_Rotate.transform.eulerAngles = new Vector3(Object_to_Rotate.transform.eulerAngles.x, -roty, Object_to_Rotate.transform.eulerAngles.z);


                }

            }
        }
       

    }
    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
