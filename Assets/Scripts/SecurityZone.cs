using System;
using UnityEngine;

public class SecurityZone : MonoBehaviour
{
    public event Action TriggerEnter;
    public event Action TriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit?.Invoke();
    }
}
