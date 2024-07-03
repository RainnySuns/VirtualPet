using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColliderTest : MonoBehaviour
{
    private int starCount = 0;
    public PetController pet;
    public TextMeshProUGUI UI;
    public StoryBegin begin;

    private Transform startpoint;
    public Transform destinate;
    private float dis=0f;
    private float ddd=0f;
    private float finald=0f;
    private bool finish = true;
    public GameObject frame;

    void Start()
    {
        startpoint = pet.transform;
    }

    private void Update()
    {
        UI.text = starCount.ToString();
        if (starCount >= 10 && finish)
        {
            begin.gamefinish();
            finish = false;
        }

        dis = Distancecalculator(frame.transform, pet.transform);

        //destinate.position = particles[number].position;
        ddd = Distancecalculator(destinate, pet.transform);


        finald = Distancecalculator(startpoint, pet.transform);
    }


    public void Stop()
    {
        pet.MoveToDestination(startpoint, finald);
    }

    public void StarPick()
    {
        StartCoroutine(randomGenerator());     
    }

    private float Distancecalculator(Transform aa, Transform bb)
    {
        return Vector2.Distance(new Vector2(aa.position.x, aa.position.z),
                               new Vector2(bb.position.x, bb.position.z));
    }

    IEnumerator randomGenerator()
    {   
        destinate.position = new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));
        pet.MoveToDestination(destinate, ddd);
        Debug.Log("rain3 " + ddd);
        yield return new WaitForSeconds(10f);
    }

    public void Finish()
    {
        frame.SetActive(true);
        pet.MoveToDestination(frame.transform, dis);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("rain1 " + other.name);
        if (begin.star)
        {
            starCount += 1;
        }
    }
}
