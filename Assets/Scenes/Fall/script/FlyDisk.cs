using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDisk : MonoBehaviour
{
    private PetController controller;
    private Vector2 modiPosition;
    private Vector3 initialPosition;
    private Vector3 newPosition;
    private Vector2 controPosition;
    private bool hasExecuted = true;
    private float dis;

    void Start()
    {
        initialPosition = transform.position;
        controller = GameObject.FindGameObjectWithTag("Pet").GetComponent<PetController>();
    }

    void Update()
    {
        UpdatePositions();
        modiPosition = new Vector2(transform.position.x, transform.position.z);
        controPosition = new Vector2(controller.transform.position.x, controller.transform.position.z);
        dis = Vector2.Distance(modiPosition, controPosition);
        float y = transform.position.y - controller.transform.position.y;

        if(speed() > 3f)
        {
            AddForce();
        }

        if (dis > 0.2f && speed() > 1f)
        {
            hasExecuted = false;
            controller.Play(transform, speed(), dis);
            DestroyAfterDelay(5f);
        }
        else if (dis <= 0.2f && !hasExecuted)
        {
            if (y >= 1f)
            {
                controller.Jump();
            }
            else
            {
                controller.FinishJump();
                controller.WaitForPlayCondition();
            }

            hasExecuted = true;
        }
    }

    public float speed()
    {
        return Vector3.Distance(newPosition, initialPosition) / Time.deltaTime;
    }

    private void UpdatePositions()
    {
        initialPosition = newPosition;
        newPosition = transform.position;
    }

    public void AddForce()
    {
        Vector3 direction = (newPosition - initialPosition).normalized;
        transform.GetComponent<Rigidbody>().AddForce(direction * speed(), ForceMode.Impulse);
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
