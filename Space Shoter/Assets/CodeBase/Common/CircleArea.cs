
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Common
{
    public class CircleArea : MonoBehaviour
    {
        [Header("Отображать Радиус в инспекторе?")]
        [SerializeField] bool DrawGizmos;
        [Space]

        [Header("Радиус")]
        [SerializeField] private float radius;
        public float Radius => radius;


        public Vector2 GetRandomInsideZone()
        {
            return (Vector2)transform.position + (Vector2)Random.insideUnitSphere * radius;
        }

        private static Color GizmoColor = new Color(0f, 1f, 0f, 0.2f);

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (DrawGizmos)
            {
                Handles.color = GizmoColor;
                Handles.DrawWireDisc(transform.position, transform.forward, radius);
            }
        }
#endif
    }
}