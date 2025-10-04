using CoffeeShop.Beverages;

namespace CoffeeShop.Condiments;

public class Liquor : CondimentDecorator
{
    public string _liquorTypeDescription = "Chocolate";

    public Liquor( IBeverage beverage, LiquorType liquorType ) : base( beverage )
    {
        switch ( liquorType )
        {
            case LiquorType.Walnut:
                {
                    _liquorTypeDescription = "Walnut";
                    break;
                }
            case LiquorType.Chocolate:
                {
                    _liquorTypeDescription = "Chocolate";
                    break;
                }
            default:
                break;
        }
    }

    public override double GetCondimentCost()
    {
        return 50;
    }

    public override string GetCondimentDescription()
    {
        return _liquorTypeDescription + " " + "Liquor";
    }
}