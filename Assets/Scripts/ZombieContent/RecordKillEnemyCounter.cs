using StatisticContent;
using TMPro;
using UnityEngine;

public class RecordKillEnemyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _recordText;
    [SerializeField] private Timer _timer;

    private int _record;

    /*
    private void Start()
    {
        Show();
    }

    private void CheckRecord()
    {
        _record = PlayerPrefs.GetInt("Record",0);
        
        
    }
    
    public void Show()
    {
        _recordText.text = _record.ToString();
    }*/
}