using System;
using UnityEngine;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        #region Serializables

        [SerializeField] 
        private InputController inputController;
        [SerializeField] 
        private PlayerModel playerModel;
        [SerializeField]
        private WeaponPivotBehaviour _weaponController;
        [SerializeField]
        private GameObject _torchLight;

        [SerializeField]
        private GameObject _lightSaber;

        #endregion

        #region Miembros privados

        private Vector2 _lookDir;

        #endregion

        #region Eventos de Unity

        private void Awake()
        {
            GameManager.Instance.PlayerTransform = transform;
        }

        private void Start()
        {
            inputController.PrimaryFireEventStarted += Attack;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                GameManager.Instance.PopupManager.ShowMessage("So... you choose the ways of the force...");
                EquipWeapon(_lightSaber);
            }

            _lookDir = (inputController.MousePosition - (Vector2)transform.position).normalized;
            playerModel.Move(inputController.Movement);
            playerModel.LookAt(_lookDir);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (playerModel.InteractableGo != null)
                {
                    playerModel.InteractableGo.Interaction();
                }
            }
            if (Input.GetKeyDown(KeyCode.F)) _torchLight.SetActive(!_torchLight.activeInHierarchy);
        }

        #endregion

        private void Attack()
        {
            playerModel.Attack();
        }

        public void EquipWeapon(GameObject w) => _weaponController.EquipWeapon(w);
    }
}