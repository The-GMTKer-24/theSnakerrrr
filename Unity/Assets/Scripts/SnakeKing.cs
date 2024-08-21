using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WumpusUnity.Battle;
using Random = UnityEngine.Random;

public class SnakeKing : Enemy
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
    public GameObject enemyProjectilePrefab;
    public float projSpeed;
    
    private List<GameObject> snakes;
    private GameObject currentRefresh;
    private float timer;

    
    private bool won;
    public void Awake()
    {
        snakes = new List<GameObject>();
        Instance = this;
        maxHealth *= 2;
        health = maxHealth;
    }

    public void FixedUpdate()
    {
        if (won)
        {
            return;
        }
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

    public void Update()
    {
        
    }

    public override void Die()
    {
        health--;
        RefreshBullet();
        slider.value = (float)health / maxHealth;
        PlayerManager.Instance.playershoot.GetComponent<PlayerShoot>().ammo--;
        LaunchProj();
        
        if (health <= 0)
        {
            won = true;
            Debug.Log("dying");
            StartCoroutine(Win());
            GameObject particles = Instantiate(deathParticles,transform.position,Quaternion.identity);
            Destroy(particles,deathParticlesLength);

        }
    }

    private void LaunchProj()
    {
        if (PlayerManager.Instance)
        {
            if (PlayerManager.Instance.player)
            {
                GameObject player = PlayerManager.Instance.player;
                GameObject newObj = Instantiate(enemyProjectilePrefab, transform.position, Quaternion.identity);
                HomingController controller = newObj.GetComponent<HomingController>();
                controller.target = player.GetComponent<Rigidbody2D>();
                controller.startingPosition = this.transform.position;
                controller.startingVelocity = (player.transform.position - this.transform.position).normalized * projSpeed;
            }

        }

    }

    private IEnumerator Win()
    {
        Debug.Log("Winning");
        yield return new WaitForSeconds(timeToWait);
        StatPasser.Instance.Nuke(0.1f,sceneToLoad);

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