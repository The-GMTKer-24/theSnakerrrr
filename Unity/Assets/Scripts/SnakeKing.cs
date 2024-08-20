using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SnakeKing : SmartShooter
{
    public static SnakeKing Instance;
    public int maxHealth;
    public int health;
    public Slider slider;
    public GameObject[] minionSpawnLocations;
    public float timeBetweenSpawns;
    public Transform[] bulletRefreshes;
    public GameObject refresh;
    public GameObject minion;
    public String sceneToLoad;
    public float timeToWait;
    private List<GameObject> snakes;
    private GameObject currentRefresh;
    private float timer;
    
    public void Awake()
    {
        snakes = new List<GameObject>();
        Instance = this;
        maxHealth *= 2;
        health = maxHealth;
    }

    public void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer > timeBetweenSpawns)
        {
            foreach (GameObject spot in minionSpawnLocations)
            {
                snakes.Add(Instantiate(minion, spot.transform.position, quaternion.identity));
            }
            timer = 0;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public override void Die()
    {
        health--;

        slider.value = (float)health / maxHealth;
        PlayerManager.Instance.playershoot.GetComponent<PlayerShoot>().ammo--;
        if (health <= 0)
        {
            Debug.Log("dying");
            Destroy(Instantiate(deathParticles,transform.position,Quaternion.identity),deathParticlesLength);
            Destroy(this.gameObject);
            StartCoroutine(Win());
        }
    }

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(sceneToLoad);
    }

    public new void OnDestroy()
    {
        foreach (GameObject sn in snakes.ToList())
        {
            snakes.Remove(sn);
            Destroy(sn);
        }
        Instance = null;
        base.OnDestroy();
    }

    public void RefreshBullet()
    {
        Destroy(currentRefresh);
        currentRefresh = Instantiate(refresh, bulletRefreshes[Random.Range(0,bulletRefreshes.Length)]);
    }
}