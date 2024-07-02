using UnityEngine;

namespace Common
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float RotationSpeed;
        [SerializeField] private float PulseRange;

        private void Update()
        {
            if (RotationSpeed > 0) transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
        }

    }
}
