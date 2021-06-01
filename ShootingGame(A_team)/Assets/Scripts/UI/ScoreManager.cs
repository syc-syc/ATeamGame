using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score { get; private set; }

    [SerializeField] private float streakExpiredTime = 2.0f;
    private float lastPressTime;
    [SerializeField] private int streakCount;

    public Image comboImage;
    public GameObject comboTextGO;
    public Text ScoreText;
    public Animator anim;

    [Header("Rolling Score")]
    [SerializeField] private float fromScore;
    [SerializeField] private float toScore;
    [SerializeField] private float animationTime;

    // Start is called before the first frame update
    void Start()
    {
        string scorepart = score.ToString();

        streakCount = 0;
        comboTextGO.SetActive(false);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            KillEnemy();
        }
    }

    public void KillEnemy()
    {
        if (Time.time <= lastPressTime + streakExpiredTime)
        {
            streakCount++;
            StartCoroutine("UpdateComboCo");
        }
        else
        {
            //Reset the streakCount
            streakCount = 0;
            streakCount++;
            comboImage.fillAmount = 1;
        }
        StartCoroutine("UpdateComboCo");
        comboTextGO.SetActive(true);
        comboTextGO.transform.GetChild(0).GetComponent<Text>().text = "COMBO X " + "<color=orange>" + "<size=80>" + (streakCount * 100) + "</size>" + "</color>";
        //GetComponent<Text>().text = "COMBO X " + "<color=orange>"+"<size=80>" + (streakCount * 100) + "</size>"+"</color>";

        anim.SetTrigger("Active");

        lastPressTime = Time.time;

        //RollingScore();
    }
}
