using ShapesApplication.Controllers;
using ShapesApplication.Models;

namespace ShapesApplication.Tests
{
    public class DocumentControllerTests
    {
        private readonly DocumentController _controller;

        public DocumentControllerTests()
        {
            _controller = new DocumentController();
        }

        [Fact]
        public void Constructor_WhenCalled_InitializesEmptyDocumentsList()
        {
            // Assert
            Assert.Empty( _controller.AllDocuments );
        }

        [Fact]
        public void CreateDocument_WithDefaultName_CreatesDocumentWithIncrementalName()
        {
            // Act
            var id1 = _controller.CreateDocument();
            var id2 = _controller.CreateDocument();

            // Assert
            var document1 = _controller.GetDocumentById( id1 );
            var document2 = _controller.GetDocumentById( id2 );

            Assert.Equal( "Document 1", document1.Name );
            Assert.Equal( "Document 2", document2.Name );
        }

        [Fact]
        public void CreateDocument_WithCustomName_CreatesDocumentWithSpecifiedName()
        {
            // Arrange
            var customName = "My Custom Document";

            // Act
            var id = _controller.CreateDocument( customName );

            // Assert
            var document = _controller.GetDocumentById( id );
            Assert.Equal( customName, document.Name );
        }

        [Fact]
        public void CreateDocument_WhenCalled_RaisesDocumentCreatedEvent()
        {
            // Arrange
            Guid id = _controller.CreateDocument( "Doc" );

            // Assert
            Assert.Equal( "Doc", _controller.GetDocumentById( id ).Name );
        }

        [Fact]
        public void GetDocumentById_WithExistingId_ReturnsDocument()
        {
            // Arrange
            var id = _controller.CreateDocument( "Test" );

            // Act
            var document = _controller.GetDocumentById( id );

            // Assert
            Assert.Equal( id, document.Id );
        }

        [Fact]
        public void GetDocumentById_WithNonExistingId_ReturnsNull()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act&Assert
            Assert.Throws<ArgumentException>( () => _controller.GetDocumentById( nonExistingId ) );
        }

        [Fact]
        public void RemoveDocument_WithExistingDocument_RemovesDocument()
        {
            // Arrange
            var id = _controller.CreateDocument( "To Remove" );
            var document = _controller.GetDocumentById( id );

            // Act
            _controller.RemoveDocument( id );

            // Assert
            Assert.Null( _controller.GetDocumentById( id ) );
            Assert.Empty( _controller.AllDocuments );
        }

        [Fact]
        public void RemoveDocument_WithNonExistingDocument_DoesNothing()
        {
            // Arrange
            var initialCount = _controller.AllDocuments.Count;

            // Act
            _controller.RemoveDocument( Guid.NewGuid() );

            // Assert
            Assert.Equal( initialCount, _controller.AllDocuments.Count );
        }

        [Fact]
        public void RemoveDocument_WhenCalled_RaisesDocumentRemovedEvent()
        {
            // Arrange
            var id = _controller.CreateDocument( "To Remove" );
            var document = _controller.GetDocumentById( id );
            Document removedDocument = null;
            _controller.DocumentRemoved += ( doc ) => removedDocument = doc;

            // Act
            _controller.RemoveDocument( id );

            // Assert
            Assert.NotNull( removedDocument );
            Assert.Equal( document.Id, removedDocument.Id );
        }

        [Fact]
        public void RemoveDocument_WhenCalled_RaisesDocumentsChangedEvent()
        {
            // Arrange
            var id = _controller.CreateDocument( "To Remove" );
            var document = _controller.GetDocumentById( id );
            var eventRaised = false;
            _controller.DocumentsChanged += () => eventRaised = true;

            // Act
            _controller.RemoveDocument( id );

            // Assert
            Assert.True( eventRaised );
        }

        [Fact]
        public void CloseAllDocuments_WhenCalled_ClearsAllDocuments()
        {
            // Arrange
            _controller.CreateDocument( "Doc1" );
            _controller.CreateDocument( "Doc2" );
            _controller.CreateDocument( "Doc3" );

            // Act
            _controller.CloseAllDocuments();

            // Assert
            Assert.Empty( _controller.AllDocuments );
        }

        [Fact]
        public void CloseAllDocuments_WhenCalled_RaisesDocumentsChangedEvent()
        {
            // Arrange
            _controller.CreateDocument( "Doc1" );
            var eventRaised = false;
            _controller.DocumentsChanged += () => eventRaised = true;

            // Act
            _controller.CloseAllDocuments();

            // Assert
            Assert.True( eventRaised );
        }

        [Fact]
        public void CloseAllDocuments_WhenNoDocumentsExist_DoesNotThrow()
        {
            // Act & Assert
            var exception = Record.Exception( () => _controller.CloseAllDocuments() );
            Assert.Null( exception );
        }

        [Fact]
        public void AllDocuments_ReturnsReadOnlyCollection()
        {
            // Arrange
            _controller.CreateDocument( "Doc1" );
            _controller.CreateDocument( "Doc2" );

            // Act
            var documents = _controller.AllDocuments;

            // Assert
            Assert.Equal( 2, documents.Count );
            Assert.IsAssignableFrom<System.Collections.Generic.IReadOnlyList<Document>>( documents );
        }

        [Fact]
        public void RemoveDocument_Twice_DoesNotThrow()
        {
            // Arrange
            var id = _controller.CreateDocument( "Test" );
            var document = _controller.GetDocumentById( id );

            // Act
            _controller.RemoveDocument( id );

            var exception = Record.Exception( () => _controller.RemoveDocument( id ) );

            // Assert
            Assert.Null( exception );
        }

        [Fact]
        public void CreateDocument_IncrementsCounterCorrectlyAfterMultipleOperations()
        {
            // Act
            var id1 = _controller.CreateDocument();
            var id2 = _controller.CreateDocument( "Custom" );
            var document2 = _controller.GetDocumentById( id2 );
            _controller.RemoveDocument( id2 );
            var id3 = _controller.CreateDocument();

            // Assert
            var document1 = _controller.GetDocumentById( id1 );
            var document3 = _controller.GetDocumentById( id3 );

            Assert.Equal( "Document 1", document1.Name );
            Assert.Equal( "Custom", document2.Name );
            Assert.Equal( "Document 3", document3.Name );
        }
    }
}