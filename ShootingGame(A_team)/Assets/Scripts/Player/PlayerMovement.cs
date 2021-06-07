using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody thisRg;
    public float moveSpeed;
    private Animator an;

    public LayerMask floorMask;
    private void Awake()
    {
        thisRg = this.GetComponent<Rigidbody>();
        an = GameObject.Find("Mesh Object").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        Turning();
    }
    public void Move()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        thisRg.MovePosition(thisRg.position + moveVelocity * Time.fixedDeltaTime);
        if(moveInput != Vector3.zero)
        {
            an.SetBool("Walk", true);
        }
        else
        {
            an.SetBool("Walk", false);
        }
    }
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, 10f, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - this.transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);
            this.transform.DORotateQuaternion(newRotatation, 0.8f);
        }
    }
 
}
