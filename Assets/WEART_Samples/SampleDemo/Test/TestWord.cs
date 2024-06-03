using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestWord : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        text.text = "User Test: " + collision.gameObject.name;
    }


}