using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BulletDestoryEvent : UnityEvent<Projectile> { }
public class Projectile : MonoBehaviour {

	public LayerMask collisionMask;
	float speed = 10;
	public BulletDestoryEvent onBulletDestory = new BulletDestoryEvent();
	public float damage;


    public void SetSpeed(float newSpeed) {
		speed = newSpeed;
	}
	
	void Update () {
		float moveDistance = speed * Time.deltaTime;
		CheckCollision(moveDistance);
		transform.Translate (Vector3.forward *moveDistance);
	}


    private void CheckCollision(float moveDistance)
    {
		Ray ray = new Ray(transform.position, transform.forward);

		RaycastHit hit;

		if(Physics.Raycast(ray,out hit,moveDistance,collisionMask,QueryTriggerInteraction.Collide))
        {
			onHit(hit);
        }
    }

    private void onHit(RaycastHit target)
    {
		IDamage damageObject = target.collider.GetComponent<IDamage>();
		if(damageObject!=null)
        {
			damageObject.GetDamage(damage,target.point,transform.forward);
        }
		onBulletDestory.Invoke(this);                  //入池
    }

   
}
