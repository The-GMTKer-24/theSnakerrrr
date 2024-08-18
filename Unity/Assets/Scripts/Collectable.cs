using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private GameObject collectParticles;




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(collectParticles, transform.position, Quaternion.identity);
            PlayerManager.Instance.level1Collectables++;
            Destroy(gameObject);
        }
    }
}
