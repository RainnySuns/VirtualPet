using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerElement : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 newPosition;
    public float speedTrigger { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        speedTrigger = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePositions();
        speedTrigger = Vector3.Distance(newPosition, initialPosition) / Time.deltaTime;
    }    

    private void UpdatePositions()
    {
        initialPosition = newPosition;
        newPosition = transform.position;
    }
}
