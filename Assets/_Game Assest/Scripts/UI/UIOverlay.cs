using TMPro;
using UnityEngine;

public class UIOverlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    //[SerializeField] private GameObject noInternetPopUp;

    private void OnEnable()
    {
        DataManager.OnCurrencyUpdated += UpdateCurrencyText;
    }

    private void Start()
    {
        UpdateCurrencyText(DataManager.Currency);
        InvokeRepeating(nameof(CheckForInternetConnection),1f, 3f);
    }

    private void CheckForInternetConnection()
    {
        var isReachable = Application.internetReachability != NetworkReachability.NotReachable;
        //noInternetPopUp.SetActive(!isReachable);
    }

    private void UpdateCurrencyText(int value)
    {
        currencyText.text = value.LargeIntToString();
    }

    private void OnDisable()
    {
        DataManager.OnCurrencyUpdated -= UpdateCurrencyText;
    }

    public void RewardPlayerForLevelCompletion()
    {
        DataManager.Currency += 100;
    }

    public void RewardPlayerForAdWatching()
    {
        DataManager.Currency += 200;
    }

    [SerializeField] private TextMeshProUGUI correctCountText;
    [SerializeField] private TextMeshProUGUI remainingAttemptsText;
    [SerializeField] private GameObject overlayCanvasBackPanel;

    public void UpdateCorrectMatchesText(int correctMatches, int totalMatches)
    {
        correctCountText.text = correctMatches.ToString() + " / " + totalMatches.ToString();
    }

    public void UpdateRemainingAttemptsText(int remainingAttempts)
    {
        remainingAttemptsText.text = remainingAttempts.ToString();
    }

    public void SetUIElementsActive(bool isActive)
    {
        correctCountText.gameObject.SetActive(isActive);
        remainingAttemptsText.gameObject.SetActive(isActive);
        overlayCanvasBackPanel.SetActive(isActive);
    }

}
