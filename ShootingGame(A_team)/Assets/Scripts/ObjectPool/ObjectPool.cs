using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ObjectPool",menuName ="ObjectPool/ObjectPool")]
[System.Serializable]
public class ObjectPool : ScriptableObject
{
    public Projectile prefab;

 
    private Queue<Projectile> poolInstanceQueue = new Queue<Projectile>();
    public Projectile GetInstance()
    {
        if(poolInstanceQueue.Count>0)
        {
            Projectile instanceToUse = poolInstanceQueue.Dequeue();
            instanceToUse.gameObject.SetActive(true);
            return instanceToUse;
        }
        return Instantiate(prefab);
    }
    public void ReturnInstance(Projectile gamejectToPool,Transform poolTransform)
    {
        poolInstanceQueue.Enqueue(gamejectToPool);
        gamejectToPool.gameObject.SetActive(false);
        gamejectToPool.transform.SetParent(poolTransform);
    }
}
