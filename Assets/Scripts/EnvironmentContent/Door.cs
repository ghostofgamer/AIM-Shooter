using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private StopGameButton _stopGameButton;
    [SerializeField] private StartGameView _startGameView;
    [SerializeField] private GameObject _door;

    private void OnEnable()
    {
        _stopGameButton.Stoping += Open;
        _startGameView.CountdownStarting += Close;
    }

    private void OnDisable()
    {
        _stopGameButton.Stoping -= Open;
        _startGameView.CountdownStarting -= Close;
    }

    private void Open()
    {
        _door.gameObject.SetActive(false);
    }

    private void Close()
    {
        _door.gameObject.SetActive(true);
    }
}