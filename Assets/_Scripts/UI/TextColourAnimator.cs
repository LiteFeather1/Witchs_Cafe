using TMPro;
using UnityEngine;

public class TextColourAnimator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private float _time;
    private float _elapsedTime;

    public void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _time)
            _elapsedTime = 0f;

        _text.color = _gradient.Evaluate(_elapsedTime / _time);
    }
}

