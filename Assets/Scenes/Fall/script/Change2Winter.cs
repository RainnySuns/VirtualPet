using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change2Winter : MonoBehaviour
{
    private string targetTag = "Trigger";
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
        if (other.tag == targetTag)
        {
            SceneManager.LoadScene("Winter");
        }
    }
}
