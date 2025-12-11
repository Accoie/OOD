using Editor.Commands;
using Editor.Documents;

namespace Editor.Parser;

public class CommandFactoryParser
{
    private IDocument _document;
    private const string _toEnd = "end";
    private const int _minImageSize = 1;
    private const int _maxImageSize = 1000;

    public CommandFactoryParser( IDocument document )
    {
        _document = document;
    }

    public InsertParagraphCommand ParseInsertParagraphCommand( string[] options )
    {
        const int optionsLength = 2;

        if ( options.Length < optionsLength )
        {
            throw new ArgumentException( $"Options length cannot be less than {optionsLength}" );
        }

        string positionStr = options[ 0 ];
        string paragraph = string.Join( " ", options.Skip( 1 ) );

        ValidateText( paragraph );

        int? position = TryParsePosition( positionStr );

        return new InsertParagraphCommand( _document, paragraph, position );
    }

    public InsertImageCommand ParseInsertImageCommand( string[] options )
    {
        const int optionsLength = 4;

        if ( options.Length < optionsLength )
        {
            throw new ArgumentException( $"Options length cannot be less than{optionsLength}" );
        }

        string positionStr = options[ 0 ];
        string widthStr = options[ 1 ];
        string heightStr = options[ 2 ];
        string path = options[ 3 ];

        if ( string.IsNullOrEmpty( path ) )
        {
            throw new ArgumentException( $"Path cannot be empty" );
        }

        (int width, int height) = TryParseImageSize( widthStr, heightStr );

        int? position = TryParsePosition( positionStr );

        return new InsertImageCommand( _document, path, width, height, position );
    }

    public DeleteItemCommand ParseDeleteItemCommand( string[] options )
    {
        if ( options.Length < 0 )
        {
            throw new ArgumentException( "Options cannot be less than 0" );
        }

        return new DeleteItemCommand( _document, TryParsePosition( options[ 0 ] ) );
    }
    public ReplaceTextCommand ParseReplaceTextCommand( string[] options )
    {
        if ( options.Length < 2 )
        {
            throw new ArgumentException( "Options cannot be less than 2" );
        }
        string positionStr = options[ 0 ];
        string text = string.Join( " ", options.Skip( 1 ) );
        int? position = TryParsePosition( positionStr );
        if ( position is null )
        {
            throw new ArgumentException( "Invalid position" );
        }

        ValidateText( text );

        return new ReplaceTextCommand( _document, text, position.Value );
    }

    public ResizeImageCommand ParseResizeImageCommand( string[] options )
    {
        if ( options.Length < 3 )
        {
            throw new ArgumentException( "Options cannot be less than 3" );
        }
        string positionStr = options[ 0 ];
        string widthStr = options[ 1 ];
        string heightStr = options[ 2 ];

        int? position = TryParsePosition( positionStr );
        if ( position is null )
        {
            throw new ArgumentException( "Invalid position" );
        }

        (int width, int height) = TryParseImageSize( widthStr, heightStr );

        return new ResizeImageCommand( _document, width, height, position.Value );
    }

    public SetTitleCommand ParseSetTitleCommand( string[] options )
    {
        if ( options.Length < 0 )
        {
            throw new ArgumentException( "Options cannot be less than 0" );
        }

        string title = options[ 0 ];

        ValidateText( title );

        return new SetTitleCommand( _document, title );
    }

    public SaveCommand ParseSaveCommand( string[] options )
    {
        if ( options.Length < 0 )
        {
            throw new ArgumentException( "Options cannot be less than 0" );
        }

        string path = options[ 0 ];

        ValidateText( path );

        return new SaveCommand( _document, path );
    }

    private void ValidateText( string text )
    {
        if ( string.IsNullOrEmpty( text ) )
        {
            throw new ArgumentException( $"Paragraph cannot be empty" );
        }
    }

    private int? TryParsePosition( string positionStr )
    {
        if ( positionStr.ToLower() == _toEnd )
        {
            return null;
        }

        if ( !int.TryParse( positionStr, out int position ) )
        {
            throw new ArgumentException( $"Position must be a number or \"{_toEnd}\"" );
        }

        if ( position < 0 )
        {
            throw new ArgumentException( "Position cannot be less than zero" );
        }

        return position;
    }

    private (int, int) TryParseImageSize( string widthStr, string heightStr )
    {
        if ( !int.TryParse( widthStr, out int width ) )
        {
            throw new ArgumentException( "Width must be a number" );
        }
        if ( !int.TryParse( heightStr, out int height ) )
        {
            throw new ArgumentException( "Height must be a number" );
        }

        if ( width < _minImageSize || width > _maxImageSize )
        {
            throw new ArgumentException( $"Width must be from {_minImageSize} to {_maxImageSize}" );
        }

        if ( height < _minImageSize || height > _maxImageSize )
        {
            throw new ArgumentException( $"Height must be from {_minImageSize} to {_maxImageSize}" );
        }

        return (width, height);
    }
}