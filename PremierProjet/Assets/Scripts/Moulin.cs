using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moulin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Moulin start!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision) 
    {
        Debug.Log("Moulin OnTriggerEnter2D - " + collision.gameObject.name);
    }
}
