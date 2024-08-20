using UnityEngine;
using UnityEngine.Serialization;

public class Shopkeeper : MonoBehaviour
{
    [FormerlySerializedAs("Overlay")] [SerializeField] private GameObject overlay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PlayerManager.Instance)
                if (PlayerManager.Instance.playershoot)
                    PlayerManager.Instance.playershoot.GetComponent<PlayerShoot>().temporarilyDisabled = true;
            overlay.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PlayerManager.Instance)
                if (PlayerManager.Instance.playershoot)
                    PlayerManager.Instance.playershoot.GetComponent<PlayerShoot>().temporarilyDisabled = false;
            overlay.SetActive(false);
        }
    }
}
