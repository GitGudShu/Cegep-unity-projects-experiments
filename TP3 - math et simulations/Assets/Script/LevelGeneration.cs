using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public int numberOfPoints;
    public GameObject[] objects;
    public float max_x = 8.0f;
    public float min_x = -8.0f;
    public float max_y = 3.0f;
    public float min_y = -3.0f;
    public LayerMask coverMask; // valeur 6

    void Start()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            int rand = Random.Range(0, objects.Length);
            TryToSpawn(objects[rand]);
        }
    }

    // Essais de placer l'objet "objecToSpawn" sans toucher au autres objet ayant le tag "coverMask"
    void TryToSpawn(GameObject objectToSpawn)
    {
        Vector2 positionHolder = Vector2.zero;
        new ContactFilter2D().SetLayerMask(coverMask);
        bool validPosition = false;
        int failureLimit = 10;
        int fails = 0;
        do
        {
            // new position
            positionHolder = new Vector2(Random.Range(min_x, max_x), Random.Range(min_y, max_y));

            // Intantiate une copie de lobjet à la position voulu (permet dactualiser la physique)
            GameObject newObject = (GameObject)Instantiate(objectToSpawn, new Vector3(positionHolder[0], positionHolder[1], 0.003f), Quaternion.identity);
            newObject.layer = 7;

            // prendre le collider de l'objet 
            Collider2D objectCollider = newObject.GetComponent<Collider2D>();
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(coverMask);

            // Vérifier les collision
            Collider2D[] collidersInsideOverlapBox = new Collider2D[1];
            int numberOfCollidersFound = Physics2D.OverlapCollider(objectCollider, contactFilter, collidersInsideOverlapBox);

            if (numberOfCollidersFound == 0)
            {
                validPosition = true;
                newObject.layer = 6;
            }
            else
            {
                fails++;
                Destroy(newObject);
            }
        }
        while (!validPosition && fails < failureLimit);

        if (!validPosition)
        {
            Debug.Log("No more room");
        }
    }

}
