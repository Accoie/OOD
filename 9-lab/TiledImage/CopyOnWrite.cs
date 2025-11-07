using System.Diagnostics;

namespace TiledImage
{
    public class CoW<T> where T : class, new()
    {
        private T _value;

        public class WriteProxy : IDisposable
        {
            private readonly CoW<T> _parent;
            private bool _disposed = false;

            public T Value => _parent._value;

            internal WriteProxy( CoW<T> parent )
            {
                _parent = parent;
            }

            public void Dispose()
            {
                if ( !_disposed )
                {
                    _disposed = true;
                }
            }
        }

        public CoW()
        {
            _value = new T();
        }

        public CoW( Func<T> factory )
        {
            _value = factory();
        }

        public CoW( T value )
        {
            _value = value ?? throw new ArgumentNullException( nameof( value ) );
        }

        public T Value
        {
            get
            {
                return _value;
            }
        }

        public void Modify( Action<T> modifier )
        {
            if ( modifier == null )
            {
                throw new ArgumentNullException( nameof( modifier ) );
            }

            modifier( _value );
        }

        public WriteProxy GetWriteProxy()
        {
            return new WriteProxy( this );
        }
    }
}