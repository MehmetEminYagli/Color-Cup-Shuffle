using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
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
    private bool isWin = false;

    private void Start()
    {
        isWin = false;
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
            if (correctMatches < 4)
            {
                remainingAttempts--;
                FailRotateDeskFunction();
            }
            GetCorrectMatches();
            TextCorrectMathesCount();
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
            isWin = true;
            WinRotateDeskFunction();
            StartCoroutine(WinnerCorrectMatch());
        }
        else if (remainingAttempts == 0)
        {
            StartCoroutine(FailPlayer());
        }
    }
    public bool IsWin()
    {
        return isWin;
    }

    public IEnumerator WinnerCorrectMatch()
    {
        for (int i = 0; i < cupComparer.Count; i++)
        {
            cupComparer[i].GetObject1().GetComponentInChildren<Rigidbody>().isKinematic = true;
            cupComparer[i].GetObject2().GetComponentInChildren<Rigidbody>().isKinematic = true;

        }
        yield return new WaitForSeconds(1f);
        correctCount.gameObject.SetActive(false);
        checkCorrectButton.gameObject.SetActive(false);
        remaingAttempsText.gameObject.SetActive(false);
        overlayCanvasBackPanel.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        uiLevelEnd.Show(true);
        GameManager.Instance.LevelFinish(true);
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


    [SerializeField] private GameObject desk;
    public void WinRotateDeskFunction()
    {
        desk.transform.DOLocalRotate(new Vector3(0,180, 180), 1f, RotateMode.FastBeyond360);
    }

    public void FailRotateDeskFunction()
    {

        desk.transform.DOShakeRotation(1f, new Vector3(0, 15, 0), 6, 90, false);


    }
}

