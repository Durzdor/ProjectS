using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarView : MonoBehaviour
{
    [SerializeField] private CarModel _carModel;
    [SerializeField] private List<TrailRenderer> _trails;

    private void Update()
    {
        _trails.ForEach(trail => trail.emitting = (_carModel.IsAccelerating && _carModel.IsRotating));
    }
}