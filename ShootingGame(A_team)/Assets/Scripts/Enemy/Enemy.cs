using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyDieEvent : UnityEvent<Enemy> { }
public class Enemy : LivingActivity
{
    Transform player;
    NavMeshAgent navMenshAgent;
    Animator thisAn;
    MeshRenderer meshRender;
    public float updateRate;

    public GameObject deathEffect; 

    public UnityEvent OnNextWave = new UnityEvent();
    public EnemyDieEvent onEnemyDie = new EnemyDieEvent();
    private Player pl;
    //socrekepper
    private ScoreKeeper socrekeeper;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player")!=null)
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pl = GameObject.Find("Player").GetComponent<Player>();
        meshRender = GetComponentInChildren<MeshRenderer>();
        navMenshAgent = this.GetComponent<NavMeshAgent>();
        thisAn = GetComponentInChildren<Animator>();
        StartCoroutine("updatePath");
        //socreKepper
        if (GameObject.Find("Score") != null)
        {
            socrekeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator updatePath()
    {
        while (player != null&&!isDead)
        {
            Vector3 tartgetPos = new Vector3(player.position.x, 0, player.position.z);
            navMenshAgent.SetDestination(tartgetPos);
            yield return new WaitForSeconds(updateRate);
        }
    }
    public override void Die(Vector3 hitpoint, Vector3 hitdirection)
    {
        if(currentHp<=0&&!isDead)
        {
            navMenshAgent.enabled = false;
            this.GetComponent<CapsuleCollider>().enabled = false;
            isDead = true;
            StartCoroutine("modelFadeOut");
            thisAn.SetTrigger("Die");
            onEnemyDie.Invoke(this);
            //score killEnemy
            socrekeeper.KillEnemy();
        }
    }
    IEnumerator modelFadeOut()
    {
        while(meshRender.material.color.a>0)
        {
            Color modelColor = meshRender.material.color;
            modelColor = new Color(modelColor.r, modelColor.g, modelColor.r, modelColor.a - 0.002f);
            meshRender.material.color = modelColor;
            yield return null;
        }
        AudioManager.instance.EnemyKill(this.transform.position);
        Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity) as GameObject, 0.5f);
        Destroy(this.gameObject);  
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pl.GetDamage(10);
        }
    }
}
