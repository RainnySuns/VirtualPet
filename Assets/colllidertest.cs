using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colllidertest : MonoBehaviour
{
    public TextMesh text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        string ab = other.gameObject.name;
        text.text = ab;
    }
}
