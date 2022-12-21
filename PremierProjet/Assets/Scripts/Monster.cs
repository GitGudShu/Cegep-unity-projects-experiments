using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float Speed = 3f; 

    private float m_LastHit;
    private float m_ImmunityDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime
                                                    , transform.position.y
                                                    , transform.position.z);
    
    }

    private void OnCollisionEnter2D(Collision2D collision) 
        {
            Player t_Player = collision.gameObject.GetComponent<Player>();

            if(t_Player&& m_LastHit + m_ImmunityDelay <= Time.time) // si t_Player est null
            {
                Debug.Log("Player hit!");
                t_Player.TakeDamage(1);
                m_LastHit = Time.time;
            }
        }
}
