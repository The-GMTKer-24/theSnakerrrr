using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private float deathParticlesLength;
    public void Die()
    {
        Destroy(Instantiate(deathParticles,transform.position,quaternion.identity),5);
        Destroy(this.gameObject);
    }
}