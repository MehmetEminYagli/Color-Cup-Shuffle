using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
public class MatchCounter : MonoBehaviour
{
    [SerializeField] public List<CupCompare> cupComparers;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private UILevelEnd uiLevelEnd;
    [SerializeField] private Button checkCorrectButton;
    [SerializeField] private DeskManager deskManager;

    [SerializeField] private GameObject winParticleEffect;

    private int correctMatches;
    private int remainingAttempts;


    private void Start()
    {
        winParticleEffect.SetActive(false);
    }
    public void InitializeGame()
    {
        StartCoroutine(InitializeGameStart());
    }

    private IEnumerator InitializeGameStart()
    {
        yield return new WaitForSeconds(0.1f);
        correctMatches = 0;
        remainingAttempts = 4;

        
        uiManager.overlay.SetUIElementsActive(true);
        uiManager.overlay.UpdateRemainingAttemptsText(remainingAttempts);
        uiManager.overlay.UpdateCorrectMatchesText(correctMatches, cupComparers.Count);

        Invoke(nameof(FirstCountCorrect), 0.00001f);
    }

    public void FirstCountCorrect()
    {
        correctMatches = 0;
        foreach (var comparer in cupComparers)
        {
            if (comparer.IsMatches())
            {
                correctMatches++;
            }
        }
        uiManager.overlay.UpdateCorrectMatchesText(correctMatches, cupComparers.Count);
        CheckWinOrFail();
    }

    public void CountCorrectMatches()
    {
        if (remainingAttempts > 0)
        {
            correctMatches = 0;
            foreach (var comparer in cupComparers)
            {
                if (comparer.IsMatches())
                {
                    correctMatches++;
                }
            }
            if (correctMatches < cupComparers.Count)
            {
                remainingAttempts--;
                deskManager.FailRotateDesk();
            }
            uiManager.overlay.UpdateCorrectMatchesText(correctMatches, cupComparers.Count);
            CheckWinOrFail();
        }
        else
        {
            StartCoroutine(HandleFailure());
        }
        uiManager.overlay.UpdateRemainingAttemptsText(remainingAttempts);
    }

    private void CheckWinOrFail()
    {
        if (correctMatches == cupComparers.Count)
        {
            deskManager.WinRotateDesk();
            StartCoroutine(WinnerSequence());
        }
        else if (remainingAttempts == 0)
        {
            StartCoroutine(HandleFailure());
        }
    }

    private IEnumerator WinnerSequence()
    {
        winParticleEffect.SetActive(true);
        yield return new WaitForSeconds(.8f);
        uiManager.overlay.SetUIElementsActive(false);
        yield return new WaitForSeconds(1.5f);
        uiLevelEnd.Show(true);
        GameManager.Instance.LevelFinish(true);
    }

    private IEnumerator HandleFailure()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.LevelFinish(false);
        uiLevelEnd.Show(false);
        uiManager.overlay.SetUIElementsActive(false);
    }
    //[SerializeField] private List<CupCompare> cupComparer;
    //[SerializeField] private TextMeshProUGUI correctCountText;
    //[SerializeField] private TextMeshProUGUI remainingAttemptsText;
    //[SerializeField] private UILevelEnd uiLevelEnd;
    //[SerializeField] private Button checkCorrectButton;
    //[SerializeField] private GameObject overlayCanvasBackPanel;
    //[SerializeField] private GameObject desk;

    //private int correctMatches;
    //private int remainingAttempts;

    //private void Start()
    //{
    //    InitializeGame();
    //    Invoke(nameof(FirstCountCorrect), 0.00001f);
    //}


    //private void InitializeGame()
    //{
    //    correctMatches = 0;
    //    remainingAttempts = 4;

    //    SetUIElementsActive(true);
    //    UpdateRemainingAttemptsText();
    //    UpdateCorrectMatchesText();
    //}

    //public void FirstCountCorrect()
    //{
    //    correctMatches = 0;
    //    for (int i = 0; i < cupComparer.Count; i++)
    //    {
    //        cupComparer[i].CupMatches();
    //        if (cupComparer[i].IsMatches())
    //        {
    //            correctMatches++;
    //        }
    //    }
    //    GetCorrectMatches();
    //    UpdateCorrectMatchesText();
    //    CheckWinOrFail();
    //}


    //public void CountCorrectMatches()
    //{
    //    if (remainingAttempts > 0)
    //    {
    //        correctMatches = 0;
    //        for (int i = 0; i < cupComparer.Count; i++)
    //        {
    //            cupComparer[i].CupMatches();
    //            if (cupComparer[i].IsMatches())
    //            {
    //                correctMatches++;
    //            }
    //        }
    //        if (correctMatches < 4)
    //        {
    //            remainingAttempts--;
    //            FailRotateDesk();
    //        }
    //        GetCorrectMatches();
    //        UpdateCorrectMatchesText();
    //        CheckWinOrFail();
    //    }
    //    else
    //    {
    //        StartCoroutine(HandleFailure());
    //    }
    //    UpdateRemainingAttemptsText();
    //}

    //private void UpdateRemainingAttemptsText()
    //{
    //    remainingAttemptsText.text = remainingAttempts.ToString();
    //}

    //public int GetCorrectMatches()
    //{
    //    return correctMatches;
    //}

    //public void UpdateCorrectMatchesText()
    //{
    //    correctCountText.text = GetCorrectMatches().ToString() + " / " + cupComparer.Count.ToString();
    //}

    //private void CheckWinOrFail()
    //{
    //    if (correctMatches == cupComparer.Count)
    //    {
    //        WinRotateDesk();
    //        StartCoroutine(WinnerSequence());
    //    }
    //    else if (remainingAttempts == 0)
    //    {
    //        StartCoroutine(HandleFailure());
    //    }
    //}

    //private void SetUIElementsActive(bool isActive)
    //{
    //    correctCountText.gameObject.SetActive(isActive);
    //    checkCorrectButton.gameObject.SetActive(isActive);
    //    remainingAttemptsText.gameObject.SetActive(isActive);
    //    overlayCanvasBackPanel.SetActive(isActive);
    //}

    //private IEnumerator WinnerSequence()
    //{
    //    SetObjectsKinematic(true);

    //    yield return new WaitForSeconds(1f);
    //    SetUIElementsActive(false);
    //    yield return new WaitForSeconds(1.5f);
    //    uiLevelEnd.Show(true);
    //    GameManager.Instance.LevelFinish(true);
    //}

    //private IEnumerator HandleFailure()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    GameManager.Instance.LevelFinish(false);
    //    uiLevelEnd.Show(false);
    //    SetUIElementsActive(false);
    //}

    //private void SetObjectsKinematic(bool isKinematic)
    //{
    //    foreach (var comparer in cupComparer)
    //    {
    //        comparer.GetObject1().GetComponentInChildren<Rigidbody>().isKinematic = isKinematic;
    //        comparer.GetObject2().GetComponentInChildren<Rigidbody>().isKinematic = isKinematic;
    //    }
    //}

    //public void WinRotateDesk()
    //{
    //    desk.transform.DOLocalRotate(new Vector3(0, 180, 180), 1f, RotateMode.FastBeyond360);
    //}

    //public void FailRotateDesk()
    //{
    //    desk.transform.DOShakeRotation(1f, new Vector3(0, 15, 0), 6, 90, false);
    //}
}

