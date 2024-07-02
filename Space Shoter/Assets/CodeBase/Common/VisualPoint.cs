using UnityEngine;

namespace Common
{
    public class VisualPoint : MonoBehaviour
    {
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            int count = transform.parent.childCount;

            for (int i = 0; i < count; i++)
            {
                Vector3 point = transform.parent.GetChild(i).transform.position;

                Gizmos.DrawSphere(point, GetComponentInParent<AISetPointsPatrol>().Radius);
            }
        }
    }
}