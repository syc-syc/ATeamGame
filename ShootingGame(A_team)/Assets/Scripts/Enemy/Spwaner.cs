using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class Spwaner : MonoBehaviour
{
    public GameObject mentionText;
    public List<Wave> waves = new List<Wave>();
    Wave currentWave;
    int currentWaveNumber =0;
    float nextSpawnTime;
    private  List<Enemy> currentEnemys = new List<Enemy>();

    public UnityEvent OnNextWave = new UnityEvent();
    public UnityAction NextWaveDelay;

    bool can_spawn =false;
    void Start()
    {
        StartCoroutine(DelayNextWave(FirstWave));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWave != null)
        {
            if (currentWave.allEnemys.Count > 0 && Time.time > nextSpawnTime&&can_spawn)
            {
                SpawnOneEnemy();
            }

            if (currentEnemys.Count <= 0 && currentWave.allEnemys.Count <= 0 && currentWaveNumber + 1 < waves.Count)    //改一下延时方式
            {
                can_spawn = false;
                DoTweenAniamtion.instance.Scale(mentionText);
                OnNextWave.Invoke();
                currentWave = waves[++currentWaveNumber];
                currentWave.SetEnemy();
                StartCoroutine(DelayNextWave(NextWave));
                //下一波事件
            }
        }
        
    }

    private void NextWave()
    {     
         can_spawn = true;
    }
    public void RemoveDiedEnemy(Enemy enemy)
    {
        if(currentEnemys.Contains(enemy))
        currentEnemys.Remove(enemy);
    }
    public Vector3 GetRandomPosition()
    {
        int posX = UnityEngine.Random.Range(-60, 61);
        int posZ = UnityEngine.Random.Range(-60, 61);

        Vector3 newPos = new Vector3(posX, 0, posZ);
        NavMeshHit hit;
       if(!NavMesh.SamplePosition(newPos, out hit, 6, 1))
        {
            GetRandomPosition();
        }
        return newPos;
    }
    public void SpawnOneEnemy()
    {
        Enemy newEenemy = Instantiate(currentWave.allEnemys.Dequeue());
        currentEnemys.Add(newEenemy);
        newEenemy.onEnemyDie.AddListener(RemoveDiedEnemy);
        newEenemy.transform.position = GetRandomPosition();
        nextSpawnTime = currentWave.timeBetweenSpawn+Time.time;
    }
    IEnumerator DelayNextWave(UnityAction todoAction)
    {
        yield return new WaitForSeconds(3f);
        todoAction.Invoke();
    }
    public void FirstWave()
    {
        currentWave = waves[currentWaveNumber];
        currentWave.SetEnemy();
        SpawnOneEnemy();
        can_spawn = true;
    }

}
[System.Serializable]
public class Wave
{
    public Queue<Enemy> allEnemys = new Queue<Enemy>();
    public List<enemyContent> enemys = new List<enemyContent>();
    public float timeBetweenSpawn;

    public void SetEnemy()
    {
        foreach(enemyContent enemycontent in enemys)
        {
            Debug.Log("这一波的怪物内容有" + enemycontent.enemyPrefab.name);
            Debug.Log("这一波怪物一共有" + enemycontent.cout);
            for (int i = 0; i < enemycontent.cout; i++)
            {
                allEnemys.Enqueue(enemycontent.enemyPrefab);
            }
        }
        allEnemys = new Queue<Enemy>(Shuffle<Enemy>.ShuffleTheList(allEnemys.ToArray()));
    }
    //波数的控制 一开始生成一大部分 接下来持续生成敌人
}
[System.Serializable]
public struct enemyContent
{
    public Enemy enemyPrefab;
    public int cout;
}