using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static Key instance;

    void Awake()
    {
        instance = this;
    }

    public void DropDone()
    {
        GetComponent<Animator>().SetBool("Drop", false);
    }
}
