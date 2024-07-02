using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ChangeColor : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private MeshRenderer mesh;
        [SerializeField] private int collorOffset;
        private int velocity;

        private void FixedUpdate()
        {
            if (rb != null)
            {
                velocity = (Mathf.Abs((int)rb.velocity.x) + Mathf.Abs((int)rb.velocity.y)) * collorOffset;
                mesh.material.SetColor("_EmissionColor", new Color32((byte)velocity, (byte)velocity, (byte)velocity, 140));
            }
        }


        public void SetTarget(SpaceShip target)
        {
            rb = target.GetComponent<Rigidbody2D>();
        }
    }
}