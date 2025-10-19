namespace CoffeeShop.Beverages.Milkshake;

public class Milkshake : IBeverage
{
    public readonly string _portionDescription = "Standart";
    public readonly double _cost = 60;

    public Milkshake( MilkshakePortion portion )
    {
        switch ( portion )
        {
            case MilkshakePortion.Small:
                {
                    _portionDescription = "Small";
                    _cost = 50;

                    break;
                }
            case MilkshakePortion.Standart:
                {
                    _portionDescription = "Standart";
                    _cost = 60;

                    break;
                }
            case MilkshakePortion.Big:
                {
                    _portionDescription = "Big";
                    _cost = 80;

                    break;
                }
            default:
                break;
        }
    }

    public double GetCost()
    {
        return _cost;
    }

    public string GetDescription()
    {
        return _portionDescription + " " + "Milkshake";
    }
}