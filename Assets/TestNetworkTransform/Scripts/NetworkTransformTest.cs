using System;
using Unity.Netcode;
using UnityEngine;

public class NetworkTransformTest : NetworkBehaviour
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
        transform.position += incrementPosition * moveSpeed * Time.fixedDeltaTime;
    }

    [Rpc(SendTo.Server)]
    void SubmitJumpingRequestServerRpc(){
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Move(){
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            
            Vector3 incrementPosition = new Vector3(moveX,0,moveZ);
            
            SubmitPositionRequestServerRpc(incrementPosition);
            
    }


}
