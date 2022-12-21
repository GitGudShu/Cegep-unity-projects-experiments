using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    [SerializeField] private AudioClip[] RandomSpawnSounds;
    private AudioSource m_AS;

    private void Awake()
    {
        m_AS = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioClip t_Sound = RandomSpawnSounds[Random.Range(0, RandomSpawnSounds.Length)];
        m_AS.pitch = Random.Range(0.7f, 1.5f);
        m_AS.PlayOneShot(t_Sound);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * 1f;
    }
}
