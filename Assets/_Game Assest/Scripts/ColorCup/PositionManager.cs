using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    private bool isFull = false;
    public bool IsFull => isFull;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Cup>(out Cup cup))
        {
            isFull = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Cup>(out Cup cup))
        {
            isFull = false;
        }
    }
    public void SetIsFull(bool value)
    {
        isFull = value;
    }
}
