using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Votanic.vXR.vGear;
public class StoryBeginWinter : MonoBehaviour
{
    public float textspeed = 2f;
    public string[] introduction;
    public string[] request;
    public string[] gameend;
    public string[] finish;

    public string[] changescene;
    public GameObject frame;
    public GameObject disk;

    private GameObject locuses;
    private Transform reference;
    private PetController pet;
    private Vector3 pointed;
    private Dialogue dialogue;
    private int n;
    private bool trig = true;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GameObject.FindGameObjectWithTag("Pet").GetComponentInChildren<Dialogue>();
        pet= GameObject.FindGameObjectWithTag("Pet").GetComponentInChildren<PetController>();
        start();
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
            pointed = (locus.points[0] + locus.points[Mathf.RoundToInt(i / 2)]) / 2;
            locus.tag = "Locus";
        }
        if (n==4)
        {
            changetheScene();
        }
    }

    IEnumerator changesentence(string[] lines)
    {
        foreach (var line in lines)
        {
            dialogue.speakline(line);
            yield return new WaitForSeconds(textspeed);
        }
        dialogue.textComponent.text = string.Empty;
    }

    public void start()
    {
        StartCoroutine(changesentence(introduction));
    }

    public void Instance()
    {
        Instantiate(disk, pointed, Quaternion.Euler(0, 0, 0));
    }

    public void gamefinish()
    {
        if (n == 3 & trig == false)
        {
            StartCoroutine(changesentence(finish));
            disk.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Instance();
            foreach (Transform child in locuses.transform)
            {
                Destroy(child.gameObject);
            }
            //vGear.controller.SetTool("Wand");
            trig = true;
            n += 1;
            Debug.Log("Finish2" + n);
        }
        else if (n == 1 && trig == false)
        {
            StartCoroutine(changesentence(gameend));
            Instance();
            foreach (Transform child in locuses.transform)
            {
                Destroy(child.gameObject);
            }
            //vGear.controller.SetTool("Wand");
            n += 1;
            trig = true;
            Debug.Log("Finish1" + n);
        }
    }


    public void changetheScene()
    {
        frame.SetActive(true);
        StartCoroutine(changesentence(changescene));
        pet.MoveToDestination(frame.transform, 10f);
    }

    public void requesttt()
    {
        if (trig == true)
        {
            StartCoroutine(changesentence(request));
            //vGear.controller.SetTool("Brush");
            trig = false;
            n += 1;
            Debug.Log("request" + n);
        }

    }

}
