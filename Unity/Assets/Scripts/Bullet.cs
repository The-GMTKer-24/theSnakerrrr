using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private float duration;
    public static List<Bullet> bullets;

    public void OnApplicationQuit()
    {
        bullets.Clear();
    }

    public void Awake()
    {
        bullets ??= new List<Bullet>();
        bullets.Add(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Splode();
    }

    public void Splode()
    {
        GameObject particle = Instantiate(particleSystem, transform.position, quaternion.identity);
        Destroy(particle,duration);
        Destroy(this.gameObject);
        bullets.Remove(this);
    }

    public void OnDestroy()
    {
        bullets.Remove(this);
    }

    public void OnPlayerDeath()
    {
        Splode();
    }
}