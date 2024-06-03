using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBegin : MonoBehaviour
{
    public float textspeed;
    public string[] introduction;
    public string[] request;
    public string[] finish;
    public string[] changescene;
    public ColliderTest picker;
    public bool star = false;


    private Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {

        dialogue = GameObject.FindGameObjectWithTag("Pet").GetComponentInChildren<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    IEnumerator ssse(string[] lines) 
    {
        foreach (var line in lines)
        {
            //dialogue.ChangeText(line);
            dialogue.speakline(line);
            yield return new WaitForSeconds(textspeed);            
        }
        picker.StarPick();
        star = true;
        dialogue.textComponent.text = string.Empty;
    }

        IEnumerator ffff(string[] lines)
    {
        picker.Stop();
        foreach (var line in lines)
        {
            dialogue.speakline(line);
            yield return new WaitForSeconds(textspeed);
        }
        picker.Finish();

        dialogue.textComponent.text = string.Empty;
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
        StartCoroutine(ssse(introduction));
    }

    public void gamefinish()
    {
        StartCoroutine(ffff(finish));
    }

    public void changetheScene()
    {
        StartCoroutine(changesentence(changescene));
    }

}
