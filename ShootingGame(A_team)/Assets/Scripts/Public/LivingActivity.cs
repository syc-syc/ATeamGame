using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingActivity : MonoBehaviour,IDamage
{
    public float maxHp;
    public float currentHp;
    protected bool isDead =false;
    private void Start()
    {
        currentHp = maxHp;
    }
    public void GetDamage(float damage,Vector3 hitpoint,Vector3 hitdirection)
    {
        currentHp = currentHp - damage > 0 ? currentHp-damage : 0;
        Die(hitpoint,hitdirection);
    }
    public virtual void Die(Vector3 hitpoint,Vector3 hitdirection)
    {
        
    }
}
