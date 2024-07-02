using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{
    public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool hold;
        public bool IsHold => hold;

        public void OnPointerUp(PointerEventData eventData)
        {
            hold = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            hold = true;
        }
    }
}
