using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ShapesApplication.Models.History;

namespace ShapesApplication.Models
{
    public class Document
    {
        private readonly ObservableCollection<Shape> _shapes = new ObservableCollection<Shape>();
        private readonly IHistory _history;

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public IReadOnlyList<Shape> Shapes => _shapes;
        public event Action ShapesChanged;

        public Document() : this( new CommandHistory() )
        {
        }

        public Document( IHistory history )
        {
            Name = "Untitled Document";
            _history = history ?? throw new ArgumentNullException( nameof( history ) );
        }

        public void AddShape( Shape shape )
        {
            _history.AddAndExecuteCommand( new AddShapeCommand( _shapes, shape ) );
            OnShapesChanged();
        }

        public void RemoveShape( Shape shape )
        {
            _history.AddAndExecuteCommand( new RemoveShapeCommand( _shapes, shape ) );
            OnShapesChanged();
        }

        public void ClearSelection()
        {
            foreach ( Shape shape in _shapes )
            {
                shape.IsSelected = false;
            }
            OnShapesChanged();
        }

        public Shape SelectShapeAtPoint( Point point )
        {
            Shape selected = _shapes.Reverse().FirstOrDefault( s => s.ContainsPoint( point ) );

            ClearSelection();
            if ( selected != null )
            {
                selected.IsSelected = true;
            }

            OnShapesChanged();
            return selected;
        }

        public void MoveSelected( Vector offset )
        {
            Shape selected = _shapes.FirstOrDefault( s => s.IsSelected );
            if ( selected != null )
            {
                _history.AddAndExecuteCommand( new MoveShapeCommand( selected, offset ) );
                OnShapesChanged();
            }
        }

        public void ResizeSelected( Point anchorPoint, Point newPoint )
        {
            Shape selected = _shapes.FirstOrDefault( s => s.IsSelected );
            if ( selected != null )
            {
                _history.AddAndExecuteCommand( new ResizeShapeCommand( selected, anchorPoint, newPoint ) );
                OnShapesChanged();
            }
        }

        public void Undo()
        {
            if ( _history.CanUndo )
            {
                _history.Undo();
                OnShapesChanged();
            }
        }

        public void Redo()
        {
            if ( _history.CanRedo )
            {
                _history.Redo();
                OnShapesChanged();
            }
        }

        private void OnShapesChanged()
        {
            ShapesChanged?.Invoke();
        }
    }
}