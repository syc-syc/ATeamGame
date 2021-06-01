using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (GunController))]
public class Player : LivingActivity
{
    GunController gunController;
    // Start is called before the first frame update
    public HPBar playerHP;
    private void Awake()
    {
        gunController = this.GetComponent<GunController>();
    }
    void Start()
    {
        playerHP.SetHpCanvas(maxHp);
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
        else
        {
            gunController.equippedGun.fireEffect.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gunController.Reload();
        }
    }
    //void Relod()
    //{
    //    if(Input.GetKeyDown(KeyCode.R))
    //    {
    //        gunController.Reload();
    //    }
    //}
    public void GetDamage(int damageValue)
    {
        playerHP.GetDamage(damageValue);
        currentHp = currentHp - damageValue > 0 ? currentHp - damageValue : 0;
    }
}
