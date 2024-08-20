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



    public virtual void Die()
    {
        Destroy(Instantiate(deathParticles,transform.position,quaternion.identity),deathParticlesLength);
        PlayerManager.Instance.DeadEnemies.Add(this);
        this.gameObject.SetActive(false);
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