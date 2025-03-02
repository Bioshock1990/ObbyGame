using UnityEngine;
using UnityEngine.Splines;

public class BootsTrap : MonoBehaviour
{
    [SerializeField] private bool _isStarted = false;
    [SerializeField] private BotMoveScript[] _bots;
    private void Awake()
    {
    }

    private void Update()
    {
        if (_isStarted)
        {
            for(int i = 0; i < _bots.Length; i++)
            {
                _bots[i]._isStarted = true;
            }
        }
    }
}
