using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Rigidbody2D target;
    [SerializeField] private float strength;
    [SerializeField] private CameraShake shake;
    [SerializeField] private float defaultDistance;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private AnimationCurve zoomAmount;
    [SerializeField] private float zoomScale;
    [SerializeField] private float power;

    private Vector3 lastSeenAt;
    private void FixedUpdate()
    {
        Vector2 next;
        float targetDepth = defaultDistance;
        if (target)
        {
            Vector2 current = transform.position;
            lastSeenAt = target.position;
            next = (Vector2)target.transform.position * strength + current * (1 - strength);
            if (Math.Abs(target.velocity.x) > minSpeed)
            {
                targetDepth += zoomAmount.Evaluate(  1 + (MathF.Min(Math.Abs(target.velocity.x),maxSpeed) - maxSpeed) / (maxSpeed - minSpeed)) * zoomScale;
            }
            else if (PlayerManager.Instance.player.GetComponent<PlayerMovement>().LastGroundTime < 0)
            {
                targetDepth = this.transform.position.z;
            }
        }
        else
        {
            next  = (Vector2)lastSeenAt * strength + (Vector2)transform.position * (1 - strength);
            targetDepth = this.transform.position.z;
        }


        Vector3 newPosition = new Vector3(next.x, next.y, Mathf.Lerp(transform.position.z,targetDepth,power)) + shake.currentShake;
        transform.position = newPosition;
    }
    
}
