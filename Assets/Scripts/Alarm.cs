using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private SecurityZone _securityZone;
    [SerializeField] private float _maxDelta = 0.05f;

    private readonly float _maxValue = 1;
    private readonly float _minValue = 0;
    private float _target;
    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minValue;
    }

    private void OnEnable()
    {
        _securityZone.OnTrigger += SetTarget;
    }

    private void OnDisable()
    {
        _securityZone.OnTrigger -= SetTarget;
    }

    private void SetTarget(bool isOnTrigger)
    {
        if (isOnTrigger)
        {
            _target = _maxValue;
            _audioSource.Play();
        }
        else
        {
            _target = _minValue;
        }

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(SmoothlyChangeVolume());
    }

    private IEnumerator SmoothlyChangeVolume()
    {
        while (_audioSource.volume != _target)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _target, _maxDelta * Time.deltaTime);

            yield return null;
        }

        if (_audioSource.volume == _minValue)
        {
            _audioSource.Stop();
        }
    }
}