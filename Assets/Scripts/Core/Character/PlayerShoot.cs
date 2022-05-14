using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Character
{
    public class PlayerShoot : MonoBehaviour
    {
        private PlayerInputController _playerInput;
        public static Action shootInput;
        public static Action reloadInput;

        public void Awake()
        {
            _playerInput = new PlayerInputController();

            _playerInput.Main.Reload.started += ReloadAction;
            _playerInput.Main.Shoot.started += ShootAction;
        }

        private static void ShootAction(InputAction.CallbackContext obj)
        {
            shootInput?.Invoke();
        }

        private static void ReloadAction(InputAction.CallbackContext obj)
        {
            reloadInput?.Invoke();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }
    }
}