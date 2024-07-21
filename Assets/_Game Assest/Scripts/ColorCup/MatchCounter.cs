using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MatchCounter : MonoBehaviour
{
    [SerializeField] private List<CupCompare> cupComparer;
    private int correctMatches;
    [SerializeField] private TextMeshProUGUI correctCount;
    [SerializeField] private GameObject remaingAttempsText;
    [SerializeField] private UILevelEnd uiLevelEnd;
    private int remainingAttempts = 4;
    [SerializeField] private Button checkCorrectButton;

    private void Start()
    {
        TextCorrectMathesCount();
        remainingAttempts = 4;
        correctCount.gameObject.SetActive(true);
        checkCorrectButton.gameObject.SetActive(true);
        remaingAttempsText.gameObject.SetActive(true);
        remaingAttempsText.GetComponentInChildren<TextMeshProUGUI>().text = remainingAttempts.ToString();
        
    }

    public void CountCorrectMatches()
    {
        if (remainingAttempts > 0)
        {
            correctMatches = 0;
            for (int i = 0; i < cupComparer.Count; i++)
            {
                cupComparer[i].CupMatches();
                if (cupComparer[i].Ismatches())
                {
                    correctMatches++;
                }
            }
            GetCorrectMatches();
            TextCorrectMathesCount();
            remainingAttempts--;
            CheckWinOrFail();
        }
        else
        {
            FailPlayer();
        }
        remaingAttempsText.GetComponentInChildren<TextMeshProUGUI>().text = remainingAttempts.ToString();
    }

    public int GetCorrectMatches()
    {
        return correctMatches;
    }

    public void TextCorrectMathesCount()
    {
        correctCount.text = GetCorrectMatches().ToString() + " / " + cupComparer.Count.ToString();
    }

    private void CheckWinOrFail()
    {
        if (correctMatches == 4)
        {
            WinnerCorrectMatch();
        }
        else if (remainingAttempts == 0)
        {
            FailPlayer();
        }
    }

    public void WinnerCorrectMatch()
    {
        GameManager.Instance.LevelFinish(true);
        uiLevelEnd.Show(true);
        correctCount.gameObject.SetActive(false);
        checkCorrectButton.gameObject.SetActive(false);
        remaingAttempsText.gameObject.SetActive(false);
    }


    public void FailPlayer()
    {
        GameManager.Instance.LevelFinish(false);
        uiLevelEnd.Show(false);
        correctCount.gameObject.SetActive(false);
        checkCorrectButton.gameObject.SetActive(false);
        remaingAttempsText.gameObject.SetActive(false);
    }
}
