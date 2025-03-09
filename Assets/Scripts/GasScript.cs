using System.Collections;
using UnityEngine;

public class GasScript : MonoBehaviour
{
    [SerializeField] private MoveAlongSpline _moveScript;
    [SerializeField] private AnimationCurve _speedCurve;
    [SerializeField] private float _playerSpeed = 10f;
    [SerializeField] private float _gasTime = 0.1f;
    [SerializeField, Range(0f, 1f)] private float _maxSpeedEnterTime = 0.2f;
    [SerializeField, Range(0f, 1f)] private float _maxSpeedExitTime = 0.2f;

    private bool _isMoving = false;
    private float _curGasTime = 0f;

    public void OnGas()
    {
        _isMoving = true;
        if(_curGasTime / _gasTime > _maxSpeedEnterTime && _curGasTime / _gasTime < _maxSpeedExitTime)
        {
            _curGasTime = _gasTime * _maxSpeedEnterTime;
        }
        if(_curGasTime / _gasTime > _maxSpeedExitTime)
        {
            _curGasTime = ((_curGasTime - _maxSpeedExitTime) / 1) * _maxSpeedEnterTime;
        }
    }

    private void Update()
    {
        if (_isMoving) _curGasTime += Time.deltaTime;
        else _curGasTime = 0f;
        _moveScript.speed = _speedCurve.Evaluate(_curGasTime / _gasTime) * _playerSpeed;
    }
}
