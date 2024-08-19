using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPickuip : MonoBehaviour
{
    [SerializeField] private GameObject collectParticles;
    [SerializeField] private UpgradesManager upgradesManager;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(collectParticles, transform.position, Quaternion.identity);
            upgradesManager.ObtainGun();
            Destroy(gameObject);
        }
    }
}
