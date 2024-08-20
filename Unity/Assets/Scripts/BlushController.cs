using UnityEngine;
using UnityEngine.UI;

public class BlushController : MonoBehaviour
{
    void Update()
    {
        Image img = this.GetComponent<Image>();
        img.enabled = Achievements.Cheater && Achievements.Collector && Achievements.Speedrun;
    }
}
