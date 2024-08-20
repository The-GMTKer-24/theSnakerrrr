using System;
using Unity.Mathematics;
using UnityEngine;

public class SnakeKingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject snakeKing;
    [SerializeField] private Transform spawnLocation;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (SnakeKing.Instance == null)
            Instantiate(snakeKing, spawnLocation.position, quaternion.identity);
    }
}