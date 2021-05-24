using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShellDestoryedEvent : UnityEvent<Shell> { }
public class Shell : MonoBehaviour
{
    Rigidbody thisRigidbody;
    MeshRenderer thisMeshRender;
    public float minForce;
    public float maxForce;

    public float lifeTime = 4;
    public float fadeTime = 2;
    public Material thisMaterial;

    public ShellDestoryedEvent OnShellDestory = new ShellDestoryedEvent();

    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisMeshRender = GetComponent<MeshRenderer>();
        float force = Random.Range(minForce, maxForce);
        thisRigidbody.AddForce(transform.right * force);
        thisRigidbody.AddTorque(Random.insideUnitSphere * force);
        StartCoroutine("FadeOut");
        
    }

    public void Reset()
    {
        StopCoroutine("FadeOut");
        thisMeshRender.material = thisMaterial;
        StartCoroutine("FadeOut");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(lifeTime);

        float percent = 0;
        float fadeSpeed = 1 / fadeTime;
        Material mat = GetComponent<Renderer>().material;
        Color intitialColor = mat.color;
        while (percent<1)
        {
            percent += Time.deltaTime * fadeSpeed;
            mat.color = Color.Lerp(intitialColor, new Color(0,0,0,0), percent);
            yield return null;
        }
        OnShellDestory.Invoke(this);
    }
}
