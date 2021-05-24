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
