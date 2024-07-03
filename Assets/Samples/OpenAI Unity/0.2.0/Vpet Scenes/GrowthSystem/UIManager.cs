using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PrefabCreate prefabCreate;
    public GameObject[] UIs;

    void Start()
    {
        prefabCreate = transform.GetComponent<PrefabCreate>();
        UIs = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            UIs[i] = transform.GetChild(i).gameObject;            
        }
    }

    // Update is called once per frame
    public void OnUIObjectClicked(int index)
    {
        prefabCreate.SpawnObject(index);
    }
}
