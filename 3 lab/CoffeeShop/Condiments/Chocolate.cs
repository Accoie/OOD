using CoffeeShop.Beverages;

namespace CoffeeShop.Condiments;

public class Chocolate : CondimentDecorator
{
    private const int _maxAmount = 5;

    private readonly int _amount = 1;

    public Chocolate( int amount, IBeverage beverage ) : base( beverage )
    {
        if ( amount <= 0 )
        {
            throw new ArgumentException( "Chocolate amount must be more than 0" );
        }

        if ( amount > _maxAmount )
        {
            throw new ArgumentException( $"Chocolate amount cannot be more than {_maxAmount}" );
        }

        _amount = amount;
    }

    public override double GetCondimentCost()
    {
        return 10 * _amount;
    }

    public override string GetCondimentDescription()
    {
        return $"Chocolate x {_amount}";
    }
}
