using System.Collections;
using UnityEngine;

public class GasScript : MonoBehaviour
{
    [SerializeField] private MoveAlongSpline _moveScript;
    [SerializeField] private float _playerSpeed = 10f;
    [SerializeField] private float _gasTime = 0.1f;

    private bool _isMoving = false;

    public void OnGas()
    {
        if (!_isMoving)
        {
            _isMoving = true;
            _moveScript.speed = _playerSpeed;
            StartCoroutine(GasWait());
        }
    }

    IEnumerator GasWait()
    {
        yield return new WaitForSeconds(_gasTime);
        _moveScript.speed = 0;
        _isMoving = false;
    }
}
