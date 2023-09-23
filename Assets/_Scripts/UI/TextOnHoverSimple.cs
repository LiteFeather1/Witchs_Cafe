using UnityEngine;

public class TextOnHoverSimple : TextOnHoverCollider
{
    [SerializeField] private string _info = "Grab ?";
    protected override string Text => _info;
}
