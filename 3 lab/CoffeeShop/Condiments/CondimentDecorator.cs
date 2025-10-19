using CoffeeShop.Beverages;

namespace CoffeeShop.Condiments;

public abstract class CondimentDecorator : IBeverage
{
    private IBeverage _beverage;

    public CondimentDecorator( IBeverage beverage )
    {
        _beverage = beverage;
    }

    public double GetCost()
    {
        return GetCondimentCost() + _beverage.GetCost();
    }

    public string GetDescription()
    {
        return $"{GetCondimentDescription()} {_beverage.GetDescription()}";
    }

    public abstract double GetCondimentCost();
    public abstract string GetCondimentDescription();
}