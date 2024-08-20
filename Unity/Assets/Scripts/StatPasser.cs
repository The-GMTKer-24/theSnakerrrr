using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class StatPasser : MonoBehaviour
{
    public static StatPasser Instance;
    public int deaths;
    public int scales;
    public float time;

    public void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }

    public void Nuke(float timeToWait, string sceneToLoad)
    {
        Achievements.Evaluate(this);
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
        SceneManager.LoadScene(sceneToLoad);
    }
}