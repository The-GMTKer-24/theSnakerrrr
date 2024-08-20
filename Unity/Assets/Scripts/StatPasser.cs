using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatPasser : MonoBehaviour
{
    public static StatPasser Instance;
    public int deaths;
    public float time;

    public void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }

    public void Nuke(float timeToWait, string sceneToLoad)
    {
        StartCoroutine(Boom(timeToWait, sceneToLoad));
    }

    private IEnumerator Boom(float timeToWait, string sceneToLoad)
    {
        foreach (var objec in GameObject.FindGameObjectsWithTag("Untagged"))
        {
            Destroy(objec);
        }
        foreach (var objec in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (objec == this.gameObject)
            {
                continue;
            }
            Destroy(objec);
        }
        yield return new WaitForSeconds(timeToWait);
        foreach (var objec in GameObject.FindGameObjectsWithTag("Untagged"))
        {
            Destroy(objec);
        }
        foreach (var objec in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (objec == this.gameObject)
            {
                continue;
            }
            Destroy(objec);
        }
        yield return new WaitForSeconds(timeToWait);
        foreach (var objec in GameObject.FindGameObjectsWithTag("Untagged"))
        {
            Destroy(objec);
        }
        foreach (var objec in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (objec == this.gameObject)
            {
                continue;
            }
            Destroy(objec);
        }
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(sceneToLoad);
    }
}