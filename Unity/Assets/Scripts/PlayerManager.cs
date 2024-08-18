using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [SerializeField] public int level1Collectables;
    [SerializeField] public GameObject deathParticles;
    [SerializeField] public float deathDuration;
    [SerializeField] public CameraFollow camera;
    [SerializeField] public Camera mouseCamera;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject playerPrefab;
    [SerializeField] public float deathShakeTime;
    [SerializeField] public AnimationCurve deathShake;
    private bool dying;
    public void Awake()
    {
        Instance = this;
    }

    public void Die()
    {
        if (dying)
            return;
        dying = true;
        if (Bullet.bullets != null)
        {
            foreach (Bullet bullet in Bullet.bullets.ToList())
            {
                bullet.Splode();
            }
        }

        Destroy(Instantiate(deathParticles, player.transform.position, Quaternion.identity),5);
        Destroy(player);
        camera.GetComponent<CameraShake>().Shake(deathShakeTime, deathShake);
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(deathDuration);
        player = Instantiate(playerPrefab, Checkpoints.LastCheckpoint.transform.position, Quaternion.identity);
        camera.target = player.GetComponent<Rigidbody2D>();
        dying = false;
    }
}