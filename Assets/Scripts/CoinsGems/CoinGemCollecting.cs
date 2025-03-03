using UnityEngine;

public class CoinGemCollecting : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CoinsSync _coins = other.GetComponent<CoinsSync>();
        if (_coins)
        {
            _coins.CoinsBalance(1);
            Destroy(gameObject);
        }
    }
}
