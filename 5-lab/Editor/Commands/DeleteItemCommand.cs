using Editor.Documents;
using Editor.Documents.Items;
using Editor.Image;
using Editor.Paragraph;

namespace Editor.Commands;

public class DeleteItemCommand : AbstractCommand
{
    private readonly int _position;
    private DocumentItem _previousItem;

    public DeleteItemCommand( IDocument document, int? position ) : base( document )
    {
        if ( _document.GetItemsCount() == 0 )
        {
            throw new ArgumentOutOfRangeException( "There is no elements in document!" );
        }

        if ( _document.GetItemsCount() <= position )
        {
            throw new ArgumentOutOfRangeException( "Incorrect item position" );
        }

        _position = position is null ? _document.GetItemsCount() - 1 : position.Value;

        _previousItem = _document.GetItem( _position );
    }

    protected override void DoExecute()
    {
        _document.DeleteItem( _position );
    }

    protected override void DoUnexecute()
    {
        if ( _previousItem?.Image is not null )
        {
            IImage image = _previousItem.Image;
            _document.InsertImage( image.Path, image.Width, image.Height, _position );
        }

        if ( _previousItem?.Paragraph is not null )
        {
            IParagraph paragraph = _previousItem.Paragraph;
            _document.InsertParagraph( paragraph.Text, _position );
        }
    }
}