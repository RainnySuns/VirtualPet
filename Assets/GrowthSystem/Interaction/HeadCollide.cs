using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeadCollide : MonoBehaviour
{
    private Animator pet;
    private Dialogue dialogue;
    private PetController petController;    
    private BodyCollide body;
    private string[] happy = new string[] {
    "Oh oh!",
    "You're holding",
    "the umbrella for me!",
    "I feel so happy",
        "so warm!",
    "You are the best buddy!",
    "Now we can go on",
        "an adventure together!",
    "Maybe find something",
        "interesting",
    "or even better",
        "find some delicious food!",
    "Woof woof!" };

    void Start()
    {
        body = GameObject.FindGameObjectWithTag("Pet").GetComponentInChildren<BodyCollide>();
        pet = GameObject.FindGameObjectWithTag("Pet").GetComponent<Animator>();
        dialogue = GameObject.FindGameObjectWithTag("Pet").GetComponentInChildren<Dialogue>();
        petController = GameObject.FindGameObjectWithTag("Pet").GetComponent<PetController>();        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trigger" && !body.isbody)
        {            
            petController.Pat();
            pet.SetBool("ishead", true);
            dialogue.ChangeText("WoW!");
            dialogue.BounceEffect();
        }
        if (other.tag == "otherTrigger" && !body.isbody)
        {
            dialogue.speak(happy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Trigger")
        {
            petController.Patleave();
            pet.SetBool("ishead", false);
            dialogue.ChangeText(string.Empty); 
        }
    }
}
