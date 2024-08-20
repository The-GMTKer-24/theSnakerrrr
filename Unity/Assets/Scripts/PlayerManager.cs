using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private bool dying;
    public List<Enemy> DeadEnemies;

    private bool willDie;
    
    public void Awake()
    {
        Instance = this;
    }

    public void Die()
    {
        if (dying)
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

        Destroy(Instantiate(deathParticles, player.transform.position, Quaternion.identity),5);
        upgrades = upgradeManager.GetComponent<UpgradesManager>().upgrades;
        Destroy(player);
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