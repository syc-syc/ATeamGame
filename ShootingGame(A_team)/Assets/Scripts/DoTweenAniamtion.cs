using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DoTweenAniamtion:MonoBehaviour
{
    public static DoTweenAniamtion instance;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }
    public  void Scale(GameObject target)
    {
        target.SetActive(true);
        target.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        target.transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutCubic);
        StartCoroutine(CloseTarget(target, 2f));
    }
    IEnumerator CloseTarget(GameObject target,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        target.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 1f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(0.9f);
        target.SetActive(false);
    }

}
