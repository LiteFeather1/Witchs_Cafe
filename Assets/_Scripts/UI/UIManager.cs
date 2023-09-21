using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Start UI")]
    [SerializeField] private CanvasGroup _startGroup;
    [SerializeField] private TextColourAnimator _clickToStartColourAnim;
    [SerializeField] private StartClickAnimator _clickToStartMovimentAnim;
    [SerializeField] private float _fadeTimeStartGroup;

    [Header("Game UI")]
    [SerializeField] private CanvasGroup _gameGroup;
    [SerializeField] private float _fadeTimeGameGroup;

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
        _clickToStartColourAnim.enabled = false;
        _clickToStartMovimentAnim.enabled = false;
    }
}

