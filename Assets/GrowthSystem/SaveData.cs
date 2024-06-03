using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public PetController controller;
    Pet myPet;
    public void SavePetData()
    {
        myPet = controller.pet;
        string petData = JsonUtility.ToJson(myPet);
        PlayerPrefs.SetString("PetData", petData);
    }

    public void LoadPetData()
    {
        if (PlayerPrefs.HasKey("PetData"))
        {
            string petData = PlayerPrefs.GetString("PetData");
            myPet = JsonUtility.FromJson<Pet>(petData);
        }
    }
}
