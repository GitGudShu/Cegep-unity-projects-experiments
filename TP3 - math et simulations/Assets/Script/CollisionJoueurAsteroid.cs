using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionJoueurAsteroid : MonoBehaviour
{

    public GameObject Explosion;
    public bool haveExplode = false;

    // Update is called once per frame
    void Update()
    {
        GameObject[] Asteroids;
        Asteroids = GameObject.FindGameObjectsWithTag("asteroid");

        if (Asteroids.Length == 0)
        {
            Debug.Log("No game objects are tagged with 'asteroid'");
        }

        PolygonCollider2D colider = GetComponent<PolygonCollider2D>();
        int number = colider.GetTotalPointCount();
        Vector2[] list_point = colider.points;

        float angle = transform.rotation.z;
        float S = Mathf.Sin(angle * Mathf.PI);
        float C = Mathf.Cos(angle * Mathf.PI);
        Vector2 p = new Vector2(transform.position.x, transform.position.y);
        for (int i = 0; i < number; i++)
        {
            //list_point[i] = list_point[i] + p;
            list_point[i] = new Vector2(C * list_point[i].x - S * list_point[i].y, S * list_point[i].x + C * list_point[i].y) + p;

        }
        foreach (GameObject Asteroid in Asteroids)
        {
            CircleCollider2D cercleColider = Asteroid.GetComponent<CircleCollider2D>();
            Vector3 cercle = new Vector3(cercleColider.transform.position.x, cercleColider.transform.position.y, cercleColider.radius);

            /*list_point = new Vector2[3];
            list_point[0] = new Vector2(1, 4);
            list_point[1] = new Vector2(3, 6);
            list_point[2] = new Vector2(5, 4);
            cercle = new Vector3(3, 2, 1.8f);
            */

            if (isCollied(cercle, list_point))
            {
                if (!haveExplode)
                {
                    // affiche l'exploosion
                    GameObject newObject = (GameObject)Instantiate(Explosion, transform.position, Quaternion.identity);
                    haveExplode = true;
                    StartCoroutine(WaitAndRestart());
                }
            }

        }
    }


    IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SampleScene");
    }


    private bool isCollied(Vector3 cercle, Vector2[] list_point)
    {
        // A compléter
        List<Vector2> shape1_normals = computeNormals(list_point);
        foreach(Vector2 axis in shape1_normals)
        {
            Vector2 projection1 = projectShape(list_point, axis);
            Vector2 projection2 = projectCircle(cercle, axis);
            if (!overlaps(projection1, projection2))
                return false;
        }
        return true;
    }

    // Ajouter les autres fonctions ici

    private bool overlaps(Vector2 projection1, Vector2 projection2)
    {
        if (projection1.y <= projection2.x || projection2.y <= projection1.x)
            return false;
        return true;
    }

    private Vector2 projectShape(Vector2[] shape_points, Vector2 axis)
    {
        float minimum = (axis.x * shape_points[0].x) + (axis.y * shape_points[0].y);
        float maximum = minimum;
        foreach(Vector2 point in shape_points)
        {
            float projection = (axis.x * point.x) + (axis.y * point.y);
            if(projection < minimum)
                minimum = projection;
            if(projection > maximum)
                maximum = projection;
        }
        return new Vector2(minimum, maximum);
    }

    private Vector2 projectCircle(Vector3 circle, Vector2 axis)
    {
        float projection = (axis.x * circle.x) + (axis.y * circle.y);
        return new Vector2(projection - circle.magnitude, projection + circle.magnitude);
    }

    private List<Vector2> computeNormals(Vector2[] shape_points)
    {
        List<Vector2> normals = new List<Vector2>();
        int current_point_index = 0;
        foreach(Vector2 point in shape_points)
        {
            int next_point_index = current_point_index + 1;
            if (next_point_index == shape_points.Length)
                next_point_index = 0;
            Vector2 next_point = shape_points[next_point_index];
            Vector2 edge_vector = next_point - point;
            Vector2 normal = new Vector2(edge_vector.y, -edge_vector.x);
            normal = normal.normalized;
            normals.Add(normal);
        }
        return normals;
    }



}
