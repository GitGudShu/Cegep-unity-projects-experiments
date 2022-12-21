using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartAfterWin : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }


    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("SampleScene");
    }
}
