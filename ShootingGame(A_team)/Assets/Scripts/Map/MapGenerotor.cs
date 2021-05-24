using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class MapGenerotor : MonoBehaviour
{

    public MapFactory MapFactory;
    public Vector2 mapSize;
    public float outlinePercent;
    public GameObject[,] floorPosList;
    private Queue<GameObject> shuffleQueue;
    [Header("====Barrier Setting====")]
    public Color initialColor;
    public Color endColor;
    private List<GameObject> BarrierList = new List<GameObject>();

    public GameObject BarrierParent;
    public GameObject currentBarriers;

    Spwaner spawner;
    private void Awake()
    {
        floorPosList = new GameObject[(int)mapSize.y, (int)mapSize.x];
        spawner = GameObject.Find("Spawner").GetComponent<Spwaner>();
        spawner.OnNextWave.AddListener(newBarrier);
    }

    // Start is called before the first frame update
    void Start()
    {
        UPdateMap();
        UpdateBarrier();
    }

    void Update()
    {
        
    }
    public void UPdateMap()
    {
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                Vector3 newPos = new Vector3(-mapSize.x / 2 + 0.5f + i, 0, -mapSize.y/2+0.5f+j);
                GameObject newFloor = MapFactory.Create();
                newFloor.transform.SetParent(this.transform.GetChild(0).transform);
                newFloor.transform.localPosition = newPos;
                newFloor.transform.localScale = new Vector3(1 - outlinePercent, 1 - outlinePercent, 1 - outlinePercent);
                newFloor.GetComponent<Floor>().FloorPos = newPos;
                floorPosList[(int)Mathf.Abs(newPos.z + 3), (int)newPos.x + 6] =newFloor;
            }
        }
    }
    #region newBarrier
    public void UpdateBarrier()
    {
        RandomMaze randomMaze = new RandomMaze((int)mapSize.y, (int)mapSize.x);
        randomMaze.BarrierMap(0, (int)mapSize.y , (int)mapSize.x , 0);
        randomMaze.Maze[(int)(mapSize.y / 2), (int)(mapSize.x / 2 )] = true;

        GameObject barrierPosition =  Instantiate(BarrierParent, this.transform);
        barrierPosition.transform.localPosition -= new Vector3(0, 1.5f, 0);
        currentBarriers = barrierPosition;
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                if(randomMaze.Maze[i,j]==false)
                {
                    GameObject newBarrier = MapFactory.CreateBarrier();
                    newBarrier.transform.SetParent(barrierPosition.transform);
                    float randomHeight = (float)UnityEngine.Random.Range(0, 5) / 10;
                    Vector3 NewPos = floorPosList[i, j].GetComponent<Floor>().FloorPos;
                    newBarrier.transform.localPosition = NewPos;
                    newBarrier.transform.localScale = new Vector3(1 - outlinePercent, 1, 1 - outlinePercent);
                    newBarrier.GetComponent<Barrier>().SetBarrier(randomHeight);
                    #region colorSet
                    float colorPercentage;
                    colorPercentage =j/mapSize.x;
                    MeshRenderer meshRender = newBarrier.GetComponent<MeshRenderer>();
                    meshRender.material.color = Color.Lerp(initialColor, endColor, colorPercentage);
                    #endregion
                }
            }
        }
       barrierPosition.transform.DOMoveY(0.5f, 3f).SetEase(Ease.OutCubic);
    }
    public void newBarrier()
    {
        StartCoroutine("setNewBarrier");
    }
    IEnumerator setNewBarrier()
    {
        currentBarriers.transform.DOMoveY(-1, 2f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(2f);
        Destroy(currentBarriers.gameObject);
        UpdateBarrier();
    }
    #endregion

}


