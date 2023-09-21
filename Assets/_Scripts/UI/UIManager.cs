using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Start UI")]
    [SerializeField] private CanvasGroup _startGroup;
    [SerializeField] private float _fadeTimeStartGroup;

    [Header("Game UI")]
    [SerializeField] private CanvasGroup _gameGroup;
    [SerializeField] private float _fadeTimeGameGroup;

    [Header("Client Dialogue")]
    [SerializeField] private GameObject _clientDialogueRoot;
    [SerializeField] private TextMeshProUGUI t_clientDialogue;

    [Header("Client Dialogue")]
    [SerializeField] private Transform _deliverFeedbackRoot;
    [SerializeField] private TextMeshProUGUI t_clientPatience;
    [SerializeField] private TextMeshProUGUI t_orderEquality;
    [SerializeField] private TextMeshProUGUI t_moneyGained;
    [SerializeField] private TextMeshProUGUI t_grade;
    [SerializeField] private Button b_NextClient;

    private IEnumerator FadeCanvasGroup(CanvasGroup group, float toAlpha, float time)
    {
        float eTime = 0f;
        float startAlpha = group.alpha;
        while (eTime < time)
        {
            eTime += Time.deltaTime;
            float t = eTime / time;
            group.alpha = Mathf.Lerp(startAlpha, toAlpha, t);
            yield return null;
        }
    }

    public IEnumerator IntroFade()
    {
        yield return FadeCanvasGroup(_startGroup, 0f, _fadeTimeStartGroup);
        yield return FadeCanvasGroup(_gameGroup, 1f, _fadeTimeGameGroup);
        yield return new WaitForSeconds(.5f);
        _startGroup.gameObject.SetActive(false);
    }

    public void PopUpDialogue(string clientDialogue)
    {
        t_clientDialogue.text = clientDialogue;
        _clientDialogueRoot.SetActive(true);
    }

    public void HideDialogue() => _clientDialogueRoot.SetActive(false);

    public void CoffeeDelivered(CoffeeComparisonResults results, float clientPatience)
    {
        _clientDialogueRoot.SetActive(false);
        t_clientPatience.text = $"Client: {clientPatience:00}%";
        t_orderEquality.text = $"Order: {results.Equality:00}%";
        t_moneyGained.text = $"+++{results.Money:0.##}$";

        // Calculate grade
        float percentile = (results.Equality + clientPatience) * .5f;
        print(percentile);
        char grade = 'F';
        if (percentile >= 90f)
            grade = 'A';
        else if (percentile >= 80f)
            grade = 'B';
        else if (percentile >= 70f)
            grade = 'C';
        else if (percentile >= 60f)
            grade = 'D';

        t_grade.text = grade.ToString();
        _deliverFeedbackRoot.gameObject.SetActive(true);
    }
}

