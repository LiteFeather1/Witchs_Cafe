using UnityEngine;

public class TextOnHoverSimple : TextOnHover
{
    [SerializeField] private string _info = "Grab ?";
    protected override string Text => _info;
}
