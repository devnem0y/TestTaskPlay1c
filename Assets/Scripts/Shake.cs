using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _magnitude;
    [SerializeField] private float _force;
    
    private bool _isShake;

    public void ShakeStart()
    {
        _isShake = true;
        StartCoroutine(Begin());
    }
    
    private IEnumerator Begin()
    {
        if (!_isShake) yield break;

        var originalPos = transform.localPosition;
        var originalRot = transform.localRotation;

        var elapsed = 0f;

        while (elapsed < _duration)
        {
            var x = Random.Range(_force * -1, _force) * _magnitude;
            var y = Random.Range(_force * -1, _force) * _magnitude;

            var pos = transform.localPosition;
            
            transform.localPosition = new Vector3(pos.x + x, pos.y + y, originalPos.z);
            transform.localRotation = new Quaternion(originalRot.x, originalRot.y, transform.localRotation.z, originalRot.w);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localRotation = originalRot;
        transform.localPosition = originalPos;
        _isShake = false;
    }
}