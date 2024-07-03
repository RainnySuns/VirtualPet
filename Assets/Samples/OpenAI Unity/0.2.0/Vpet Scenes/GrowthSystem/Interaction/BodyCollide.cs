using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BodyCollide : MonoBehaviour
{
    private Animator pet;
    private PetController petController;
    private Dialogue dialogue;
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

    public bool isbody { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        isbody = false;
        pet = GameObject.FindGameObjectWithTag("Pet").GetComponent<Animator>();
        dialogue = GameObject.FindGameObjectWithTag("Pet").GetComponentInChildren<Dialogue>();
        petController = GameObject.FindGameObjectWithTag("Pet").GetComponent<PetController>();
    }

    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trigger")
        {
            isbody = true;
            petController.Pat();
            pet.SetBool("ishead", false);
            dialogue.ChangeText("Wohooooo!");
        }
        if (other.tag == "otherTrigger")
        {
            dialogue.speak(happy);
            SceneManager.LoadScene("Spring");
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.tag == "Trigger")
        {
            isbody = false;
            petController.Patleave();
            dialogue.ChangeText(string.Empty);
        }
    }
}
