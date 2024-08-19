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
    [SerializeField] private String enemyTag;
    [SerializeField] private float damageWidth;
    private bool started;
    private float timer;
    private Transform source;
    public void SetMoveDirection(Vector2 direction, Transform source)
    {
        this.source = source;
        rend.SetPosition(0,transform.position);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction, 1000, hittable);

        bool hitSomething = false;

        if (hit.collider != null)
        {
            rend.SetPosition(1, hit.point);
            if (hit.collider.gameObject.CompareTag(enemyTag))
            {
                hit.transform.GetComponent<Enemy>().Die();
                PlayerShoot.Instance.AddAmmo();
                hitSomething = true;
            }
            GameObject particles =Instantiate(impactParticles, hit.point, Quaternion.identity);
            Destroy(particles,particleLifeTime);
        }
        else
        {
            rend.SetPosition(1,direction * 1000 + (Vector2)transform.position);
        }
        
        Vector2 p1 = rend.GetPosition(0);
        Vector2 p2 = rend.GetPosition(1);
        Vector2 delta = p2 - p1;
        float distance = Vector2.Distance(p1, p2);
        foreach (RaycastHit2D hits in Physics2D.BoxCastAll(p1,new Vector2(0.1f,damageWidth),0.0f,delta,distance))
        {
            Debug.Log(hits);
            if (hits.collider.CompareTag(enemyTag))
            {
                hits.transform.GetComponent<Enemy>().Die();
                PlayerShoot.Instance.AddAmmo();
                hitSomething = true;
            }
        }

        if (hitSomething)
        {
            Rigidbody2D rb = PlayerManager.Instance.player.GetComponent<Rigidbody2D>();

            if (rb.velocityY < 0)
            {
                rb.velocityY *= -1;
            }

        }
        started = true;
    }

    private void Update()
    {
        if (started)
        {
            timer += Time.deltaTime;
            float targetWidth = width.Evaluate(timer);
            rend.startWidth = targetWidth;
            rend.endWidth = targetWidth;
            if (timer > lifetime)
            {
                Destroy(this.gameObject);
            }
            if (source)
            {
                rend.SetPosition(0,source.position);
            }
            
        }
    }
}