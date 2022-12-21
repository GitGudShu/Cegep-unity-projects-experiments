using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject Prefab_Spawn;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        Instantiate(Prefab_Spawn, transform.position, Quaternion.identity);
    }
}
