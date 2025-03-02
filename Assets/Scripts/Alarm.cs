using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private SecurityZone _securityZone;
    [SerializeField] private float _maxDelta = 0.05f;

    private readonly float _minValue = 0;
    private readonly float _maxValue = 1;
    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minValue;
    }

    private void OnEnable()
    {
        _securityZone.TriggerEnter += ActivateSound;
        _securityZone.TriggerExit += DeactivateSound;
    }

    private void OnDisable()
    {
        _securityZone.TriggerEnter -= ActivateSound;
        _securityZone.TriggerExit -= DeactivateSound;
    }

    private void ActivateSound()
    {
        _audioSource.Play();
        StopPreviousCoroutine();
        _coroutine = StartCoroutine(SmoothlyChangeVolume(_maxValue));
    }

    private void DeactivateSound()
    {
        StopPreviousCoroutine();
        _coroutine = StartCoroutine(SmoothlyChangeVolume(_minValue));
    }

    private IEnumerator SmoothlyChangeVolume(float target)
    {
        while (_audioSource.volume != target)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, _maxDelta * Time.deltaTime);

            yield return null;
        }

        if (_audioSource.volume == _minValue)
        {
            _audioSource.Stop();
        }
    }

    private void StopPreviousCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
}