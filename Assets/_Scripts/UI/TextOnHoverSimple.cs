using UnityEngine;

public class TextOnHoverSimple : TextOnHoverCollider
{
    [SerializeField] private TranslatedString _info;
    protected override TranslatedString Text => _info;
}
