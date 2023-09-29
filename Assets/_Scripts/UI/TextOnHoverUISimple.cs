using UnityEngine;

public class TextOnHoverUISimple : TextOnHoverUI
{
    [SerializeField] private TranslatedString _info;

    protected override TranslatedString Text => _info;
}
