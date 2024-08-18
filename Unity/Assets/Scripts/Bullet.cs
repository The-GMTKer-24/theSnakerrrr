using System;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject particleSystem;
    [SerializeField] private float duration;
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(Instantiate(particleSystem,transform.position,quaternion.identity),duration);
        Destroy(this.gameObject);
    }
}