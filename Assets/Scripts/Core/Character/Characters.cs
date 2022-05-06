using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Character
{
    public class Character : MonoBehaviour
    {
        //References Variables
        private CharacterController _charController;
        private PlayerInputController _playerInput;

        //Store playerInput Variables
        private Vector2 _movementInput;
        private Vector3 _currentMovement;
        private Vector3 _movementY;
        private bool _isMovementPressed;

        //Movement Variables
        [SerializeField] protected float runSpeed;
        [SerializeField] protected float walkSpeed;
        private bool _isWalkPressed;
        
        //Camera Variables
        private float _mouseSensitivity;
        private Vector2 _mouseInput;
        private Transform _headObject;
        private float _cameraRotX = 0f;
        private Camera _camera;

        //Gravity Variables
        private float _gravity;
        private const float GroundedGravity = -2.0f;

        //Jumping Variables
        private bool _isJumpPressed = false;
        private float _initialJumpVelocity;
        private bool _isJumping = false;
        private const float MaxJumpHeight = 1f;
        private const float MaxJumpTime = 0.5f;

        //GroundCheck
        private Transform _groundCheck;
        private bool _isGrounded;
        private const float CheckRadius = 0.5f;
        private LayerMask _groundMask;

        private void Awake()
        {
            _playerInput = new PlayerInputController();
            _charController = GetComponent<CharacterController>();
            
            _groundCheck = transform.Find("GroundCheck");
            _headObject = transform.Find("Head");
            _camera = Camera.main;

            _groundMask = LayerMask.GetMask("Ground");

            _playerInput.Main.Movement.performed += OnMovementInput;
            _playerInput.Main.Movement.canceled += OnMovementInput;
            _playerInput.Main.Jump.started += OnJumpInput;
            _playerInput.Main.Jump.canceled += OnJumpInput;
            _playerInput.Main.Walk.started += OnWalkInput;
            _playerInput.Main.Walk.canceled += OnWalkInput;
            _playerInput.Main.Look.performed += OnMouseInput;
            _playerInput.Main.Look.canceled += OnMouseInput;

            SetupJumpVariables();
        }

        private void SetupJumpVariables()
        {
            const float timeToApex = MaxJumpTime / 2;
            _gravity = (-2 * MaxJumpHeight) / Mathf.Pow(timeToApex, 2);
            _initialJumpVelocity = (2 * MaxJumpHeight) / timeToApex;
        }

        private void OnMouseInput(InputAction.CallbackContext obj)
        {
            _mouseInput = obj.ReadValue<Vector2>();
        }

        private void OnWalkInput(InputAction.CallbackContext obj)
        {
            _isWalkPressed = obj.ReadValueAsButton();
        }

        private void OnJumpInput(InputAction.CallbackContext obj)
        {
            _isJumpPressed = obj.ReadValueAsButton();
        }

        private void OnMovementInput(InputAction.CallbackContext obj)
        {
            _movementInput = obj.ReadValue<Vector2>();
            _isMovementPressed = _movementInput.x != 0 || _movementInput.y != 0;
        }

        private void Update()
        {
            HandleGroundCheck();
            HandleMovement();
            HandleGravity();
            HandleJump();
            
            MouseMovement();

            //Character Movement
            _currentMovement.y = MathF.Max(_movementY.y, -20.0f);
            _charController.Move(_currentMovement * Time.deltaTime);
        }

        private void MouseMovement()
        {
            var mouseSensitivityDelta = _mouseSensitivity * Time.deltaTime;
            var mouseX = _mouseInput.x * mouseSensitivityDelta;
            var mouseY = _mouseInput.y * mouseSensitivityDelta;

            _cameraRotX -= mouseY;
            _cameraRotX = Mathf.Clamp(_cameraRotX, -90f, 90);

            _headObject.transform.localRotation = Quaternion.Euler(_cameraRotX, 0,0);
            transform.Rotate(Vector3.up * mouseX);
        }

        private void HandleGroundCheck()
        {
            _isGrounded = Physics.CheckSphere(_groundCheck.position, CheckRadius, _groundMask);
        }

        private void HandleJump()
        {
            if (!_isJumping && _charController.isGrounded && _isJumpPressed)
            {
                _isJumping = true;
                _movementY.y = _isWalkPressed
                    ? (_initialJumpVelocity / 2)
                    : _initialJumpVelocity;
            }
            else if (!_isJumpPressed && _isJumping && _charController.isGrounded)
            {
                _isJumping = false;
            }
        }

        private void HandleMovement()
        {
            var yTransform = transform;
            _currentMovement = (_movementInput.x * yTransform.right) + (_movementInput.y * yTransform.forward);

            _currentMovement *= _isWalkPressed switch { true when _isGrounded => walkSpeed, _ => runSpeed };
        }

        private void HandleGravity()
        {
            if (_charController.isGrounded)
            {
                _movementY.y = GroundedGravity;
            }
            else
            {
                _movementY.y += _gravity * Time.deltaTime;
            }
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