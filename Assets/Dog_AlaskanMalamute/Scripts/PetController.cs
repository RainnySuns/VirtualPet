using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private int IdleLayerIndex;
    private int patLayerIndex;
    private Vector3 initialScale = new Vector3 (1f, 1f, 1f);
    public Pet pet;
    private float moodDecayRate = 1f;
    private AudioManager audioManager;


    void Start()
    {
        transform.localScale = initialScale;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        IdleLayerIndex = animator.GetLayerIndex("Idle");
        patLayerIndex = animator.GetLayerIndex("Pat");

        pet = new Pet();
        pet.mood = pet.maxAttribute;
        audioManager = transform.GetComponent<AudioManager>();
    }

    void Update()
    {
        transform.localScale = initialScale;
        Vector3 pos = new Vector3(transform.position.x - 1f, 0, transform.position.z - 0.3f);
        Shader.SetGlobalVector("_PositionMoving", pos);
        ReduceMoodOverTime();
    }

    public void Pat()
    {        
        float currentWeight = animator.GetLayerWeight(patLayerIndex);
        animator.SetLayerWeight(patLayerIndex, Mathf.Lerp(currentWeight, 1, Time.deltaTime));
        animator.SetBool("ifpat", true);
        pet.Play();
        audioManager.ChangeAudio(1);
    }

    public void Patleave()
    {
        float currentWeight = animator.GetLayerWeight(IdleLayerIndex);
        animator.SetLayerWeight(IdleLayerIndex, Mathf.Lerp(currentWeight, 0, Time.deltaTime * 3));
        animator.SetBool("ifpat", false);
    }

    public void Eat(Transform destinationPoint, float distance)
    {        
        MoveToDestination(destinationPoint, distance);        
    }

    public void Drink(Transform destinationPoint, float distance)
    {
        MoveToDestination(destinationPoint, distance);                
    }

    public void Play(Transform destinationPoint, float speed, float distance)
    {
        if(speed >= 1f)
        {
            MoveToDestination(destinationPoint, distance);
            WaitForPlayCondition();
        }        
    }


    public void MoveToDestination(Transform destinationPoint, float distance)
    {
        audioManager.ChangeAudio(1);
        agent.SetDestination(destinationPoint.position);

        if (distance >= 0.2f)
        {
            animator.SetBool("ifrun", true);
        }
        else
        {
            animator.SetBool("ifrun", false);
        }
    }

    public void Jump()
    {
        animator.SetBool("ifjump", true);
    }

    public void FinishJump()
    {
        animator.SetBool("ifjump", false);
    }
    
    public void WaitForEatCondition()
    {
        animator.SetBool("ifrun", false);
        animator.SetBool("IsEat", true);
            audioManager.ChangeAudio(4);
            pet.Feed();
            StartCoroutine(SetEatFalseAfterDelay(3.0f));
            Debug.Log("eat");
    }

    public void WaitForDrinkCondition()
    {
        animator.SetBool("ifrun", false);
        animator.SetBool("ifdrink", true);
            audioManager.ChangeAudio(3);
            pet.Drink();
            StartCoroutine(SetDrinkFalseAfterDelay(3.0f));
            Debug.Log("drink");
    }

    public void WaitForPlayCondition()
    {
        animator.SetBool("ifrun", false);
        animator.SetBool("ifplay", true);
        audioManager.ChangeAudio(0);
        pet.Play();
        StartCoroutine(SetPlayFalseAfterDelay(3.0f));
        Debug.Log("play");
    }

    IEnumerator SetEatFalseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("IsEat", false);       
    }

    IEnumerator SetDrinkFalseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("ifdrink", false);        
    }

    IEnumerator SetPlayFalseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("ifplay", false);        
    }

    private void ReduceMoodOverTime()
    {
        if (pet != null)
        {
            pet.mood -= moodDecayRate * Time.deltaTime;
        }

        if(pet.mood <= 2)
        {
            audioManager.ChangeAudio(2);
        }
    }

}
