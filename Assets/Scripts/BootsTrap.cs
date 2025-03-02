using UnityEngine;
using UnityEngine.Splines;

public class BootsTrap : MonoBehaviour
{
    [SerializeField] private SplineAnimate _sp;
    private void Awake()
    {
        _sp.Play();
        Debug.Log(_sp.IsPlaying);
        initRoads();

        
    }

    private void initRoads()
    {

    }
}
