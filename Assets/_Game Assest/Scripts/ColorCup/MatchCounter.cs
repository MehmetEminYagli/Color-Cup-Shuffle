using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchCounter : MonoBehaviour
{
    [SerializeField] private List<CupCompare> cupComparer;
    private int correctMatches;
    [SerializeField] private TextMeshProUGUI correctCount;

    private void Start()
    {
        TextCorrectMathesCount();
    }
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
        TextCorrectMathesCount();
    }

    public int GetCorrectMatches()
    {
        
        return correctMatches;
    }

    public void TextCorrectMathesCount()
    {
        correctCount.text = GetCorrectMatches().ToString() + " / " + cupComparer.Count.ToString();
    }
}
