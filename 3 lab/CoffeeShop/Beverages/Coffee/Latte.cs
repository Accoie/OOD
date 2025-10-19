namespace CoffeeShop.Beverages.Coffee;

public class Latte : Coffee
{
    private readonly string _portionDescription = "Standart";
    private readonly double _cost;

    public Latte( CoffeePortion portion )
    {
        switch ( portion )
        {
            case CoffeePortion.Standart:
                {
                    _portionDescription = "Standart";
                    _cost = 90;
                    break;
                }
            case CoffeePortion.Double:
                {
                    _portionDescription = "Double";
                    _cost = 130;
                    break;
                }
            default:
                break;
        }
    }

    public override string GetDescription()
    {
        return _portionDescription + " " + "Latte";
    }

    public override double GetCost()
    {
        return _cost;
    }
}