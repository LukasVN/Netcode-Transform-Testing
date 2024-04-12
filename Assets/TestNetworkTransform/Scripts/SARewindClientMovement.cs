using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SARewindClientMovement : NetworkBehaviour
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
                SubmitJumpingRequestServerRpc();
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


    [Rpc(SendTo.Server)]
    void  SubmitPositionRequestServerRpc(Vector3 incrementPosition){
        Vector3 newPosition = transform.position + (incrementPosition * moveSpeed * Time.fixedDeltaTime);
        if(newPosition.x > 5f || newPosition.x < -5f || newPosition.z > 5f || newPosition.z < -5f){
            transform.position -= incrementPosition * moveSpeed * Time.fixedDeltaTime;
        }else{
            transform.position += incrementPosition * moveSpeed * Time.fixedDeltaTime;
        }
    }

    [Rpc(SendTo.Server)]
    void SubmitJumpingRequestServerRpc(){
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Move(){
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            
            Vector3 incrementPosition = new Vector3(moveX,0,moveZ);

            if(!IsHost){
                transform.position += incrementPosition * moveSpeed * Time.fixedDeltaTime;
            }

            SubmitPositionRequestServerRpc(incrementPosition);
            
    }
}
