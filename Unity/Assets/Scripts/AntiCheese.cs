using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiCheese : MonoBehaviour
{
    private void OnDisable()
    {
        if (PlayerManager.Instance.playershoot && enabled)
        {
            PlayerManager.Instance.playershoot.GetComponent<PlayerShoot>().ammo = -1;
            if (SnakeKing.Instance)
                SnakeKing.Instance.RefreshBullet();
        }
    }
}
