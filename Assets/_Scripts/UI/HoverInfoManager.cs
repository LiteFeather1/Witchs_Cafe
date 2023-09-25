using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverInfoManager : MonoBehaviour
{
    private const string BULLET_POINT = "• ";
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform rt_hover;
    [SerializeField] private float _padding;
    [SerializeField] private TextMeshProUGUI t_title;
    [SerializeField] private TextMeshProUGUI t_body;
    public bool _active;

    private void Update()
    {
        if (!_active)
            return;

        //Calculate Position
        Vector3 newPos = Input.mousePosition;
        newPos.z = 0f;

        //Handle Padding
        float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + rt_hover.rect.width * (2f - rt_hover.pivot.x) * _canvas.scaleFactor / 2f) - _padding;
        if (rightEdgeToScreenEdgeDistance < 0f)
            newPos.x += rightEdgeToScreenEdgeDistance;

        float leftEdgeToScreenEdgeDistance = -(newPos.x - rt_hover.rect.width * rt_hover.pivot.x * _canvas.scaleFactor / 2f) + _padding;
        if (leftEdgeToScreenEdgeDistance > 0f)
            newPos.x += leftEdgeToScreenEdgeDistance;

        float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + rt_hover.rect.height * (1f - rt_hover.pivot.y) * _canvas.scaleFactor) - _padding;
        if (topEdgeToScreenEdgeDistance < 0f)
            newPos.y += topEdgeToScreenEdgeDistance;

        rt_hover.transform.position = newPos;
    }

    public void SetText(string title, string body)
    {
        t_title.text = title;
        t_body.text = body;

        rt_hover.gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rt_hover);
        _active = true;
    }

    public void SetSimpleText(string body) => SetText(string.Empty, body);

    public void SetCoffeeText(string tittle, Coffee coffee)
    {
        System.Text.StringBuilder sb = new();
        if (coffee.CoffeeBean != null)
            sb.Append(BULLET_POINT).Append(coffee.CoffeeBean.Name).AppendLine();
        if (coffee.Milk != null)
            sb.Append(BULLET_POINT).Append(coffee.Milk.Name).AppendLine();
        for (int i = 0; i < coffee.MiscIngredients.Count; i++)
            sb.Append(BULLET_POINT).Append(coffee.MiscIngredients[i].Name).AppendLine();

        SetText($"{tittle} Ingredients:", sb.ToString());
    }

    public void HideHover()
    {
        _active = false;
        rt_hover.gameObject.SetActive(false);
    }
}
