using System.Collections;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Vector3 _targetRotation;
    [SerializeField] private float _rotationTime;
    private Coroutine _rotateCoroutine;

    private void OnEnable()
    {
        Rotate();
    }

    private void OnDisable()
    {
        StopCoroutine();
    }

    private void Rotate()
    {
        StopCoroutine();
        _rotateCoroutine = StartCoroutine(LerpRotation(Quaternion.Euler(_targetRotation), _rotationTime));
    }

    private void StopCoroutine()
    {
        if (_rotateCoroutine != null)
            StopCoroutine(_rotateCoroutine);
    }

    private IEnumerator LerpRotation(Quaternion endValue, float duration)
    {
        float time = 0;
        Quaternion startValue = transform.rotation;

        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = startValue;
        Rotate();
    }
}
