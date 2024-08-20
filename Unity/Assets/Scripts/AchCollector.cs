using UnityEngine;
using UnityEngine.UI;

public class AchCollector : MonoBehaviour
{
    private void Update()
    {
        Image img = this.GetComponent<Image>();
        img.enabled = Achievements.Collector;
    }
}
