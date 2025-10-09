using Editor.Document.Items;
using Editor.History;
using Editor.Html;
using Editor.Image;
using Editor.Paragraph;

namespace Editor.Document
{
    public class Document : IDocument
    {
        private ICommandHistory _history;
        private List<DocumentItem> _items = [];

        public string Title { get; private set; }

        public Document( ICommandHistory history, string title )
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

        public void DeleteItem( int index )
        {
            _items.RemoveAt( index );
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
            HtmlLayout layout = new( Title );

            foreach ( DocumentItem item in _items )
            {
                AddDocumentItem( layout, item );
            }
        }

        private static void AddDocumentItem( HtmlLayout layout, DocumentItem item )
        {
            if ( item.Paragraph is not null )
            {
                layout.AddParagraph( item.Paragraph.Text );
            }
            else if ( item.Image is not null )
            {
                IImage image = item.Image;
                layout.AddImage( image.Path, image.Width, image.Height );
            }
        }
    }
}
