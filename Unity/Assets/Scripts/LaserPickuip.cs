using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPickuip : MonoBehaviour
{
    [SerializeField] private GameObject collectParticles;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("collided");
            Instantiate(collectParticles, transform.position, Quaternion.identity);
            PlayerManager.Instance.upgradeManager.GetComponent<UpgradesManager>().ObtainGun();
            Destroy(gameObject);
        }
    }
}
