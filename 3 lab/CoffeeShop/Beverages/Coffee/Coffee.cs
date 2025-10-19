namespace CoffeeShop.Beverages.Coffee;

public class Coffee : IBeverage
{
    public virtual string GetDescription()
    {
        return "Coffee";
    }

    public virtual double GetCost()
    {
        return 60;
    }
}