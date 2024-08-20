using UnityEngine;
using UnityEngine.UI;

public class AchSpeedrun : MonoBehaviour
{
    private void Update()
    {
        Image img = this.GetComponent<Image>();
        img.enabled = Achievements.Speedrun;
    }
}
