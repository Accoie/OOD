using CoffeeShop.Beverages;
using CoffeeShop.Beverages.Coffee;
using CoffeeShop.Beverages.Milkshake;
using CoffeeShop.Beverages.Tea;
using CoffeeShop.Condiments;

public class Program
{
    private static void Main()
    {
        Cappuccino cappuccino = new Cappuccino( CoffeePortion.Double );
        Console.WriteLine( cappuccino.GetDescription() );
        Console.WriteLine( cappuccino.GetCost() );

        Latte latte = new Latte( CoffeePortion.Double );
        Console.WriteLine( latte.GetDescription() );
        Console.WriteLine( latte.GetCost() );

        Milkshake milkshake = new Milkshake( MilkshakePortion.Big );
        Console.WriteLine( milkshake.GetDescription() );
        Console.WriteLine( milkshake.GetCost() );

        Tea tea = new Tea( TeaType.Oolong );
        Console.WriteLine( tea.GetDescription() );
        Console.WriteLine( tea.GetCost() );

        IBeverage superCappuccino = new Liquor( new Cream( new Chocolate( 5, cappuccino ) ), LiquorType.Walnut );
        Console.WriteLine( superCappuccino.GetDescription() );
        Console.WriteLine( superCappuccino.GetCost() );
    }
}