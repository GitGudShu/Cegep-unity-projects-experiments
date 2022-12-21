using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnDelay = 5f;
    public float MonsterSpeed = 3f;
    public int capacity = 5;

    private float m_LastSpawn;

    // Update is called once per frame
    void Update()
    {
        if(m_LastSpawn + SpawnDelay	<= Time.time)
        {
            GameObject t_Instance = Instantiate(Prefab, transform.position, Quaternion.identity);
            t_Instance.GetComponent<Monster>().Speed = MonsterSpeed;
            m_LastSpawn = Time.time;
            capacity --;
        }
        if(capacity < 1)
            Destroy(gameObject);
    }
}