using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Camera _camera;


    public void Shake()
    {
        _camera.transform
            .DOShakePosition(0.15f, 1f, 10, 90, false, true, ShakeRandomnessMode.Harmonic)
            .SetEase(Ease.InOutBounce)
            .SetLink(_camera.gameObject);

        _camera.transform
            .DOShakeRotation(0.15f, 1f, 10, 90, true, ShakeRandomnessMode.Harmonic)
            .SetEase(Ease.InOutBounce)
            .SetLink(_camera.gameObject);
    }
}