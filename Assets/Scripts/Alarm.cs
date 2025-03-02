using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private SecurityZone _securityZone;
    [SerializeField] private float _maxDelta = 0.05f;

    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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
        float maxValue = 1;

        _audioSource.Play();
        StopPreviousCoroutine();
        _coroutine = StartCoroutine(SmoothlyChangeVolume(maxValue));
    }

    private void DeactivateSound()
    {
        float minValue = 0;

        StopPreviousCoroutine();
        _coroutine = StartCoroutine(SmoothlyChangeVolume(minValue));

        if (_audioSource.volume == minValue)
        {
            _audioSource.Stop();
        }
    }

    private IEnumerator SmoothlyChangeVolume(float target)
    {
        while (_audioSource.volume != target)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, _maxDelta * Time.deltaTime);

            yield return null;
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