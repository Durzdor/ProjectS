using System;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [SerializeField] private InputController _inputController;
    [SerializeField] private float _knockForce = 5f;
    [SerializeField] private float _damage = 5f;
    
    private CharacterControl _input;
    private CarModel _carModel;
    private bool _playerOnProximity;
    

    private void Start()
    {
        _inputController.InteractEventStarted += InteractCar;
        _carModel = GetComponent<CarModel>();
    }

    private void Update()
    {
        var traction = _inputController.Movement.y;
        var rotation = _inputController.Movement.x;
        
        _carModel.ApplyTraction(traction);
        _carModel.ApplyRotation(rotation);
    }

    private void InteractCar()
    {

        Debug.Log($"Have car key: {_carModel.HasKey()}");
        
        if (!_carModel.HasKey())
        {
            GameManager.Instance.PopupManager.ShowMessage(
                $"You need a {_carModel.Stats.Key.Identifier} to drive this");
            return;
        }
        
        Debug.Log($"_playerOnProximity: {_playerOnProximity} and _carModel.IsDriving: {_carModel.IsDriving}");
        
        if(_carModel.IsDriving)
            _carModel.CharacterExitCar();
        
        if (_playerOnProximity && !_carModel.IsDriving)
            _carModel.CharacterEnterCar();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<PlayerModel>();

        if (player == null) return;
        _carModel.SetDriver(player);

        _playerOnProximity = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<PlayerModel>();

        if (player == null) return;

        _playerOnProximity = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<PlayerModel>();

        if (player != null) return;

        if (!_carModel.IsDriving) return;
        
        var knock = other.gameObject.GetComponent<IKnockable>();
        if (knock != null) knock.Knock(_knockForce, (other.transform.position - transform.position).normalized);
        
        var damage = other.gameObject.GetComponent<IDamageable>();
        if (damage != null) damage.Damage(_damage);
    }
}