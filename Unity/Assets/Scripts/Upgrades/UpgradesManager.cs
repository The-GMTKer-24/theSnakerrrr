﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private bool jetbootsEnabled;
    [SerializeField] private bool grappleEnabled;

    [SerializeField] private GameObject jetboots;
    [SerializeField] private GameObject grappleHooks;

    [SerializeField] public int grapplePrice;
    [SerializeField] public int jetbootsPrice;
    void Start()
    {
        RefreshUpgrades();
    }

    void RefreshUpgrades()
    {

        jetboots.SetActive(jetbootsEnabled);
        grappleHooks.SetActive(grappleEnabled);
    }


    public void ObtainJetboots()
    {
        if (PlayerManager.Instance.level1Collectables < jetbootsPrice)
        {
            return;
        }

        PlayerManager.Instance.level1Collectables -= jetbootsPrice;
        jetbootsEnabled = true;
        RefreshUpgrades();
    }

    public void ObtainGrapplingHook()
    {
        if (PlayerManager.Instance.level1Collectables < grapplePrice)
        {
            return;
        }

        PlayerManager.Instance.level1Collectables -= grapplePrice;
        grappleEnabled = true;
        RefreshUpgrades();
    }
}
