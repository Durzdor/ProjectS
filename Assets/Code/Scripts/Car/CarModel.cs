using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class CarModel : NPCModel
{
    [SerializeField] private bool _isDriving;
    [SerializeField] private bool _isAccelerating;
    [SerializeField] private bool _isRotating;
    
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField][Range(0,1)] private float _deaccelerationSpeed;
    
    [SerializeField] private Vector2 _acceleration;
    [SerializeField] private Transform _forward;
    [SerializeField] private PlayerModel _player;
    [SerializeField] private CameraController _camera;
    [SerializeField] private MiniMap _miniMapCamera;
    [SerializeField] private Car_Stats stats;
    
    [SerializeField] private List<Transform> _doors;
    [SerializeField] private LayerMask _obstacleLayer; 
    
    
    public bool IsDriving => _isDriving;
    public bool IsAccelerating => _isAccelerating;
    public bool IsRotating => _isRotating;
    public Car_Stats Stats => stats;
    private Rigidbody2D _rb;
    
    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        if (_camera == null) _camera = FindObjectOfType<CameraController>();
        if (_miniMapCamera == null) _miniMapCamera = FindObjectOfType<MiniMap>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _maxSpeed);
        _acceleration = _rb.velocity;
    }

    public void ApplyTraction(float traction)
    {
        if (!_isDriving) return;
        if (traction == 0)
        {
            _isAccelerating = false;
            return;
        }

        _isAccelerating = true;
        
        var dir = (_forward.position - transform.position).normalized;
        _rb.AddForce(dir * (_accelerationSpeed * traction));
    }

    public void ApplyRotation(float rotation)
    {
        if (!_isDriving) return;
        if (rotation == 0)
        {
            _isRotating = false;
            return;
        }

        _isRotating = true;
        
        transform.Rotate(rotation > 0 ? new Vector3(0, 0, -1) : new Vector3(0, 0, 1));
    }

    public void SetDriver(PlayerModel player)
    {
        _player = player;
    }

    public void CharacterExitCar()
    {
        _player.gameObject.SetActive(true);

        foreach (var door in _doors)
        {
            var result = Physics.OverlapSphere(door.position, 1, _obstacleLayer);
            Debug.Log(result);
            if (result.Length <= 0)
            {
                _player.transform.position = door.position;
                break;
            } 
        }
        
        _camera.SetFollowObject(_player.gameObject);
        _miniMapCamera.SetFollow(_player.transform);
        _rb.mass = 99999;
        _rb.velocity = Vector2.zero;
        _isDriving = false;
        GameManager.Instance.PlayerTransform = _player.transform;
    }

    public void CharacterEnterCar()
    {
        _player.gameObject.SetActive(false);
        _camera.SetFollowObject(gameObject);
        _miniMapCamera.SetFollow(transform);
        _rb.mass = 1;
        _isDriving = true;
        GameManager.Instance.PlayerTransform = gameObject.transform;
    }

    public bool HasKey()
    {
        return GameManager.Instance.PlayerInventory.CheckItemSO(stats.Key, 1);
    }
}