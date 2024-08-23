using System;
using System.Collections;
using UnityEngine;

public static class Achievements
{
    public static bool Collector = false;
    public static bool Cheater = false;
    public static bool Speedrun = false;
    public static bool Deathless = false;
    
    private const int MINSCALES = 9;
    private const int MAXSCALES = 12;
    private const int TIMESECONDS = 480;
    private const int MAXDEATHS = 0;
    
    public static void GetCollector()
    {
        Collector = true;
        PlayerPrefs.SetInt("ACH_Collector", 1);
    }

    public static void GetCheater()
    {
        Cheater = true;
        PlayerPrefs.SetInt("ACH_Cheater", 1);
    }

    public static void GetSpeedrun()
    {
        Speedrun = true;
        PlayerPrefs.SetInt("ACH_Speedrun", 1);
    }

    public static void GetDeathless()
    {
        Deathless = true;
        PlayerPrefs.SetInt("ACH_Deathless", 1);
    }

    public static void Evaluate(StatPasser stats)
    {
        if (stats.scales == MAXSCALES || PlayerPrefs.GetInt("ACH_Collector", 0) == 1)
        {
            GetCollector();
        }

        if (stats.scales < MINSCALES || PlayerPrefs.GetInt("ACH_Cheater", 0) == 1)
        {
            GetCheater();
        }

        if (stats.time <= TIMESECONDS || PlayerPrefs.GetInt("ACH_Speedrun", 0) == 1)
        {
            GetSpeedrun();
        }

        if (stats.deaths <= MAXDEATHS || PlayerPrefs.GetInt("ACH_Deathless", 0) == 1)
        {
            GetDeathless();
        }
        
        // Save achievements
        PlayerPrefs.Save();
    }
}