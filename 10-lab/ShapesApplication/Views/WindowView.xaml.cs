using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ShapesApplication.Controllers;
using ShapesApplication.Models;
using ShapesApplication.Views;

namespace ShapesApplication
{
    public partial class WindowView : Window
    {
        private readonly Dictionary<Guid, TabItem> _documentTabs = new Dictionary<Guid, TabItem>();
        private readonly DocumentController _documentController;
        private Guid _activeDocumentId;

        public WindowView( DocumentController documentController )
        {
            if ( documentController == null )
            {
                throw new ArgumentNullException( nameof( documentController ) );
            }

            _documentController = documentController;

            InitializeComponent();
            InitializeDocumentController();
        }

        private void InitializeDocumentController()
        {
            _documentController.DocumentCreated += OnDocumentCreated;
            _documentController.DocumentRemoved += OnDocumentRemoved;
            LoadExistingDocuments();
        }

        private void LoadExistingDocuments()
        {
            DocumentsTabControl.Items.Clear();
            _documentTabs.Clear();

            foreach ( Document document in _documentController.AllDocuments )
            {
                AddDocumentTab( document );
            }

            if ( DocumentsTabControl.Items.Count == 0 )
            {
                Guid id = _documentController.CreateDocument();
                AddDocumentTab( _documentController.GetDocumentById( id ) );
            }

            if ( DocumentsTabControl.Items.Count > 0 )
            {
                DocumentsTabControl.SelectedIndex = 0;
                UpdateActiveDocument();
            }
        }

        private void OnDocumentCreated( Document document )
        {
            Dispatcher.Invoke( () =>
            {
                if ( !_documentTabs.ContainsKey( document.Id ) )
                {
                    AddDocumentTab( document );
                    SelectDocumentTab( document );
                }
            } );
        }

        private void OnDocumentRemoved( Document document )
        {
            Dispatcher.Invoke( () =>
            {
                if ( _documentTabs.TryGetValue( document.Id, out TabItem tab ) )
                {
                    DocumentsTabControl.Items.Remove( tab );
                    _documentTabs.Remove( document.Id );

                    if ( _activeDocumentId == document.Id )
                    {
                        UpdateActiveDocument();
                    }
                }
            } );
        }

        private void AddDocumentTab( Document document )
        {
            if ( _documentTabs.ContainsKey( document.Id ) )
            {
                return;
            }

            DocumentView view = new DocumentView( document );

            StackPanel headerPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            TextBlock textBlock = new TextBlock
            {
                Text = document.Name,
                Margin = new Thickness( 0, 0, 5, 0 )
            };

            Button closeButton = new Button
            {
                Content = "×",
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                Width = 20,
                Height = 20,
                Padding = new Thickness( 0 ),
                Background = Brushes.Transparent,
                BorderThickness = new Thickness( 0 ),
                Focusable = false,
                Tag = document
            };
            closeButton.Click += CloseTabButtonOnClick;

            headerPanel.Children.Add( textBlock );
            headerPanel.Children.Add( closeButton );

            TabItem tab = new TabItem
            {
                Header = headerPanel,
                Content = view,
                Tag = document
            };

            DocumentsTabControl.Items.Add( tab );
            _documentTabs[ document.Id ] = tab;
        }

        private void CloseTabButtonOnClick( object sender, RoutedEventArgs e )
        {
            if ( sender is Button button && button.Tag is Document document )
            {
                _documentController.RemoveDocument( document.Id );
            }
        }

        private void SelectDocumentTab( Document document )
        {
            if ( _documentTabs.TryGetValue( document.Id, out TabItem tab ) )
            {
                DocumentsTabControl.SelectedItem = tab;
                UpdateActiveDocument();
            }
        }

        private void UpdateActiveDocument()
        {
            if ( DocumentsTabControl.SelectedItem is TabItem selectedTab &&
                selectedTab.Tag is Document document )
            {
                _activeDocumentId = document.Id;
            }
        }

        private void WindowOnLoad( object sender, RoutedEventArgs e )
        {
            if ( DocumentsTabControl.Items.Count == 0 )
            {
                _documentController.CreateDocument();
            }
        }

        private void NewDocumentOnClick( object sender, RoutedEventArgs e )
        {
            Guid id = _documentController.CreateDocument();
            SelectDocumentTab( _documentController.GetDocumentById( id ) );
        }

        private void NewWindowOnClick( object sender, RoutedEventArgs e )
        {
            WindowView newWindow = new WindowView( _documentController );
            newWindow.Show();
        }

        private DocumentView GetActiveView()
        {
            if ( DocumentsTabControl.SelectedItem is TabItem selectedTab )
            {
                return selectedTab.Content as DocumentView;
            }

            return null;
        }

        private void AddRectangleOnClick( object sender, RoutedEventArgs e )
        {
            GetActiveView()?.AddRectangle();
        }

        private void AddTriangleOnClick( object sender, RoutedEventArgs e )
        {
            GetActiveView()?.AddTriangle();
        }

        private void AddEllipseOnClick( object sender, RoutedEventArgs e )
        {
            GetActiveView()?.AddEllipse();
        }

        private void DeleteOnClick( object sender, RoutedEventArgs e )
        {
            GetActiveView()?.DeleteSelectedShape();
        }

        private void UndoOnClick( object sender, RoutedEventArgs e )
        {
            GetActiveView()?.Undo();
        }

        private void RedoOnClick( object sender, RoutedEventArgs e )
        {
            GetActiveView()?.Redo();
        }

        private void ExitOnClick( object sender, RoutedEventArgs e )
        {
            Close();
        }
    }
}