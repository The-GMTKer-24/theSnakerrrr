using UnityEngine;
using UnityEngine.UI;

public class AchCollector : MonoBehaviour
{
    private void Update()
    {
        Image img = this.GetComponent<Image>();
        
        transform.GetChild(0).gameObject.SetActive(Achievements.Collector);
        img.enabled = Achievements.Collector;
    }
}
