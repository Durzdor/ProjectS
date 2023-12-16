using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private GameObject _characterUI;
    [SerializeField]
    private GameObject _mapCamera;
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private InputController _input;

    private bool _onMap;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            if (_onMap) CloseMap();
            else OpenMap();
    }

    private void OpenMap() { _characterUI.SetActive(false); _mainCamera.gameObject.SetActive(false); _mapCamera.SetActive(true); _onMap = true; }

    private void CloseMap() { _characterUI.SetActive(true); _mainCamera.gameObject.SetActive(true); _mapCamera.SetActive(false); _onMap = false; }

}
