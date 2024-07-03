using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private float a; 
    public float moveSpeed = 2f; 
    private float originalY; 
    private bool movingUp = true; 
    private float moveAmount = 12f; 
    void Start()
    {
        a = Random.Range(0, 10);
        originalY = transform.position.y;
        Debug.Log(transform.name + a);
    }

    void Update()
    {
        if(a<3)
        {
            moveSpeed = 2f;
            Move();
        } 
        else if (3 <= a && a < 6)
        {
            moveSpeed = 3f;
            Move();
        }
        else if (a >= 6)
        {
            moveSpeed = 2.5f;
            MoveDown();
        }
    }

    private void Move()
    {
        if (movingUp)
        {
            if (transform.position.y < originalY + moveAmount)
            {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            }
            else
            {
                movingUp = false;
            }
        }
        else
        {
            if (transform.position.y > originalY)
            {
                transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
            }
            else
            {
                movingUp = true;
            }
        }
    }

    private void MoveDown()
    {
        if (movingUp)
        {
            if (transform.position.y > originalY - moveAmount)
            {
                transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
            }
            else
            {
                movingUp = false;
            }
        }
        else
        {
            if (transform.position.y < originalY)
            {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            }
            else
            {
                movingUp = true;
            }
        }
    }


}
