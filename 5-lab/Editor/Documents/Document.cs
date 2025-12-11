using System.Text;
using Editor.Documents.Items;
using Editor.History;
using Editor.Html;
using Editor.Image;
using Editor.Paragraph;

namespace Editor.Documents;

public class Document : IDocument
{
    private ICommandHistoryUser _history;
    private List<DocumentItem> _items = [];

    public string Title { get; private set; }

    public Document( ICommandHistoryUser history, string title )
    {
        _history = history;
        Title = title;
    }

    public IParagraph InsertParagraph( string text, int? position = null )
    {
        if ( position is null || position > _items.Count )
        {
            position = _items.Count;
        }

        IParagraph paragraph = new ParagraphItem( text );

        _items.Insert( position.Value, new DocumentItem( paragraph ) );

        return paragraph;
    }

    public IImage InsertImage( string path, int width, int height, int? position = null )
    {
        if ( position is null || position > _items.Count )
        {
            position = _items.Count;
        }

        IImage image = new ImageItem( path, width, height );

        _items.Insert( position.Value, new DocumentItem( image ) );

        return image;
    }

    public int GetItemsCount()
    {
        return _items.Count;
    }

    public DocumentItem GetItem( int index )
    {
        return _items[ index ];
    }

    public void DeleteItem( int? index = null )
    {
        _items.RemoveAt( index is null ? GetItemsCount() - 1 : index.Value );
    }

    public string GetTitle()
    {
        return Title;
    }

    public void SetTitle( string title )
    {
        Title = title;
    }

    public bool CanUndo()
    {
        return _history.CanUndo();
    }

    public bool CanRedo()
    {
        return _history.CanRedo();
    }

    public void Undo()
    {
        _history.Undo();
    }

    public void Redo()
    {
        _history.Redo();
    }

    public void Save( string path )
    {
        string? directory = Path.GetDirectoryName( path );
        if ( directory is null )
        {
            throw new ArgumentException( $"Invalid path: {path}" );
        }

        string imagesDirectory = Path.Combine( directory, "images" );

        if ( !Directory.Exists( imagesDirectory ) )
        {
            Directory.CreateDirectory( imagesDirectory );
        }

        HtmlLayout layout = new( Title );

        foreach ( DocumentItem item in _items )
        {
            AddDocumentItem( layout, item, imagesDirectory );
        }

        File.WriteAllText( path, layout.CreateLayout(), Encoding.UTF8 );
    }

    private static void AddDocumentItem( HtmlLayout layout, DocumentItem item, string imagesDirectory )
    {
        if ( item.Paragraph is not null )
        {
            layout.AddParagraph( item.Paragraph.Text );
        }
        else if ( item.Image is not null )
        {
            IImage image = item.Image;

            string newImagePath = CopyImageToDirectory( image.Path, imagesDirectory );

            layout.AddImage( newImagePath, image.Width, image.Height );
        }
    }

    private static string CopyImageToDirectory( string originalImagePath, string targetDirectory )
    {
        if ( !File.Exists( originalImagePath ) )
        {
            throw new FileNotFoundException( $"Picture is not found: {originalImagePath}" );
        }

        string extension = Path.GetExtension( originalImagePath );

        string fileName = Guid.NewGuid().ToString() + extension;
        string newImagePath = Path.Combine( targetDirectory, fileName );

        File.Copy( originalImagePath, newImagePath, true );

        return Path.Combine( "images", fileName );
    }
}
