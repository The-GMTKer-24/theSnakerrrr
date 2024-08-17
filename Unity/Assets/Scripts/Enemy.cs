using System;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private float deathParticlesLength;
    [SerializeField] private string playertag = "Player";
    public void Die()
    {
        Debug.Log("DEAD!");
        Destroy(Instantiate(deathParticles,transform.position,quaternion.identity),5);
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("hit");

        if (other.gameObject.CompareTag(playertag))
        {
            Debug.Log("hit");
            PlayerManager.Instance.Die();
        }
    }
}