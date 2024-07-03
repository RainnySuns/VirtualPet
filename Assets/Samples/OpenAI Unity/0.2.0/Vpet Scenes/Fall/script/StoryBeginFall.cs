using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Votanic.vXR.vGear;
public class StoryBeginFall : MonoBehaviour
{
    public float textspeed = 2f;
    public string[] introduction;
    public string[] request;
    public string[] finish;
    public string[] gameend;
    public string[] changescene;
    public GameObject frame;
    public GameObject disk;

    public GameObject locuses;
    private Transform reference;
    private Vector3 pointed;
    private Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GameObject.FindGameObjectWithTag("Pet").GetComponentInChildren<Dialogue>();
        //start();

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

    IEnumerator start(string[] lines)
    {
        yield return new WaitForSeconds(5);
        foreach (var line in lines)
        {
            dialogue.speakline(line);
            yield return new WaitForSeconds(textspeed);
        }
        dialogue.textComponent.text = string.Empty;
    }

    public void start()
    {
        StartCoroutine(start(introduction));
        vGear.controller.SetTool("Brush");
        requesttt();
    }

    public void Instance()
    {
        //Instantiate(disk, pointed, Quaternion.Euler(0, 0, 0));
        Instantiate(disk, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
    }

    public void gamefinish()
    {
        StartCoroutine(changesentence(finish));
        vGear.controller.SetTool("Wand");
    }

    public void restart()
    {

    }

    public void gamend()
    {
        StartCoroutine(changesentence(gameend));
    }

    public void changetheScene()
    {
        frame.SetActive(true);
        StartCoroutine(changesentence(changescene));
    }

    public void requesttt()
    {
        StartCoroutine(changesentence(request));
    }

}
