using UnityEngine;
using UnityEngine.UI;

public class AchDeathless : MonoBehaviour
{
    private void Update()
    {
        Image img = this.GetComponent<Image>();
        img.enabled = Achievements.Deathless;
    }
}
