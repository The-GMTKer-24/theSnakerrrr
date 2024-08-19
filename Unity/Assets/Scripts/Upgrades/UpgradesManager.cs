﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] public Upgrades upgrades;

    [SerializeField] private GameObject jetboots;
    [SerializeField] private GameObject grappleHooks;
    [SerializeField] private GameObject gunObject;

    [SerializeField] public int grapplePrice;
    [SerializeField] public int jetbootsPrice;

    [SerializeField] private GameObject grappleIcon;
    [SerializeField] private GameObject bootsIcon;
    [SerializeField] private GameObject gunIcon;

    void Start()
    {
        RefreshUpgrades();
    }

    void RefreshUpgrades()
    {

        jetboots.SetActive(upgrades.rocketBoots);
        bootsIcon.SetActive(upgrades.rocketBoots);

        grappleHooks.SetActive(upgrades.grapplingHook);
        grappleIcon.SetActive(upgrades.grapplingHook);

        gunObject.SetActive(upgrades.laserGun);
        gunIcon.SetActive(upgrades.laserGun);
    }

    private void FixedUpdate()
    {
        RefreshUpgrades();
    }

    public void ObtainJetboots()
    {
        if (PlayerManager.Instance.level1Collectables < jetbootsPrice)
        {
            return;
        }

        PlayerManager.Instance.level1Collectables -= jetbootsPrice;
        upgrades.rocketBoots = true;
        RefreshUpgrades();
    }

    public void ObtainGrapplingHook()
    {
        if (PlayerManager.Instance.level1Collectables < grapplePrice)
        {
            return;
        }

        PlayerManager.Instance.level1Collectables -= grapplePrice;
        upgrades.grapplingHook = true;
        RefreshUpgrades();
    }

    public void ObtainGun()
    {
        upgrades.laserGun = true;
        RefreshUpgrades();
    }
}
