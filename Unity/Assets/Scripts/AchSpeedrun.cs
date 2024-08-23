using UnityEngine;
using UnityEngine.UI;

public class AchSpeedrun : MonoBehaviour
{
    private void Update()
    {
        Image img = this.GetComponent<Image>();
        
        transform.GetChild(0).gameObject.SetActive(Achievements.Speedrun);
        img.enabled = Achievements.Speedrun;
    }
}
