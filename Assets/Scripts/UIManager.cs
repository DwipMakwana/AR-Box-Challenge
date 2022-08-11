using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
        
    public GameObject BG;
    public GameObject StartBtn;
    public GameObject Shuffling;
    public TextMeshProUGUI CountdownText;
    public TextMeshProUGUI PointsText;

    public int CountdownTime = 10;

    Coroutine CountdownCoroutine;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    public void OnButtonClick(string BtnName)
    {
        switch(BtnName)
        {
            case "start":
                GameManager.instance.OnStartClick();
                BG.SetActive(false);
                StartBtn.SetActive(false);
                break;

            case "quit":
                Application.Quit();
                break;

            case "ar mode":
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    SceneManager.LoadScene(1);
                }
                else if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    SceneManager.LoadScene(0);
                }
                break;
        }
    }

    public void ControlCountdown(bool isStart)
    {
        if (isStart)
        {
            CountdownCoroutine = StartCoroutine(ChooseCountdown());
        }
        else
        {
            StopCoroutine(CountdownCoroutine);
        }
    }

    IEnumerator ChooseCountdown()
    {
        GameManager.instance.canRestart = false;

        for(int i = CountdownTime; i > 0; i--)
        {
            CountdownText.text = "Choose the box for the key in " + i;
            yield return new WaitForSeconds(1.0f);
        }

        GameManager.instance.CheckAnswer(GameManager.instance.Answer + "0", GameObject.Find(GameManager.instance.Answer).transform.parent.gameObject);
    }

    public void UpdatePoints(int point)
    {           
        GameManager.instance.Points += point;
        PointsText.text = "Points: " + GameManager.instance.Points.ToString();
    }
}
