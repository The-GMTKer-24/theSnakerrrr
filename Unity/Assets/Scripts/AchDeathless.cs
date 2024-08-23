using UnityEngine;
using UnityEngine.UI;

public class AchDeathless : MonoBehaviour
{
    private void Update()
    {
        Image img = this.GetComponent<Image>();
        
        transform.GetChild(0).gameObject.SetActive(Achievements.Deathless);
        img.enabled = Achievements.Deathless;
    }
}
