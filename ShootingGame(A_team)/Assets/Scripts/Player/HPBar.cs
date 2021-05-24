using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    private Image hpBar;
    private Image hpBarBk;
    public Text Hptext;
    public float hurtSpeed;
   private float maxHp;
    private float currentHP;


    private void Start()
    {
        hpBar = this.transform.GetChild(1).gameObject.GetComponent<Image>();
        hpBarBk = this.transform.GetChild(0).gameObject.GetComponent<Image>();
    }

    public void SetHpCanvas(float maxHpValue)
    {
        maxHp = maxHpValue;
        Hptext.text = maxHp.ToString() + "/" + maxHp.ToString();
        currentHP = maxHp;
    }
    public void SetHpCanvas(float maxHpValue,float currentHpvalue)
    {
        maxHp = maxHpValue;
        currentHP = currentHpvalue;
        Hptext.text = currentHP.ToString() + "/" + maxHp.ToString();
    }
    public void GetDamage(int value)
    {
        currentHP -= value;
        Hptext.text = currentHP.ToString() + "/" + maxHp.ToString();
        StartCoroutine("UpdateHpCo");
    }
    public void UpdateCurrentHp(int value)
    {
        currentHP = value;
        Hptext.text = currentHP.ToString() + "/" + maxHp.ToString();
        StartCoroutine("UpdateHpCo");
    }

    IEnumerator UpdateHpCo()
    {
        hpBar.fillAmount = currentHP / maxHp;
        while (hpBarBk.fillAmount>=hpBar.fillAmount)
        {
            hpBarBk.fillAmount -= hurtSpeed;
            yield return new WaitForSeconds(0.005f);
        }
        if (hpBarBk.fillAmount < hpBar.fillAmount)
        {
            hpBarBk.fillAmount = hpBar.fillAmount;
        }

    }
}
