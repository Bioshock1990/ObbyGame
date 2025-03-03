using UnityEngine;

public class CoinGemRotate : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 100, Space.World);
    }
}
