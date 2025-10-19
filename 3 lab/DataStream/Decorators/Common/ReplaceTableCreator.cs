namespace DataStream.Decorators.Common;

public class ReplaceTableCreator
{
    private const int _tableLength = 256;

    public static Dictionary<byte, byte> CreateEncryptTable( int key )
    {
        return CreateReplaceTable( true, key );
    }

    public static Dictionary<byte, byte> CreateDecryptTable( int key )
    {
        return CreateReplaceTable( false, key );
    }

    private static Dictionary<byte, byte> CreateReplaceTable( bool encrypt, int key )
    {
        int[] numbers = Enumerable.Range( 0, _tableLength ).ToArray();

        Random rnd = new( key );

        for ( int i = 0; i < numbers.Length; i++ )
        {
            int randomIndex = rnd.Next( _tableLength );
            (numbers[ i ], numbers[ randomIndex ]) = (numbers[ randomIndex ], numbers[ i ]);
        }

        Dictionary<byte, byte> replaceTable = new();

        for ( int i = 0; i < numbers.Length; i++ )
        {
            if ( encrypt )
            {
                replaceTable.Add( ( byte )i, ( byte )numbers[ i ] );
            }
            else
            {
                replaceTable.Add( ( byte )numbers[ i ], ( byte )i );
            }
        }

        return replaceTable;
    }
}