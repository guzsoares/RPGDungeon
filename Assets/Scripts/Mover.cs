using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;

    private Vector3 scale;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    protected virtual void UpdateMotor(Vector3 input, float ySpeed, float xSpeed){
        
        // Reset MoveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed,0);

        // Revert sprite
        if (moveDelta.x > 0){
            scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (moveDelta.x < 0){
            scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        //add push vector

        moveDelta += pushDirection;

        // Reduce push force, based off recovery
        pushDirection = Vector3.Lerp(pushDirection,Vector3.zero,pushRecoverySpeed);

        hit = Physics2D.BoxCast(transform.position,boxCollider.size, 0, new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Player","Blocking"));

        if (hit.collider == null){
            // Move mechanics
            transform.Translate(0, moveDelta.y* Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position,boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Player","Blocking"));
        
        if (hit.collider == null){
            // Move mechanics
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
