using QlmLicenseLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Votanic.vXR.vGear;
using static UnityEngine.GraphicsBuffer;

public class Instantiateumbrella : MonoBehaviour
{
    private Vector3 pointend;
    public GameObject sphere;
    public GameObject cat;
    public float speed = .5f;
    private Animator animator;
    private bool isOK = false;
    private GameObject ob;
    private GameObject locuses;
    private Transform reference;

    // Start is called before the first frame update
    void Start()
    {
        cat = GameObject.FindWithTag("Pet");
        animator = cat.GetComponent<Animator>();
        pointend = new Vector3(0f, 0f, 0f);
        locuses = GameObject.Find("vGear/Tmp/Locuses");
    }

    // Update is called once per frame
    void Update()
    {

        if (locuses.GetComponentInChildren<vGear_Locus>() != null)
        {
            vGear_Locus locus = locuses.GetComponentInChildren<vGear_Locus>();
            int i = locuses.GetComponentInChildren<vGear_Locus>().points.Length - 1;

            reference = locus.transform;
            pointend = (locus.points[0] + locus.points[Mathf.RoundToInt(i)]) / 2;
            locus.tag = "Locus";
            //locus.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/ColorbyDistance");
        }

        if (vGear.Cmd.Received("OK"))
        {
            isOK = true;
        }

        if (isOK == true)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Locus");
            if (obj != null)
            {
                obj.SetActive(false);
            }
            ob = Instantiate(sphere, pointend, Quaternion.Euler(0, 0, 0));
            ob.tag = "otherTrigger";
            isOK = false;
        }


    }



}
