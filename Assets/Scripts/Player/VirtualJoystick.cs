using UnityEngine;
using UnityEngine.EventSystems;

namespace MobileFPS.Player
{
    /// <summary>
    /// Joystick virtual simples para toque.
    /// Configure um painel (fundo) e um handle (alavanca) na UI.
    /// </summary>
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform background;
        [SerializeField] private RectTransform handle;
        [SerializeField] private float handleRange = 80f;

        public Vector2 InputVector { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out var localPoint))
                return;

            Vector2 radius = background.sizeDelta / 2f;
            Vector2 normalized = new Vector2(localPoint.x / radius.x, localPoint.y / radius.y);
            InputVector = Vector2.ClampMagnitude(normalized, 1f);

            handle.anchoredPosition = InputVector * handleRange;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            InputVector = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
        }
    }
}
