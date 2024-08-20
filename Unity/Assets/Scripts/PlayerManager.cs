using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [SerializeField] public int level1Collectables;
    [SerializeField] public GameObject playershoot;
    [SerializeField] public GameObject deathParticles;
    [SerializeField] public float deathDuration;
    [SerializeField] public CameraFollow camera;
    [SerializeField] public Camera mouseCamera;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject playerPrefab;
    [SerializeField] public float deathShakeTime;
    [SerializeField] public AnimationCurve deathShake;
    [SerializeField] public GameObject upgradeManager;
    [SerializeField] public PlayerMovement movement;
    [SerializeField] public Upgrades upgrades;

    public int deathCount;
    public float speedrunTime;
    public int maxCollected;
    
    private bool dying;
    public List<Enemy> DeadEnemies;

    private bool willDie;

    private StatPasser passer;
    public void Awake()
    {
        Instance = this;
        deathCount = 0;
        speedrunTime = 0;
        passer = (new GameObject("Stat passer")).AddComponent<StatPasser>();

    }

    public void OnDisable()
    {

    }

    public void OnDestroy()
    {

    }

    private void Update()
    {
        if (player)
        {
            Player component = player.GetComponent<Player>();
            if (component.timer)
                speedrunTime = component.timer.GetComponent<Clock>().GetTime();

        }

        passer.time = speedrunTime;
        passer.deaths = deathCount;
        passer.scales = maxCollected;
    }

    public void Die()
    {
        if (dying || !Instance)
            return;
        willDie = false;
        dying = true;
        if (Bullet.bullets != null)
        {
            foreach (Bullet bullet in Bullet.bullets.ToList())
            {
                bullet.Splode();
            }
        }

        // Update death count and time counter
        deathCount++;

        GameObject particles = Instantiate(deathParticles, player.transform.position, Quaternion.identity);
        Destroy(particles,5);
        upgrades = upgradeManager.GetComponent<UpgradesManager>().upgrades;
        Destroy(player);
        if (camera)
            camera.GetComponent<CameraShake>().Shake(deathShakeTime, deathShake);
        StartCoroutine(Respawn());
    }

    public void DelayPlayerKill(float time, float gravityIncrease)
    {
        camera.target = null;
        player.GetComponent<PlayerMovement>().SetGravityMult(gravityIncrease);
        StartCoroutine(DelayKill(time));
    }

    private IEnumerator DelayKill(float time)
    {
        willDie = true;
        yield return new WaitForSeconds(time);
        if (willDie)
            Die();
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(deathDuration);
        player = Instantiate(playerPrefab, Checkpoints.LastCheckpoint.transform.position, Quaternion.identity);
        playershoot = player.GetComponent<Player>().playerShoot;
        upgradeManager = player.GetComponent<Player>().upgradeManager;
        upgradeManager.GetComponent<UpgradesManager>().upgrades = upgrades;
        camera.target = player.GetComponent<Rigidbody2D>();

        // Set the visual death count and speedrun timer
        Player playerScript = player.GetComponent<Player>();
        playerScript.deathCount.GetComponent<TMP_Text>().text = deathCount.ToString();
        playerScript.timer.GetComponent<Clock>().InitializeTime(speedrunTime);
        
        foreach (Enemy enemy in DeadEnemies.ToList())
        {
            Debug.Log("Respawning "+ enemy);
            enemy.gameObject.SetActive(true);
            DeadEnemies.Remove(enemy);
        }

        if (SnakeKing.Instance)
        {
            Destroy(SnakeKing.Instance.gameObject);
        }
        dying = false;
    }

    public void EnableMovement()
    {
        movement.EnableInput();
    }
    
    public void DisableMovement()
    {
        movement.DisableInput();
    }
}