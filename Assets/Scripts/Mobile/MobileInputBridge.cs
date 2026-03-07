using MobileFPS.Player;
using UnityEngine;

namespace MobileFPS.Mobile
{
    /// <summary>
    /// Faz ponte entre botões da UI touch e lógica do jogador.
    /// </summary>
    public class MobileInputBridge : MonoBehaviour
    {
        [SerializeField] private PlayerMotor playerMotor;

        private bool isAiming;

        public void OnFirePressed()
        {
            playerMotor.FirePressed(isAiming);
        }

        public void OnReloadPressed()
        {
            playerMotor.ReloadPressed();
        }

        public void OnJumpPressed()
        {
            playerMotor.RequestJump();
        }

        public void SetAiming(bool aiming)
        {
            isAiming = aiming;
        }
    }
}
