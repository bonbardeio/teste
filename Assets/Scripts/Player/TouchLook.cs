using UnityEngine;
using UnityEngine.EventSystems;

namespace MobileFPS.Player
{
    /// <summary>
    /// Controle de câmera por toque (arrastar no lado direito da tela).
    /// </summary>
    public class TouchLook : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        [SerializeField] private Transform playerBody;
        [SerializeField] private float sensitivity = 0.15f;
        [SerializeField] private float minVertical = -70f;
        [SerializeField] private float maxVertical = 75f;

        private Vector2 lastPointer;
        private float xRotation;

        public void OnPointerDown(PointerEventData eventData)
        {
            lastPointer = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 delta = eventData.position - lastPointer;
            lastPointer = eventData.position;

            float mouseX = delta.x * sensitivity;
            float mouseY = delta.y * sensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minVertical, maxVertical);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
