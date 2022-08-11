using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChecker : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Key" && GameManager.instance.canRestart)
        {
            transform.parent.GetComponent<Box>().HasKey = true;
            GameManager.instance.FlipBoxes();
            GameManager.instance.Answer = gameObject.name;
        }
    }
}
