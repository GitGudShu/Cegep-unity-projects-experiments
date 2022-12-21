using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSnowmanController : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnDelay = 5f;
    public float SnowBallSpeed = -3f;
    public int capacity = 10;

    private float m_LastSpawn;
    private float m_LastHit;
    private float m_ImmunityDelay = 0.5f;
    
    // Update is called once per frame
    void Update()
    {
        if(m_LastSpawn + SpawnDelay	<= Time.time)
        {
            GameObject t_Instance = Instantiate(Prefab, transform.position, Quaternion.identity);
            t_Instance.GetComponent<SnowBall>().Speed = SnowBallSpeed;
            m_LastSpawn = Time.time;
            capacity--;
        }
        if(capacity <= 0)
        {
            Debug.Log("no more snowballs");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
        {
            RobotController t_Robot = collision.gameObject.GetComponent<RobotController>();

            if(t_Robot&& m_LastHit + m_ImmunityDelay <= Time.time) // si t_Robot est null
            {
                Debug.Log("Robot hit!");
                t_Robot.TakeDamage(1);
                m_LastHit = Time.time;
            }
        }
}
