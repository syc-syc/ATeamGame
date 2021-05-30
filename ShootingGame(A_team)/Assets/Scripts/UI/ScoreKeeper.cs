using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance;
    public Text scoretext;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoretext.text = "SCORE:" + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = "SCORE:" + score.ToString();
    }
}
