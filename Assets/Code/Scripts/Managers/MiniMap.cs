using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private Transform _follow;

    private void Awake()
    {
        if (_follow == null) _follow = FindObjectOfType<PlayerModel>().transform;
    }

    private void LateUpdate()
    {
        if (_follow == null) return;
        transform.position = new Vector3(_follow.position.x, _follow.position.y, -22);
    }

    public void SetFollow(Transform follow)
    {
        _follow = follow;
    }
}