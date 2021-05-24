using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage 
{
    public void GetDamage(float damage,Vector3 hitPoint,Vector3 hitdirection);
}
