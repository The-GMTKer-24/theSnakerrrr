using UnityEngine;
using UnityEngine.UI;

public class AchCheater : MonoBehaviour
{
    private void Update()
    {
        Image img = this.GetComponent<Image>();
        
        transform.GetChild(0).gameObject.SetActive(Achievements.Cheater);
        img.enabled = Achievements.Cheater;
    }
}
