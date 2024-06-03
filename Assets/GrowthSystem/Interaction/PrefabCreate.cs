using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Votanic.vXR.vGear;

public class PrefabCreate : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject currentInstance;

    private void Start()
    {
    }

    void Update()
    {

    }

    public void SpawnObject(int prefabIndex)
    {
        if (currentInstance == null)
        {
            currentInstance = Instantiate(prefabs[prefabIndex]);
            var ballComponent = currentInstance.AddComponent<Ball>();
            ballComponent.PrefabIndex = prefabIndex;                 
        }
        else
        {
            Destroy(currentInstance);
            currentInstance = Instantiate(prefabs[prefabIndex]);
            var ballComponent = currentInstance.AddComponent<Ball>();
            ballComponent.PrefabIndex = prefabIndex;
        }


        if (prefabIndex < 0 || prefabIndex >= prefabs.Length)
        {
            Debug.LogError("Prefab Index Out of Range");
            return;
        }
    }    


}
