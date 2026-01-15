using System;
using System.Collections.Generic;
using ShapesApplication.Models;

namespace ShapesApplication.Controllers
{
    public class DocumentController
    {
        private readonly List<Document> _documents = new List<Document>();
        private int _documentCounter = 1;

        public event Action<Document> DocumentCreated;
        public event Action<Document> DocumentRemoved;
        public event Action DocumentsChanged;

        public IReadOnlyList<Document> AllDocuments => _documents.AsReadOnly();

        public DocumentController() { }

        public Guid CreateDocument( string name = null )
        {
            Document document = new Document
            {
                Name = name ?? $"Document {_documentCounter++}"
            };

            _documents.Add( document );

            DocumentCreated?.Invoke( document );
            DocumentsChanged?.Invoke();

            return document.Id;
        }

        public Document GetDocumentById( Guid id )
        {
            return _documents.Find( x => x.Id == id );
        }

        public void RemoveDocument( Guid id )
        {
            Document document = GetDocumentById( id );

            if ( !_documents.Contains( document ) )
            {
                return;
            }

            _documents.Remove( document );
            DocumentRemoved?.Invoke( document );
            DocumentsChanged?.Invoke();
        }
    }
}