using UnityEngine;

namespace Character
{
    public class PlayerMouseLook : MonoBehaviour
    {
        public float mouseSensitivity = 100f;
        
        private PlayerInputController _control;
        private Vector2 _mouseLook;
        private float _xRotation = 0f;
        public Transform playerBody;

        private void Awake()
        {
            _control = new PlayerInputController();
        }

        private void Update()
        {
            Look();
        }

        private void Look()
        {
            _mouseLook = _control.Main.Look.ReadValue<Vector2>();

            var mouseSensitivityDelta = mouseSensitivity * Time.deltaTime;
            var mouseX = _mouseLook.x * mouseSensitivityDelta;
            var mouseY = _mouseLook.y * mouseSensitivityDelta;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation,-90f, 90f);
            
            transform.localRotation = Quaternion.Euler(_xRotation,0,0);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        private void OnEnable()
        {
            _control.Enable();
        }

        private void OnDisable()
        {
            _control.Disable();
        }
    }
}
