using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GravityWell: MonoBehaviour
{
    [SerializeField] private float m_Force;
    [SerializeField][Range(0.1f, 50f)] private float m_Radius;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.attachedRigidbody == null) return;

        Vector2 dir = transform.position - collision.transform.position;

        float dist = dir.magnitude;

        if (dist < m_Radius)
        {
            Vector2 force = dir.normalized * m_Force * (m_Radius/dist);
            collision.attachedRigidbody.AddForce(force,ForceMode2D.Force);
        }
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
        GetComponent<CircleCollider2D>().radius = m_Radius;
    }
    #endif
}
