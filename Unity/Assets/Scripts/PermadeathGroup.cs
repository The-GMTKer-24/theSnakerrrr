using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermadeathGroup : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;

    private void FixedUpdate()
    {
        foreach (Enemy enemy in enemies)
        {
            if (!PlayerManager.Instance.DeadEnemies.Contains(enemy))
            {
                return;
            }
        }

        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}