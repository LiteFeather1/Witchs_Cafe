public interface IIngredient : IDestroyable
{
    public string Name { get; }
    public void AddIngredient(Coffee coffee);
}

public interface IMixable : IIngredient { }

public interface ITopping : IIngredient { }
