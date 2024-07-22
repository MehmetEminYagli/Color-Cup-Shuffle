using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class MatchCounter : MonoBehaviour
{
    [SerializeField] private List<CupCompare> cupComparer;
    private int correctMatches;
    [SerializeField] private TextMeshProUGUI correctCount;
    [SerializeField] private GameObject remaingAttempsText;
    [SerializeField] private UILevelEnd uiLevelEnd;
    private int remainingAttempts = 4;
    [SerializeField] private Button checkCorrectButton;
    [SerializeField] private GameObject overlayCanvasBackPanel;

    private void Start()
    {
        StartedFunctions();
        Invoke(nameof(CountCorrectMatches), 0.1f);
    }

    public void StartedFunctions()
    {
        remainingAttempts = 5;
        correctCount.gameObject.SetActive(true);
        checkCorrectButton.gameObject.SetActive(true);
        remaingAttempsText.gameObject.SetActive(true);
        overlayCanvasBackPanel.SetActive(true);
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
            StartCoroutine(FailPlayer());
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
            StartCoroutine(WinnerCorrectMatch());
        }
        else if (remainingAttempts == 0)
        {
            StartCoroutine(FailPlayer());
        }
    }

    public IEnumerator WinnerCorrectMatch()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.LevelFinish(true);
        uiLevelEnd.Show(true);
        correctCount.gameObject.SetActive(false);
        checkCorrectButton.gameObject.SetActive(false);
        remaingAttempsText.gameObject.SetActive(false);
        overlayCanvasBackPanel.SetActive(false);
    }


    public IEnumerator FailPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.LevelFinish(false);
        uiLevelEnd.Show(false);
        correctCount.gameObject.SetActive(false);
        checkCorrectButton.gameObject.SetActive(false);
        remaingAttempsText.gameObject.SetActive(false);
        overlayCanvasBackPanel.SetActive(false);
    }
}
