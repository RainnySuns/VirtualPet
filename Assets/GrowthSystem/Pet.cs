using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Pet
{
    public int level = 1;
    public float stamina;
    public int experience;
    public float fullness;
    private float _mood;
    public float thirst;

    public float mood
    {
        get => _mood;
        set => _mood = Mathf.Clamp(value, 0, maxAttribute);  
    }

    public int maxExperience => level * 10; 
    public float maxAttribute => 50 + (level - 1) * 5; 

    public void AddExperience(int amount)
    {
        experience += amount;
        if (experience >= maxExperience)
        {
            experience = 0;
            level = Mathf.Min(10, level + 1); 
        }
    }

    public void Feed()
    {
        stamina = Mathf.Min(stamina + 10, maxAttribute);
        AddExperience(5);
        fullness = Mathf.Min(fullness + 20, maxAttribute);
        mood += 3;
    }

    public void Play()
    {
        mood += 3;
        mood = Mathf.Min(mood + 10, maxAttribute);
        stamina = Mathf.Max(stamina - 5, 0);  
        fullness = Mathf.Max(fullness - 5, 0);
        thirst = Mathf.Min(thirst + 5, maxAttribute);
    }

    public void Drink()
    {
        thirst = Mathf.Max(thirst - 20, 0);
        stamina = Mathf.Min(stamina + 2, maxAttribute);
        AddExperience(1);
        mood += 1;
    }

    public void Sick() 
    {
        mood -= 10;
    }

}

