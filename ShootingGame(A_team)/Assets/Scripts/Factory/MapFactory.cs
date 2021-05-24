using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapFactory", menuName = "Factory/MapFactory")]
public class MapFactory : FactorySO<GameObject>
{
    public GameObject floorPrefab;
    public GameObject BarrierPrefab;

    public override GameObject Create()
    {
        GameObject floorInstance = Instantiate(floorPrefab);
        return floorInstance;
    }
    public GameObject CreateBarrier()
    {
        GameObject BarrierInstance = Instantiate(BarrierPrefab);
        return BarrierInstance;
    }
}
