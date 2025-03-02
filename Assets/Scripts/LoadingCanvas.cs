using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvas : MonoBehaviour
{
    [SerializeField] private Image _rotatingCircle;
    
    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    private void Update()
    {
       _rotatingCircle.transform.Rotate(Vector3.forward * Time.deltaTime * 100, Space.World); 
    }
}
