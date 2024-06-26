using System;
using Unity.Netcode;
using UnityEngine;

public class CAClientMovement : NetworkBehaviour
{
    private float moveSpeed = 5f;
    private float jumpForce = 5f;
    private Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    } 
    private void Update() {
        if(IsOwner){
            if(Input.GetKeyDown(KeyCode.Space)){
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        
    }
    void FixedUpdate()
    {
        if (IsOwner)
        {
            Move();
        }

    }

    void Move(){
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            
            Vector3 incrementPosition = new Vector3(moveX,0,moveZ);
            
            Vector3 newPosition = transform.position + (incrementPosition * moveSpeed * Time.fixedDeltaTime);
            if(newPosition.x > 5f || newPosition.x < -5f || newPosition.z > 5f || newPosition.z < -5f){
                return;
            }else{
                transform.position += incrementPosition * moveSpeed * Time.fixedDeltaTime;
            }

            
    }


}
