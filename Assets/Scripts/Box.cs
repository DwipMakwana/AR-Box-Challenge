using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Box : MonoBehaviour
{
    public static Box instance;

    public bool HasKey = false;

    private float lerp = 0f, speed = 5f;

    readonly int shuffleSpeed = 100;
    int shuffle = 0;
    static List<Vector3> listOfCupPositions, shuffleList;
    Vector3 theNewPos, startPos;
    TouchPhase touchPhase;

    void Awake()
    {
        instance = this;
    }

    void FlipDone()
    {
        GameManager.instance.Key.SetActive(false);
        GameManager.instance.isShuffling = true;
        UIManager.instance.Shuffling.SetActive(true);
    }

    void PickDone()
    {
        GameManager.instance.ResetRound();       
    }

    void Start()
    {
        if (null == listOfCupPositions)
        {
            // These lists are global to each cup
            listOfCupPositions = new List<Vector3>();
            shuffleList = new List<Vector3>();
        }
        theNewPos = startPos = transform.position;
        listOfCupPositions.Add(theNewPos); // Add this cup to the main list
    }

    void FixedUpdate()
    {
        BoxLerpPosition();
    }

    void Update()
    {
        if (GameManager.instance.canSelect)
        {
            SelectBox();
        }
    }

    void LateUpdate()
    {
        BoxNewPosShufle();
    }

    void BoxLerpPosition()
    {
        if (startPos != theNewPos && GameManager.instance.isShuffling)
        {
            lerp += Time.deltaTime * speed;
            lerp = Mathf.Clamp(lerp, 0f, 1f); // keep lerp between the values 0..1
            transform.position = Vector3.Lerp(startPos, theNewPos, lerp);
            if (lerp >= 1f)
            {
                startPos = theNewPos;
                lerp = 0f;
            }
        }
    }

    void BoxNewPosShufle()
    {
        if (--shuffle <= 0 && GameManager.instance.isShuffling)
        {
            if (GameManager.instance.shuffleCount >= GameManager.instance.TimesToShuffle) // shuffle done
            {
                GameManager.instance.shuffleCount = 0;
                GameManager.instance.isShuffling = false;
                GameManager.instance.canSelect = true;
                UIManager.instance.Shuffling.SetActive(false);
                UIManager.instance.ControlCountdown(true);
                return;
            }
            // Shuffle the cups
            shuffle = shuffleSpeed;
            if (0 == shuffleList.Count)
                shuffleList = listOfCupPositions.ToList(); // refresh shuffle positions

            // Loop until we get a position this cup isn't, 
            // or unless there's only one spot left in shuffle list, 
            // use it (ie don't move this cup this round)
            int index;
            do
            {
                index = Random.Range(0, shuffleList.Count);
            } while (startPos == shuffleList[index] && shuffleList.Count > 1);

            // give this cup a new position
            theNewPos = shuffleList[index];
            shuffleList.RemoveAt(index); // remove position from shuffle list so it isn't duplicated to another cup
            GameManager.instance.shuffleCount++;
        }
    }

    void SelectBox()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) // selecting box
            {
                if (hit.transform.tag == "Box")
                {
                    UIManager.instance.ControlCountdown(false);
                    GameManager.instance.CheckAnswer(hit.transform.name, GameObject.Find(GameManager.instance.Answer).transform.parent.gameObject);
                }
            }
        }
    }
}
