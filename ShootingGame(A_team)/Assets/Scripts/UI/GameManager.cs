using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;
    public GameObject MainUI;
    public TextMeshProUGUI gamewintext;
    public GameObject BackButton;
    public int targetScore;
    private ScoreKeeper scorekeeper;
    void Start()
    {
        camera2.SetActive(false);
        gamewintext.gameObject.SetActive(false);
        if (GameObject.Find("Score") != null)
        {
            scorekeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        }
    }

    // Update is called once per frame
    void Update()
    {
     if(scorekeeper.score >= targetScore)
        {
            Win();
        }   
    }

    public void Win()
    {
        camera1.SetActive(false);
        camera2.SetActive(true);
        gamewintext.gameObject.SetActive(true);
        Invoke("delate",1);
        BackButton.SetActive(true);
        MainUI.SetActive(false);
        //Spwaner spwaner = GameObject.Find("Spwaner").GetComponent<Spwaner>();
        //spwaner.enabled = false;
    }

    public void Back()
    {
        SceneManager.LoadSceneAsync(0);
    }
    void delate()
    {
        Time.timeScale = 0;
    }
}
