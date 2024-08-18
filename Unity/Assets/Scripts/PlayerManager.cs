using System;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [SerializeField] public GameObject deathParticles;
    [SerializeField] public float deathDuration;
    [SerializeField] public CameraFollow camera;
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
        Destroy(Instantiate(deathParticles, player.transform.position, Quaternion.identity),5);
        Destroy(player);
        camera.GetComponent<CameraShake>().Shake(deathShakeTime, deathShake);
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(deathDuration);
        player = Instantiate(playerPrefab, Checkpoints.LastCheckpoint.transform.position, Quaternion.identity);
        player.GetComponent<PlayerShoot>().camera = camera.GetComponent<Camera>();
        camera.target = player.transform;
        dying = false;
    }
}