using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private LineRenderer rend;
    [SerializeField] private LayerMask hittable;
    [SerializeField] private AnimationCurve width;
    [SerializeField] private float lifetime;
    [SerializeField] private float particleLifeTime;
    [SerializeField] private GameObject impactParticles;
    private bool started;
    private float timer;
    private Transform source;
    public void SetMoveDirection(Vector2 direction, Transform source)
    {
        this.source = source;
        rend.SetPosition(0,transform.position);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction, 1000, hittable);
        if (hit.collider != null)
        {
            rend.SetPosition(1, hit.point);
            GameObject particles =Instantiate(impactParticles, hit.point, Quaternion.identity);
            Destroy(particles,particleLifeTime);
        }
        else
        {
            rend.SetPosition(1,direction * 1000 + (Vector2)transform.position);
        }

        started = true;
    }

    private void Update()
    {
        if (started)
        {
            rend.SetPosition(0,source.position);
            timer += Time.deltaTime;
            float targetWidth = width.Evaluate(timer);
            rend.startWidth = targetWidth;
            rend.endWidth = targetWidth;
            if (timer > lifetime)
            {
                Destroy(this.gameObject);
            }
        }
    }
}