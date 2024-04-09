using System;
using Unity.Netcode;
using UnityEngine;

public class NetworkTransformTest : NetworkBehaviour
{

    private float moveSpeed = 5f;
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

    void Move(){
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 incrementPosition = new Vector3(moveX,0,moveZ);

            //transform.position += incrementPosition * moveSpeed * Time.fixedDeltaTime;

            SubmitPositionRequestServerRpc(incrementPosition);
            
    }


}
