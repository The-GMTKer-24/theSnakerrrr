using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private String playerTag;
        [SerializeField] private float refreshTime;
        [SerializeField] private AnimationCurve popout;
        [SerializeField] private AnimationCurve popin;
        [SerializeField] private float scale;
        private bool active;
        private float timeUntilRefresh;
        private float growTimer;
        private float shrinkTimer;
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(playerTag) && active)
            {
                if (!PlayerManager.Instance.playershoot)
                    return;
                PlayerManager.Instance.playershoot.GetComponent<PlayerShoot>().AddAmmo();
                active = false;
                timeUntilRefresh = refreshTime;
                shrinkTimer = 0;
            }
        }

        public void Update()
        {
            if (!active)
            {
                shrinkTimer += Time.deltaTime;
                transform.localScale = new Vector3(popin.Evaluate(shrinkTimer),popin.Evaluate(shrinkTimer),popin.Evaluate(shrinkTimer)) * scale;

                timeUntilRefresh -= Time.deltaTime;
                if (timeUntilRefresh < 0)
                {
                    active = true;
                    growTimer = 0;
                }
            }
            else
            {
                growTimer += Time.deltaTime;
                transform.localScale = new Vector3(popout.Evaluate(growTimer), popout.Evaluate(growTimer), popout.Evaluate(growTimer)) * scale;
            }
        }
    }
}