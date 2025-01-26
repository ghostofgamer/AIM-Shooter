using UnityEngine;

public class FruitTrigger : MonoBehaviour, ITargetHandler
{
    [SerializeField] private SlicedTarget _slicedTarget;

    public void HandleHit()
    {
        Debug.Log("Попал в фрукт");
        _slicedTarget.Slice();
    }
}