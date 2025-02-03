using Interfaces;
using UnityEngine;

namespace TargetContent
{
    public class FruitTrigger : MonoBehaviour, ITargetHandler
    {
        [SerializeField] private SlicedTarget _slicedTarget;

        public void HandleHit()
        {
            _slicedTarget.Slice();
        }
    }
}