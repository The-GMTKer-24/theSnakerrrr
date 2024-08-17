using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    [SerializeField] private CameraShake shake;
    [SerializeField] private float depth = -10;
    Vector3 currentVelocity;
    private void LateUpdate()
    {
        Vector2 delta = target.position - transform.position;
        
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref currentVelocity, smoothTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, depth) + shake.currentShake;
    }
}
