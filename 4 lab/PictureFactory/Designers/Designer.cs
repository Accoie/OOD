using System.Text;
using PictureFactory.Factories;
using PictureFactory.PictureDrafts;

namespace PictureFactory.Designers
{
    public class Designer : IDesigner
    {
        private string _exitStr = "exit";
        private IShapeFactory _shapeFactory;

        public Designer( IShapeFactory shapeFactory )
        {
            _shapeFactory = shapeFactory;
        }

        public IPictureDraft CreateDraft( Stream stream )
        {
            IPictureDraft draft = new PictureDraft();

            using StreamReader reader = new( stream, Encoding.UTF8, leaveOpen: true );

            string line = "init";
            while ( line != _exitStr )
            {
                Console.WriteLine( $"Please, enter a command or \"{_exitStr}\"" );

                line = reader.ReadLine() ?? "";

                if ( line == _exitStr )
                {
                    return draft;
                }

                if ( string.IsNullOrEmpty( line ) )
                {
                    Console.WriteLine( $"Please, write command or \"{_exitStr}\"" );

                    continue;
                }

                try
                {
                    draft.AddShape( _shapeFactory.CreateShape( line ) );
                }
                catch ( Exception ex )
                {
                    Console.WriteLine( $"{ex.Message}\n" );
                }
            }

            return draft;
        }
    }
}
