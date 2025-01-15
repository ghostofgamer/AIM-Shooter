using TMPro;
using UnityEngine;

public class KillsZombieCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _killsCountText;
    [SerializeField] private ZombieReviver _zombieReviver;

    private int _killsCount;

    private void Start()
    {
        Debug.Log("старт");
        ShowScore();
    }

    public void AddKills()
    {
        _killsCount++;
        ShowScore();
    }

    private void ShowScore()
    {
        _killsCountText.text = _killsCount.ToString(); 
        Debug.Log("килл " + _killsCount);
    }

    private void Clear()
    {
        _killsCount = 0;
        ShowScore();
    }
}