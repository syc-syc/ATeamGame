using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Barrier : MonoBehaviour
{
    float thisHeight;
    void Start()
    {
      
    }

    void Update()
    {
        
    }
    
    public void SetBarrier(float height)
    {
        thisHeight =height;
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, -height, this.transform.localPosition.z);
    }
    public void Quit()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        Invoke("DestotyThis", 2.5f);
    }
    public void DestotyThis()
    {
        Destroy(this.gameObject);
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
public void PlaySound(AudioClip clip,Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos, sfxVoluePercent * masterVolumePercent);
    }
    public void Shoot(Vector3 pos)
    {
        PlaySound(ShootClip, pos);
    }
    public void Relod(Vector3 pos)
    {
        PlaySound(RelodClip, pos);
    }
    public void EnemyKill(Vector3 pos)
    {
        PlaySound(EnemykillClip, pos);
    }
