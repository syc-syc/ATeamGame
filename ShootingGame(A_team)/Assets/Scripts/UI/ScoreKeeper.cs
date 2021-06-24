using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
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
        streakCount = 0;
        comboTextGO.SetActive(false);

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
        comboTextGO.transform.GetComponent<Text>().text = "COMBO X " + "<color=orange>" + "<size=75>" + (streakCount * 100) + "</size>" + "</color>";
        //GetComponent<Text>().text = "COMBO X " + "<color=orange>"+"<size=80>" + (streakCount * 100) + "</size>"+"</color>";

        anim.SetTrigger("Active");

        lastPressTime = Time.time;

        RollingScore();
    }

    private void RollingScore()
    {
        fromScore = score;
        toScore = fromScore + streakCount * 100;
        //LeanTween.value(fromScore, toScore, animationTime)
        //         .setEase(LeanTweenType.easeInOutQuart)
        //         .setOnUpdate(UpdateScoreUI);
        LeanTween.value(fromScore, toScore, animationTime)
                 .setEase(LeanTweenType.easeInOutQuart)
                 .setOnUpdate((float _obj) =>
                 {
                     fromScore = _obj;
                     ScoreText.text = "Score:" + _obj.ToString("000000 ") + "/ 2W";

                 });

        score = (int)toScore;
    }

    //private void UpdateScoreUI(float _obj)
    //{
    //    fromScore = _obj;
    //    ScoreText.text = "Score:" + _obj.ToString(" 000000");
    //}

    public IEnumerator UpdateComboCo()
    {
        float percent = 1;
        int currentStreakCount = streakCount;

        while (percent > 0)
        {
            if (currentStreakCount == streakCount)
            {
                percent -= Time.deltaTime / streakExpiredTime;
                comboImage.fillAmount = percent;
            }
            else
            {
                StopCoroutine("UpdateComboCo");
                StartCoroutine("UpdateComboCo");
            }

            yield return null;

        }
        streakCount = 0;
        comboTextGO.gameObject.SetActive(false);
    }
}
