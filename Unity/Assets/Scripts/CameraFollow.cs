using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] private float strength;
    [SerializeField] private CameraShake shake;

    private void FixedUpdate()
    {
        Vector2 next;
        if (target)
        {
            Vector2 current = transform.position;

            next = (Vector2)target.transform.position * strength + current * (1 - strength);
        }
        else
        {
            next = transform.position;
        }
        
        Vector3 newPosition = new Vector3(next.x, next.y, this.transform.position.z) + shake.currentShake;
        transform.position = newPosition;
    }
    
}
