using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParameter : MonoBehaviour
{

    private Animator dog;
    private int[] i = {0, 1, 2, 3, 4, 5};
    private int previousIndex = -1;
    public Vector3 pos;
    public Material grass;

    // Start is called before the first frame update
    void Start()
    {
        dog = GetComponent<Animator>();
        StartCoroutine(ChangeAnimation());
        pos = new Vector3(transform.position.x, 0, transform.position.z);
        Shader.SetGlobalVector("_Position", pos);
    }

    // Update is called once per frame
    void Update()
    {
        pos = new Vector3 (transform.position.x, 0, transform.position.z);
        grass.SetVector("_Position", pos);
    }

    private IEnumerator ChangeAnimation()
    {
        while (true)
        {
            int index;
            do
            {
                index = Random.Range(0, i.Length);
            }
            while (index == previousIndex); 

            dog.SetInteger("change", i[index]);

            previousIndex = index;

            Debug.Log(i[index]);
            
            yield return new WaitForSeconds(5f);        
        }
    }
}
