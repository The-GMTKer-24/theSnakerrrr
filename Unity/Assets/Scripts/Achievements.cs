using UnityEngine;

public static class Achievements
{
    public static bool Collector = false;
    public static bool Cheater = false;
    public static bool Speedrun = false;

    public static void GetCollector()
    {
        Collector = true;
    }

    public static void GetCheater()
    {
        Cheater = true;
    }

    public static void GetSpeedrun()
    {
        Speedrun = true;
    }
}