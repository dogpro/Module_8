using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionVector : MonoBehaviour {
    [SerializeField] private List<Vector3> _travelPoints;

    [SerializeField] private float _speedMovement;
    [SerializeField] private float _speedRotate;
    [SerializeField] private bool _isMovement;
    [SerializeField] private bool _isRotate;
    private bool _forward;

    private Vector3 _target;
    private Vector3 _rotateVetor;
    private int _pointCount;

    private void PointByPointMovenet() {
        transform.LookAt(_target);

        if (_isRotate) {
            transform.Rotate(_rotateVetor, _speedRotate);
        }

        if (_isMovement) {
            transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * _speedMovement);
        }

        if (transform.position == _target) {
            if (_target == _travelPoints[_travelPoints.Count - 1] && _forward) {
                _forward = false;
                _pointCount = _travelPoints.Count - 1;
                _target = _travelPoints[_pointCount];
            } else if (_target == _travelPoints[_pointCount] && _forward) {
                _pointCount++;
                _target = _travelPoints[_pointCount];
            }

            if (_target == _travelPoints[0] && !_forward) {
                _pointCount = 0;
                _target = _travelPoints[_pointCount];
                _forward = true;
            } else if (_target == _travelPoints[_pointCount] && !_forward) {
                _pointCount--;
                _target = _travelPoints[_pointCount];
            }
        }
    }

    private void Start() {
        _pointCount = 0;
        _forward = true;
        _rotateVetor = new Vector3(0, 0, 1);
        _isMovement = true;
        _isRotate = true;

        _target = _travelPoints[_pointCount];
    }

    private void Update() {
        PointByPointMovenet();
    }
}
