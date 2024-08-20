using System;
using Unity.Mathematics;
using UnityEngine;

public class SnakeKingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject snakeKing;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private AudioSource ambientMusic;
    [SerializeField] private AudioSource fightMusic;
    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (SnakeKing.Instance)
            return;
        if (SnakeKing.Instance == null)
            Instantiate(snakeKing, spawnLocation.position, quaternion.identity);

        // MUSIC
        ambientMusic.Stop();
        if (!fightMusic.isPlaying)
        {
            fightMusic.Play();
        }
    }

    private void Update()
    {
        if (!SnakeKing.Instance)
        {
            if (!ambientMusic.isPlaying)
                ambientMusic.Play();
            if (fightMusic.isPlaying)
                fightMusic.Stop();
        }
    }
}