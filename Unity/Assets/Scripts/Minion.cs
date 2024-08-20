using System;
using UnityEngine;

public class Minion : Enemy
{
    public Rigidbody2D rb;
    private bool goingRight;
    [SerializeField]
    private float speed;

    private float lastColision;
    public override void Die()
    {
        Destroy(Instantiate(deathParticles,transform.position,Quaternion.identity),deathParticlesLength);
        Destroy(this.gameObject);
    }

    public void Update()
    {
        this.rb.velocity = (goingRight ? Vector2.right : Vector2.left) * speed;
        lastColision -= Time.deltaTime;
    }

    public void OnCollisionStay2D(Collision2D other)
    {
        if (lastColision <= 0)
        {
            goingRight = !goingRight;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            lastColision = 0.1f;
        }

    }
}