using EnemyContent;
using UnityEngine;

public class ZombieMode : MonoBehaviour
{
    [SerializeField] private ZombieMovement[] _zombieMovements;

    private void Start()
    {
        SimpleMode();
    }

    public void ChangeMode(int index)
    {
        switch (index)
        {
            case 0:
                SimpleMode();
                break;

            case 1:
                MediumMode();
                break;

            case 2:
                HardMode();
                break;
        }
    }

    private void SimpleMode()
    {
        SetValueMovementZombie(false);
        SetValueSpeedEnemy(1.65f,3f);
    }

    private void MediumMode()
    {
        SetValueMovementZombie(true);
        SetValueSpeedEnemy(1.65f,3f);
    }

    private void HardMode()
    {
        SetValueMovementZombie(true);
        SetValueSpeedEnemy(3f,1.65f);
    }

    private void SetValueMovementZombie(bool value)
    {
        foreach (var zombieMovement in _zombieMovements)
            zombieMovement.SetLoopPositionValue(!value);
    }

    private void SetValueSpeedEnemy(float value,float timeWaitValue)
    {
        foreach (var zombieMovement in _zombieMovements)
            zombieMovement.SetValue(value,timeWaitValue);
    }
}