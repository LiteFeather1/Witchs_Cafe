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

    [Header("Time")]
    [SerializeField] private TextMeshProUGUI t_time;

    [Header("Client Patience")]
    [SerializeField] private RectTransform _patienceRoot;
    private Vector2 _downPosPatience;
    private Vector2 _upPosPatience;
    [SerializeField] private Image i_patienceFill;
    [SerializeField] private Gradient g_patienceGradient;
    [SerializeField] private float _timeToAppearPatience = .5f;
    [SerializeField] private AnimationCurve _curveAppearPatience;
    private Client _currentClient;

    [Header("Money")]
    [SerializeField] private TextMeshProUGUI t_totalMoney;
    [SerializeField] private float _moneyStepTime = 1.5f;

    [Header("Mute Sprites")]
    [SerializeField] private Image i_mute;
    [SerializeField] private Sprite _mutedSprite;
    [SerializeField] private Sprite _unMutedSprite;

    private void Awake()
    {
        _gameGroup.alpha = 0f;

        _downPosPatience = _patienceRoot.localPosition;
        _patienceRoot.localPosition += new Vector3(0f, _patienceRoot.sizeDelta.y * 2f);
        _upPosPatience = _patienceRoot.localPosition;
    }

    private void Update()
    {
        if (_currentClient != null)
            SetPatience(_currentClient.PatienceT);
    }

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
        t_moneyGained.text = $"+++${results.Money:0.##}";

        // Calculate grade
        float percentile = (results.Equality + clientPatience) * .5f;
        char grade;
        if (percentile >= 90f)
            grade = 'A';
        else if (percentile >= 80f)
            grade = 'B';
        else if (percentile >= 70f)
            grade = 'C';
        else if (percentile >= 60f)
            grade = 'D';
        else
            grade = 'F';

        t_grade.text = grade.ToString();
        _deliverFeedbackRoot.gameObject.SetActive(true);
    }

    public void SetTime(float time)
    {
        int hour = Mathf.FloorToInt(time % 12);
        int minutes = Mathf.RoundToInt((time * 60f) % 60);
        t_time.text = $"{hour:00}:{minutes:00}{(hour >= 6 ? "PM" : "AM")}";
    }


    private IEnumerator Move(Transform trasnform, Vector2 from, Vector2 to, float time, AnimationCurve curve)
    {
        trasnform.localPosition = from;
        float eTime = 0f;
        while (eTime < time) 
        {
            float t = curve.Evaluate(eTime/ time);
            trasnform.localPosition = Vector2.LerpUnclamped(from, to, t);
            eTime += Time.fixedDeltaTime;
            yield return null;
        }
        trasnform.localPosition = to;
    }

    public void SetCurrentClient(Client currentClient)
    {
        _currentClient = currentClient;
        StartCoroutine(Move(_patienceRoot, _upPosPatience, _downPosPatience, _timeToAppearPatience, _curveAppearPatience));
    }

    public void ClientServed()
    {
        _currentClient = null;
        StartCoroutine(Move(_patienceRoot, _downPosPatience, _upPosPatience, _timeToAppearPatience, _curveAppearPatience));
    }

    private void SetPatience(float t)
    {
        i_patienceFill.fillAmount = t;
        i_patienceFill.color = g_patienceGradient.Evaluate(t);
    }

    public IEnumerator StepMoney(float currentMoney, float moneyAdded)
    {
        float eTime = 0f;
        while (eTime < _moneyStepTime)
        {
            eTime += Time.deltaTime;
            float m = Mathf.Lerp(currentMoney, currentMoney + moneyAdded, eTime / _moneyStepTime);
            t_totalMoney.text = m.ToString("00.00");
            yield return null;
        }
        t_totalMoney.text = (currentMoney + moneyAdded).ToString("00.00");
    }

    public void SwapMuteSprite()
    {
        i_mute.sprite = i_mute.sprite == _mutedSprite ? _unMutedSprite : _mutedSprite;
    }

    [ContextMenu("Appear")]
    public void Appear() => StartCoroutine(Move(_patienceRoot, _upPosPatience, _downPosPatience, _timeToAppearPatience, _curveAppearPatience));
    [ContextMenu("Disappear")]
    public void DisAppear() => StartCoroutine(Move(_patienceRoot, _downPosPatience, _upPosPatience, _timeToAppearPatience, _curveAppearPatience));
}

