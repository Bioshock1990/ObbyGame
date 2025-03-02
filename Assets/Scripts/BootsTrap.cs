using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BootsTrap : MonoBehaviour
{
    [SerializeField] private bool _isStarted = false;
    [SerializeField] private BotMoveScript[] _bots;
    [SerializeField] private LoadingCanvas _loadingCanvas;
    [SerializeField] private Button _gasButton;
    [SerializeField] private Text _timerText;
    [SerializeField] private int _timerLength = 5;

    private bool WasIsStarted = false;
    private void Awake()
    {
        _gasButton.gameObject.SetActive(false);
        StartCoroutine(SceneLoading());
    }

    private void Update()
    {
        if (_isStarted && !WasIsStarted)
        {
            WasIsStarted = true;
            BotsStart();
        }
    }

    private void BotsStart()
    {
        for (int i = 0; i < _bots.Length; i++)
        {
            _bots[i]._maxSpeed = UnityEngine.Random.Range(7, 15);
            _bots[i]._isStarted = true;
        }
    }

    IEnumerator SceneLoading()
    {
        _loadingCanvas.Show();
        yield return new WaitForSeconds(1.5f);
        _loadingCanvas.Hide();
        for(int i = 0; i < _timerLength; i++)
        {
            _timerText.text = Convert.ToString(_timerLength - i);
            yield return new WaitForSeconds(1);
        }
        _timerText.text = "Start!";
        yield return new WaitForSeconds(0.5f);
        _timerText.text = "";
        _isStarted = true;
        _gasButton.gameObject.SetActive(true);
    }
}
