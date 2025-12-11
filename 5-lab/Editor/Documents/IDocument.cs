using Editor.Documents.Items;
using Editor.Image;
using Editor.Paragraph;

namespace Editor.Documents;

public interface IDocument
{
    IParagraph InsertParagraph( string text, int? position = null );
    IImage InsertImage( string path, int width, int height, int? position = null );

    int GetItemsCount();

    DocumentItem GetItem( int index );
    void DeleteItem( int? index );

    string GetTitle();
    void SetTitle( string title );

    bool CanUndo();
    bool CanRedo();

    void Undo();
    void Redo();

    void Save( string path );
}
