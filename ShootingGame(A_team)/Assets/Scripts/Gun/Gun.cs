using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class Gun : MonoBehaviour
{
	public UnityEvent OnShoot;
	public Transform muzzle;
	[Header("Bullet Setting")]
	public Projectile projectile;
	public float msBetweenShots = 100;  //子弹发射间隔
	public float muzzleVelocity = 35;  //子弹速度
	public int bulletNumber;          //一个弹夹子弹数
	protected float nextShotTime;
	[Header("BulletPool")]
	public Transform poolPositon;
	public ObjectPool bulletPool;

	public GameObject fireEffect;
    private void Start()
    {
		poolPositon = GameObject.FindGameObjectWithTag("BulletPool").transform;
    }
    public virtual void GenerateBullet()
    {
		
	}
	public virtual void Shoot()
	{
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            OnShoot.Invoke();
            GenerateBullet();
        }
	}
	public void returnBullet(Projectile targetBullet)
    {
		StartCoroutine(returnBulletToPoolLater(targetBullet));
    }
	
	IEnumerator returnBulletToPoolLater(Projectile targetBullet)
    {
		yield return new WaitForSeconds(3f);
		if (targetBullet != null)
			bulletPool.ReturnInstance(targetBullet, poolPositon);
	}
	public void returnHitBulletToPool(Projectile targetBullet)
    {
		targetBullet.gameObject.SetActive(false);
		//if (targetBullet != null)
			//bulletPool.ReturnInstance(targetBullet, muzzle);
	}
}
