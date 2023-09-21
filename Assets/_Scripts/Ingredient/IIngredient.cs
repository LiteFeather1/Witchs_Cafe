public interface IIngredient : IDestroyable
{
    public void AddIngredient(Coffee coffee);
}

public interface IMixable : IIngredient { }

public interface ITopping : IIngredient { }
