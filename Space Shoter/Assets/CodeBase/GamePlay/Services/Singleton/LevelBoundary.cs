using UnityEngine;

public class LevelBoundary : SingletonBase<LevelBoundary>
{
    [SerializeField] private float m_Radius;
    public float Radius => m_Radius;

    public enum Mode
    {
        Limit,
        Teleport
    }

    [SerializeField] private Mode m_LimiteMode;
    public Mode LimitMode => m_LimiteMode;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position,transform.forward,m_Radius);

    }
#endif
}
