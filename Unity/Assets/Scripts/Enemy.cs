using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject deathParticles;
    [SerializeField] protected float deathParticlesLength;
    [SerializeField] protected string playerTag = "Player";
    [SerializeField] private List<Enemy> real;
    [SerializeField] private bool permadeath = false;



    public virtual void Die()
    {
        GameObject particles = Instantiate(deathParticles, transform.position, quaternion.identity);
        Destroy(particles, deathParticlesLength);
        PlayerManager.Instance.DeadEnemies.Add(this);
        this.gameObject.SetActive(false);
        if (permadeath)
        {
            Destroy(this);
        }
    }

    public void OnDestroy()
    {
        PlayerManager.Instance.DeadEnemies.Remove(this);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            PlayerManager.Instance.Die();
        }
    }
}