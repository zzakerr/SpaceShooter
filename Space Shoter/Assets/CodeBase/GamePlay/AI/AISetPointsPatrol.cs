using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AISetPointsPatrol : MonoBehaviour
{
    [Header("Отображать Точки в инспекторе?")]
    [SerializeField] bool DrawGizmos;
    [Space]

    [Header("Размер точки")]
    [SerializeField][Range(0.1f, 1f)] float radius;
    public float Radius => radius;
    [Space]

    private List<Vector3> points;
    public List<Vector3> Points => points;

    private void Awake()
    {
        points = new List<Vector3>();
        for (int i = 0; i < transform.childCount; i++)
        {
            points.Add(transform.GetChild(i).position);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (DrawGizmos)
        {
            Handles.color = Color.red;
            for (int i = 0; i < transform.childCount; i++)
            {
                Handles.DrawSolidDisc(transform.GetChild(i).position, transform.forward, radius);
            }           
        }
    }
#endif

}
