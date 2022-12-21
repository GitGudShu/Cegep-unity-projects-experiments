using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    public float Speed = 3f;
    public GameObject Explosion;

    private float m_LastHit;
    private float m_ImmunityDelay = 0.5f;

    void Update()
    {
        gameObject.transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime
                                                    , transform.position.y
                                                    , transform.position.z);
    
    }

    private void OnTriggerEnter2D(Collider2D collision) 
        {
            RobotController t_Robot = collision.gameObject.GetComponent<RobotController>();

            if(t_Robot&& m_LastHit + m_ImmunityDelay <= Time.time) // si t_Robot est null
            {
                Debug.Log("Robot hit!");
                t_Robot.TakeDamage(1);
                m_LastHit = Time.time;

                GameObject objet = Instantiate(Explosion, transform.position, Quaternion.identity);
                Destroy(objet, 0.5f);
                Destroy(gameObject);

            }
        }
}
