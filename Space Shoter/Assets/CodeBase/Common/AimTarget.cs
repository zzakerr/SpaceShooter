using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Common
{
    public class AimTarget : MonoBehaviour
    {
        [SerializeField] private Aim aim;
        private bool isTarget;
        public bool IsTarget => isTarget;
        private Event newTarget;
        private SpriteRenderer sprite;

        private void Start()
        {
                sprite = GetComponent<SpriteRenderer>();
        }
        
        private void Update()
        {
            if (aim != null)
            {
                if (Vector2.Distance(aim.transform.position, transform.position) > aim.Radius || aim.NoTarget == true)
                {
                    sprite.enabled = false;
                    isTarget = false;
                }
                else
                {
                    sprite.enabled = true;
                    isTarget = true;
                }

                if ((Vector2)transform.position != aim.AimTarget)
                {
                    transform.position = aim.AimTarget;
                }
            }
            if (aim == null)
            {
                // Âðåìåííî.
                aim = FindAnyObjectByType<Aim>();
            }
        }
        /// <summary>
        /// Âîçâðàùàåò ïîçèöèþ öåëè
        /// </summary>
        /// <returns></returns>
        public Vector2 GetTargetPosition()
        {
            return transform.position;
        }

    }
}
