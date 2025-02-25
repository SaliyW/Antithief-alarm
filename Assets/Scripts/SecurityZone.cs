using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SecurityZone : MonoBehaviour
{
    private AudioSource _audioSource;
    private readonly float _maxDelta = 0.05f;
    private readonly float _maxValue = 1;
    private readonly float _minValue = 0;
    private bool _isOnTriggerEnter = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minValue;
    }

    private void Update()
    {
        SmoothlyChangeVolume();
    }

    private void OnTriggerEnter(Collider other)
    {
        _isOnTriggerEnter = true;
        _audioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        _isOnTriggerEnter = false;
    }

    private void SmoothlyChangeVolume()
    {
        float currentTarget;

        if (_isOnTriggerEnter)
        {
            currentTarget = _maxValue;
        }
        else
        {
            currentTarget = _minValue;
        }

        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, currentTarget, _maxDelta * Time.deltaTime);

        if (_audioSource.volume == _minValue)
        {
            _audioSource.Stop();
        }
    }
}
