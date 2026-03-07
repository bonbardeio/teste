using MobileFPS.Weapons;
using UnityEngine;

namespace MobileFPS.Player
{
    /// <summary>
    /// Movimento em primeira pessoa para mobile: usa joystick + botão de pulo.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private VirtualJoystick movementJoystick;
        [SerializeField] private float moveSpeed = 4.5f;
        [SerializeField] private float jumpHeight = 1.2f;
        [SerializeField] private float gravity = -20f;
        [SerializeField] private WeaponManager weaponManager;

        private CharacterController controller;
        private Vector3 velocity;
        private bool jumpRequested;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 input = movementJoystick != null ? movementJoystick.InputVector : Vector2.zero;
            Vector3 move = transform.right * input.x + transform.forward * input.y;

            controller.Move(move * moveSpeed * Time.deltaTime);

            if (controller.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (jumpRequested && controller.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpRequested = false;
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        public void RequestJump()
        {
            jumpRequested = true;
        }

        public void FirePressed(bool aiming)
        {
            weaponManager.TryShoot(aiming);
        }

        public void ReloadPressed()
        {
            weaponManager.TryReload();
        }
    }
}
