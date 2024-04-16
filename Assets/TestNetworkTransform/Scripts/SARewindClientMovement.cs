using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class SARewindClientMovement : NetworkBehaviour
{
    private float moveSpeed = 5f;
    private float jumpForce = 5f;
    private Rigidbody rb;
    private Vector3 incrementPosition;
    private RewindNetworkTransform rewindNetworkTransform;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rewindNetworkTransform = GetComponent<RewindNetworkTransform>();
    } 

    private void Update() {
        if(IsOwner){
            Move();
            if(Input.GetKeyDown(KeyCode.Space)){
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        
    }

    [Rpc(SendTo.Server)]
    void  SubmitPositionRequestServerRpc(Vector3 incrementPosition){
        Invoke("InvokeTest",0.5f);
    }


    void Move(){
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            
            incrementPosition = new Vector3(moveX,0,moveZ);
            
            Vector3 newPosition = transform.position + (incrementPosition * moveSpeed * Time.fixedDeltaTime);

            transform.position += incrementPosition * moveSpeed * Time.fixedDeltaTime;

            if(newPosition.x > 5f || newPosition.x < -5f || newPosition.z > 5f || newPosition.z < -5f){
                rewindNetworkTransform.SetServerAuthoritative();
                SubmitPositionRequestServerRpc(incrementPosition);
            }
            
    }

    public void InvokeTest(){
        transform.position -= incrementPosition * moveSpeed * Time.fixedDeltaTime;
        rewindNetworkTransform.SetClientAuthoritative();
    }


}
