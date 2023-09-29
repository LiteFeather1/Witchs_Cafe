using System.Collections;
using UnityEngine;

public class Client : ReceiveIngredient<CoffeeCup>
{
    [Header("Coffee")]
    [SerializeField] private TranslatedString _dialogue;
    [SerializeField] private CoffeeOrder _coffeeOrder;

    [Header("Patience")]
    [SerializeField] private float _patience = 120f;
    private float _maxPatience = 1f;

    [Header("Pop Animation")]
    [SerializeField] private float _timeToPop;
    [SerializeField] private AnimationCurve _popCurve;

    public System.Action<CoffeeComparisonResults, float> OnCoffeeDelivered { get; set; }

    public TranslatedString Dialogue => _dialogue;
    public Coffee Coffee => _coffeeOrder.CofferOrder;

    public float PatienceT => _patience / _maxPatience;

    private void OnMouseEnter()
    {
        if (_t != null)
        {
            switch (GameManager.Instance.Language)
            {
                case Languages.Portuguese:
                    GameManager.Instance.HoverInfoManager.SetSimpleText("Entregar Café?");
                    break;
                default:
                    GameManager.Instance.HoverInfoManager.SetSimpleText("Deliver Coffee?");
                    break;
            }
        }
    }

    private void OnMouseExit() => GameManager.Instance.HoverInfoManager.HideHover();

    private void Awake() => _maxPatience = _patience;

    private void Update() => _patience -= Time.deltaTime;

    protected override void TakeIngredient()
    {
        CoffeeComparisonResults result = new(_coffeeOrder.CofferOrder, _t.DeliverCoffee);
        float t = Mathf.Clamp(_patience / (_maxPatience * .7f), .33f, 1f);
        result.Money *= t;
        _draggable.ForceRelease();
        _t.TeleportToKitchen();
        OnCoffeeDelivered?.Invoke(result, t * 100f);
        base.TakeIngredient();
        StartCoroutine(Dissapear());
    }

    private IEnumerator Dissapear()
    {
        yield return ScaleAnim(Vector3.one, Vector3.zero);
        gameObject.SetActive(false);
    }

    private IEnumerator ScaleAnim(Vector3 from, Vector3 to)
    {
        transform.localScale = from;
        float eTime = 0f;
        while (eTime < _timeToPop)
        {
            float t = _popCurve.Evaluate(eTime / _timeToPop);
            transform.localScale = Vector3.LerpUnclamped(from, to, t);
            eTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = to;
    }

    public IEnumerator PopAnimation() => ScaleAnim(Vector3.zero, Vector3.one);

    [ContextMenu("Test")]
    private void Test() => StartCoroutine(PopAnimation());
}
