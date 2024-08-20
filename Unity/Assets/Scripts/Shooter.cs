using System;
using UnityEngine;

public class Shooter : Enemy
{
    [SerializeField] GameObject enemyProjectilePrefab;
    [SerializeField] private float shotDelay;
    [SerializeField] private LayerMask hittable;
    [SerializeField] private float projSpeed;

    private float shotTimer;

    public void Update()
    {
        shotTimer += Time.deltaTime;

        if (shotTimer > shotDelay)
        {
            GameObject player = PlayerManager.Instance.player;
            if (!player)
                return;
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position,
                 player.transform.position - this.transform.position, 1000, hittable);
            if (!hit.collider.CompareTag(playerTag)) return;
            
            
            
            GameObject newObj = Instantiate(enemyProjectilePrefab, transform.position, Quaternion.identity);
            newObj.GetComponent<Rigidbody2D>().velocity = (player.transform.position - this.transform.position).normalized * projSpeed;
            shotTimer = 0;
        }
    }
}