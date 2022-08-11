using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    GameObject[] Boxes;
    [SerializeField]
    GameObject KeyPrefab;

    public GameObject Key;

    public bool isShuffling = false;
    public bool canSelect = false;
    public bool canRestart = false;
    public int TimesToShuffle = 10;
    public int Points = 0;
    public int shuffleCount = 0;
    public string Answer = "";

    public Animator[] BoxesAnimators;
    public Animator KeyAnimator;

    bool answerChecked = false;

    void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
        Application.targetFrameRate = 60;
    }

    void Start()
    {

    }

    public void OnStartClick()
    {        
        KeyAnimator.SetBool("Drop", true);
        foreach (var animator in BoxesAnimators)
        {
            animator.SetBool("Repick", false);
        }
    }

    public void FlipBoxes()
    {
        foreach(var animator in BoxesAnimators)
        {
            animator.SetBool("Repick", false);
            animator.SetBool("Flip", true);
        }
    }

    public void CheckAnswer(string Boxname, GameObject Box)
    {
        if(answerChecked) return;
        answerChecked = true;
        if(Boxname == (Answer + "0"))
        {
            UIManager.instance.CountdownText.text = "Timeout \n +0";
            UIManager.instance.UpdatePoints(0);
        }
        else if (Boxname == Answer)
        {
            UIManager.instance.CountdownText.text = "Correct \n +10";
            UIManager.instance.UpdatePoints(10);
        }
        else
        {
            UIManager.instance.CountdownText.text = "Wrong \n +0";
            UIManager.instance.UpdatePoints(0);
        }
        Box.GetComponent<Animator>().SetBool("Pick", true);
        GameObject Key = Instantiate(KeyPrefab, Box.transform);

        foreach (var key in GameObject.FindGameObjectsWithTag("Key"))
        {
            Destroy(key, 2.0f);
        }
    }

    public void ResetRound()
    {
        canSelect = false;
        Answer = "";
        foreach (var animator in BoxesAnimators)
        {
            animator.SetBool("Flip", false);
            animator.SetBool("Pick", false);
            animator.SetBool("Repick", true);
            animator.gameObject.GetComponent<Box>().HasKey = false;
        }
        UIManager.instance.CountdownText.text = "";
        UIManager.instance.StartBtn.SetActive(true);
        UIManager.instance.BG.SetActive(true);
        Key.SetActive(true);
        Key.GetComponent<MeshRenderer>().enabled = true;
        canRestart = true;
        answerChecked = false;
    }
}
