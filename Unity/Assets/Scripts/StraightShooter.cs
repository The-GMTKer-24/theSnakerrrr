using System;
using UnityEngine;

public class StraightShooter : Enemy
{
    [SerializeField] GameObject enemyProjectilePrefab;
    [SerializeField] private float shotDelay;
    [SerializeField] private float projSpeed;
    

    private float shotTimer;

    public void Update()
    {
        shotTimer += Time.deltaTime;

        if (shotTimer > shotDelay)
        {
            GameObject newObj = Instantiate(enemyProjectilePrefab, transform.position, Quaternion.identity);
            newObj.GetComponent<Rigidbody2D>().velocity = -transform.right * projSpeed;
            
            shotTimer = 0;
        }
    }
}