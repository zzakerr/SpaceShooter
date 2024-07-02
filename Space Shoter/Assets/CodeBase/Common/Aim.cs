using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(CircleArea))]
    public class Aim : MonoBehaviour
    {
        private CircleArea area;
        public float Radius => area.Radius;

        private Vector2 aim;
        public Vector2 AimTarget => aim;

        private bool noTarget;
        public bool NoTarget => noTarget;

        private void Start()
        {
            area = GetComponent<CircleArea>();
        }

        private void FixedUpdate()
        {
            GetAimPosition();
        }

        // Изменяет положение Vector2 ближайшего обьекта.
        public void GetAimPosition()
        {
            Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, area.Radius);

            if (hitTargets.Length > 1)
            {
                noTarget = false;
                float closestDistance = Mathf.Infinity;
                foreach (Collider2D col in hitTargets)
                {
                    if (col.transform.root.GetComponent<Aim>()) continue;
                    float distance = Vector3.Distance(transform.position, col.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        aim = col.transform.position;
                    }
                }
            }
            if (hitTargets.Length == 1)
            {
                noTarget = true;
            }
        }

    }

}
