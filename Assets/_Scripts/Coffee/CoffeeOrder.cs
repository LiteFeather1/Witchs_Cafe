using UnityEngine;

[CreateAssetMenu(fileName = "New_Coofee_Order", menuName = "Coffee Order")]
public class CoffeeOrder : ScriptableObject
{
    [SerializeField] private Coffee _cofferOrder;

    public Coffee CofferOrder => _cofferOrder;
}
