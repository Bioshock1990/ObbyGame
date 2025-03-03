using System;
using UnityEngine;
using UnityEngine.UI;

public class CoinsSync : MonoBehaviour
{
    public long _coins { get; private set; }
    [SerializeField] private Text _coinsText;

    private void Start()
    {
        // ��� ���� ������� ������������� ���������� ����� � �� 
    }

    public void CoinsBalance(long sum)
    {
        _coins += sum;
        _coinsText.text = Convert.ToString(_coins);
        //  � ��� ���� �������������
    }
}
