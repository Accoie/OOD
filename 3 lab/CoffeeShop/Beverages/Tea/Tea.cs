namespace CoffeeShop.Beverages.Tea;

public class Tea : IBeverage
{
    private readonly string _teaTypeDescription = "Black";
    private readonly double _cost = 30;

    public Tea( TeaType teaType )
    {
        switch ( teaType )
        {
            case TeaType.Oolong:
                _teaTypeDescription = "Oolong";
                break;
            case TeaType.Willowherb:
                _teaTypeDescription = "Willowherb";
                break;
            case TeaType.Buckwheat:
                _teaTypeDescription = "Buckwheat";
                break;
            case TeaType.Black:
                _teaTypeDescription = "Black";
                break;
            default:
                break;
        }
    }

    public string GetDescription()
    {
        return _teaTypeDescription + " " + "Tea";
    }

    public double GetCost()
    {
        return _cost;
    }
}