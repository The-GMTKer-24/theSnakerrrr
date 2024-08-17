using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private float strength;
    [SerializeField] private float Z;

    private void FixedUpdate()
    {
        Vector2 target = player.position;
        Vector2 current = this.GetComponent<Transform>().position;

        Vector2 next = target * strength + current * (1 - strength);

        Vector3 newPosition = new Vector3(next.x, next.y, Z);
        this.GetComponent<Transform>().position = newPosition;
    }
}