public class CoffeeComparisonResults
{
    public Coffee Deliver { get; private set; }
    public int Money { get; private set; }
    public float Equality { get; private set; }

    public CoffeeComparisonResults(int money, float equality, Coffee deliver)
    {
        Money = money;
        Equality = equality;
        Deliver = deliver;
    }
}