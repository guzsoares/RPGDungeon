using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Transform lookAt;
    public float boundX = 0.40f;
    public float boundY = 0.05f;

    private void Start() {
        lookAt = GameObject.Find("Player").transform;
    }

    private void LateUpdate(){

        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        float deltaY = lookAt.position.y - transform.position.y;

        if (deltaX > boundX || deltaX < -boundX){
            if (transform.position.x < lookAt.position.x){
                delta.x = deltaX - boundX;
            }
            else{
                delta.x = deltaX +  boundX;
            }

        }

         if (deltaY > boundY || deltaY < -boundY){
            if (transform.position.y < lookAt.position.y){
                delta.y = deltaY - boundY;
            }
            else{
                delta.y = deltaY +  boundY;
            }

        }

        transform.position += new Vector3(delta.x,delta.y,0);


    }
}
