using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _sprintMultiplier;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private bool _invertMouse;
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _gravity = -9.81f;

    [Header("Shooting")]
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _shootForce;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundCheckDistance;

    [Header("Interactable")]
    [SerializeField] private LayerMask _buttonLayer;
    [SerializeField] private float _rayCastDistance;

    [Header("Selectable")]
    [SerializeField] private LayerMask _pickUpLayer;
    [SerializeField] private float _pickUpDistance;
    [SerializeField] private Transform _attachPoint;

    private CharacterController _characterController;
    private float _horizontal, _vertical;
    private float _mouseX, _mouseY;
    private float _cameraRotationX;
    private float _moveMultiplier = 1.0f;
    private Vector3 _playerVelocity;
    private bool _isGrounded;
    private bool _isPicked;
    private Camera _mainCamera;
    private RaycastHit _rayCastHit;
    private ISelectable _selectable;
    private IPickable _pickable;

    // Start is called before the first frame update
    private void Start()
    {
        _mainCamera = Camera.main;
        _characterController = GetComponent<CharacterController>();

        //Hides mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
        MovePlayer();
        RotatePlayer();
        GroundCheck();
        JumpCheck();
        ShootBullet();
        Interact();
        PickAndDrop();
        //Debug.DrawRay(_bulletSpawnPoint.position, _bulletSpawnPoint.forward, Color.red);
    }

    private void GetInput()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _moveMultiplier = Input.GetButton("Fire3") ? _sprintMultiplier : 1.0f;
    }

    private void MovePlayer()
    {
        _characterController.Move((transform.forward * _vertical + transform.right * _horizontal) * _playerSpeed * Time.deltaTime * _moveMultiplier);

        // Ground check
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2.0f;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }
    
    private void RotatePlayer()
    {
        // Turn player
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _mouseX);

        // Camera up and down
        _cameraRotationX += Time.deltaTime * _mouseY * _turnSpeed * (_invertMouse ? 1 : -1);
        _cameraRotationX = Mathf.Clamp(_cameraRotationX, -85.0f, 85.0f);

        _cameraTransform.localRotation = Quaternion.Euler(_cameraRotationX, 0, 0);
    }

    private void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundMask);
    }

    private void JumpCheck()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _playerVelocity.y = _jumpVelocity;
        }
    }

    private void ShootBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            bullet.AddForce(_cameraTransform.forward * _shootForce, ForceMode.Impulse);
            Destroy(bullet.gameObject, 5.0f);
        }
    }

    private void Interact()
    {
        Ray ray = _mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out _rayCastHit, _rayCastDistance, _buttonLayer))
        {
            // Temporarily hold last ray cast
            _selectable = _rayCastHit.transform.GetComponent<ISelectable>();

            if (_selectable != null)
            {
                _selectable.OnHoverEnter();

                if (Input.GetKeyDown(KeyCode.F))
                {
                    _selectable.OnSelect();
                }
            }
        }

        if (_rayCastHit.transform == null && _selectable != null)
        {
            _selectable.OnHoverExit();
            _selectable = null;
        }
    }

    private void PickAndDrop()
    {
        Ray ray = _mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out _rayCastHit, _pickUpDistance, _pickUpLayer))
        {
            // Temporarily hold last ray cast
            _pickable = _rayCastHit.transform.GetComponent<IPickable>();

            if (Input.GetKeyDown(KeyCode.F) && !_isPicked)
            {
                _pickable.OnPicked(_attachPoint);
                _isPicked = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && _isPicked && _pickable != null)
        {
            _pickable.OnDropped();
            _isPicked = false;
        }
    }
}
