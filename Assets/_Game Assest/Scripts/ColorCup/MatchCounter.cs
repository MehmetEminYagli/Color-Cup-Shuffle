using System.Collections.Generic;
using UnityEngine;

public class MatchCounter : MonoBehaviour
{
    [SerializeField] private List<CupCompare> cupComparer;
    [SerializeField] private int correctMatches;

    public void CountCorrectMatches()
    {
        correctMatches = 0;
        for (int i = 0; i < cupComparer.Count; i++)
        {
            cupComparer[i].CupMatches();
            if (cupComparer[i].Ismatches())
            {
                correctMatches++;
            }
            else
            {
            }
        }

        GetCorrectMatches();
    }

    public int GetCorrectMatches()
    {
        Debug.Log(correctMatches);
        return correctMatches;
    }
}
