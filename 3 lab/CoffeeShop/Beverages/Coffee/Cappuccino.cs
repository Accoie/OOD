namespace CoffeeShop.Beverages.Coffee;

public class Cappuccino : Coffee
{
    private readonly string _portionDescription = "Standart";
    private readonly double _cost = 80;

    public Cappuccino( CoffeePortion portion )
    {
        switch ( portion )
        {
            case CoffeePortion.Standart:
                {
                    _portionDescription = "Standart";
                    _cost = 80;
                    break;
                }
            case CoffeePortion.Double:
                {
                    _portionDescription = "Double";
                    _cost = 120;
                    break;
                }
            default:
                break;
        }
    }

    public override string GetDescription()
    {
        return _portionDescription + " " + "Cappuccino";
    }

    public override double GetCost()
    {
        return _cost;
    }
}