using System;
using UnityEngine;

public class SecurityZone : MonoBehaviour
{
    public event Action<bool> OnTrigger;
    private bool _isOnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        _isOnTrigger = true;
        OnTrigger?.Invoke(_isOnTrigger);
    }

    private void OnTriggerExit(Collider other)
    {
        _isOnTrigger = false;
        OnTrigger?.Invoke(_isOnTrigger);
    }
}
