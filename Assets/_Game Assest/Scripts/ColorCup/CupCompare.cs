using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCompare : MonoBehaviour
{
    //[SerializeField] private MatchCounter matchCounter;
    [SerializeField] private List<GameObject> cupPairs;
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    public bool isMatches;

  public void InitializeCounting()
    {
        StartCoroutine(InitializeComparison());
    }

    public IEnumerator InitializeComparison()
    {
        yield return new WaitForSeconds(0.1f);
        CompareObjects();
        CupMatches();
    }

    public void CompareObjects()
    {
        cupPairs.Clear(); 

        if (object1 != null && object2 != null)
        {
            cupPairs.Add(object1.GetComponentInChildren<Cup>().gameObject);
            cupPairs.Add(object2.GetComponentInChildren<Cup>().gameObject);
        }
    }
    public void CupMatches()
    {
        if (cupPairs.Count < 2)
        {
            isMatches = false; // Eşleşme için en az iki öğe olmalı
            return;
        }

        if (cupPairs[0].GetComponent<Cup>().ObjectID() == cupPairs[1].GetComponent<Cup>().ObjectID())
        {
            isMatches = true;
        }
        else
        {
            isMatches = false;
        }

    }
    public void UpdateCupPairs()
    {
        // cupPairs listesindeki ikinci elemanı güncelle
        if (cupPairs.Count >= 2)
        {
            cupPairs.RemoveAt(1); // İkinci elemanı sil
        }
        cupPairs.Add(object2.GetComponentInChildren<Cup>().gameObject); // Yeni nesneyi ekle

        // Karşılaştırmayı yeniden yap
        CupMatches();
    }
    public bool IsMatches()
    {
        return isMatches;
    }
    public void BtnCupMatchesControl()
    {
        CupMatches();
    }
}
