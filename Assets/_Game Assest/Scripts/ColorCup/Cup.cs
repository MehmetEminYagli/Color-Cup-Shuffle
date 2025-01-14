using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField] private int objectID;
    [SerializeField] private string objectName;

    public int ObjectID()
    {
        return objectID;
    }
    public string ObjectNanem()
    {
        return objectName;
    }



}
