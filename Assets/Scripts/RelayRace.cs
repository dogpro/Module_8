using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayRace : MonoBehaviour {

    [SerializeField] private List<Transform> _runnerList;
    [SerializeField] private float _runnerSpeed;
    [SerializeField] private int _rounds;
    [SerializeField] private GameObject _stickPrefab;

    private Vector3 _target;
    private int _pointCount;
    private int _roundsCount;
    private float _passDistance;
    private GameObject _stick;

    private void RunnerLogic() {
        if (_pointCount == _runnerList.Count) {
            _target = _runnerList[0].position;
        }

        _passDistance = Vector3.Distance(_runnerList[_pointCount - 1].position, _target);

        if (_passDistance <= 0.5) {
            if (_pointCount == _runnerList.Count) {
                _pointCount = 1;
                _target = _runnerList[_pointCount].position;
                _roundsCount++;
            } else if (_pointCount == _runnerList.Count - 1) {
                _pointCount++;
            } else {
                _pointCount++;
                _target = _runnerList[_pointCount].position;
            }
            //.transform.SetParent(_runnerList[_pointCount - 1])
            Destroy(_stick, .05f);
        }

        _runnerList[_pointCount - 1].LookAt(_target);
        _runnerList[_pointCount - 1].position = Vector3.MoveTowards(_runnerList[_pointCount - 1].position, _target, Time.deltaTime * _runnerSpeed);

        if (_stick == null) {
            _stick = Instantiate(_stickPrefab, _runnerList[_pointCount - 1].GetChild(3).position, Quaternion.identity);
            _stick.transform.SetParent(_runnerList[_pointCount - 1]);
        }
    }

    void Start() {
        _pointCount = 1;
        _roundsCount = 0;
        _target = _runnerList[_pointCount].position;
    }

    void Update() {
        if (_roundsCount < _rounds) {
            
            RunnerLogic();
        }
    }
}