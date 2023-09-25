public interface IIngredient : IDestroyable, IName
{
    public void AddIngredient(Coffee coffee);
}

public interface IMixable : IIngredient { }

public interface ITopping : IIngredient { }
