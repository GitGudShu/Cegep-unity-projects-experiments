using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mouvementjoueur : MonoBehaviour
{
    //vitesse de déplacement
    public float walkSpeed;
    public float turnSpeed;

    //Imputs
    public string imputFront = "up";
    public string imputBack = "down";
    public string imputLeft = "left";
    public string imputRight = "right";
    public string imputRestart = "r";

    void Update()
    {
        bool haveExplode = GetComponent<CollisionJoueurAsteroid>().haveExplode;
        //si on avance
        if (Input.GetKey(imputFront) & !haveExplode)
        {
            transform.Translate(walkSpeed * Time.deltaTime, 0, 0);
        }

        //si on recule
        if (Input.GetKey(imputBack) & !haveExplode)
        {
            transform.Translate(-(walkSpeed / 2) * Time.deltaTime, 0, 0);
        }

        //roatation à gauche
        if (Input.GetKey(imputLeft) & !haveExplode)
        {
            transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }

        //rotaion à droite
        if (Input.GetKey(imputRight) & !haveExplode)
        {
            transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
        }

        if (Input.GetKey(imputRestart))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
