using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] private float lifeTimer;

        private void Start()
        {
            Destroy(gameObject, lifeTimer);
        }
    }
}