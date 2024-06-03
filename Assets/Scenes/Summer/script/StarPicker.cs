using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarPicker : MonoBehaviour
{
    public int starCount=0;
    public PetController pet;
    public TextMeshProUGUI UI;
    public ParticleSystem partSystem;
    public StoryBegin begin;

    private Transform startpoint;
    private Transform destinate;
    private float dis;
    private float ddd;
    private float finald;
    private ParticleSystem.Particle[] particles;


    public GameObject frame;

    void Start()
    {
        particles = new ParticleSystem.Particle[partSystem.particleCount];
        int numParticlesAlive = partSystem.GetParticles(particles);
        startpoint = pet.transform;
    }

    private void Update()
    {
        UI.text = starCount.ToString();
        if(starCount >= 3)
        {
            begin.gamefinish();
        }

        dis = Distancecalculator(frame.transform, pet.transform);

        StartCoroutine(randomGenerator());
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
        pet.MoveToDestination(destinate, ddd);
        starCount += 1;

    }

    private float Distancecalculator(Transform aa, Transform bb)
    {
        return Vector2.Distance(new Vector2(aa.position.x, aa.position.z),
                               new Vector2(bb.position.x, bb.position.z));
    }

    IEnumerator randomGenerator( )
    {
        destinate.position = new Vector3 (Random.Range(-8, 8), 0, Random.Range(-8, 8));

        yield return new WaitForSeconds(2f);
    }

    public void Finish()
    {
        begin.changetheScene();
        frame.SetActive(true);
        pet.MoveToDestination(frame.transform, dis);
    }
}
