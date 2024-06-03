using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Votanic.vXR.vGear;

public class UIController : MonoBehaviour
{
    public PetController controller;
    public Slider experienceSlider;
    public TextMeshProUGUI levelText;
    public Slider moodSlider;

    void Start()
    {
        UpdateExperienceUI();
        UpdateMoodUI();

        vGear.uiScreen.MountScreen(transform, true); 
    }

    void Update()
    {
        UpdateExperienceUI();
        UpdateMoodUI();
    }

    public void UpdateExperienceUI()
    {
        experienceSlider.maxValue = controller.pet.maxExperience; 
        experienceSlider.value = controller.pet.experience;

        levelText.text = "Level " + controller.pet.level.ToString();
    }

    public void UpdateMoodUI()
    {
        moodSlider.maxValue = controller.pet.maxAttribute;
        moodSlider.value = controller.pet.mood;
    }
}
