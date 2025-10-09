namespace Editor.Commands
{
    public abstract class AbstractCommand : ICommand
    {
        private bool _execute = false;

        public void Execute()
        {
            _execute = true;

            StartExecute();
        }

        public void Unexecute()
        {
            if ( _execute )
            {
                StartUnexecute();
            }
        }

        protected abstract void StartExecute();
        protected abstract void StartUnexecute();
    }
}
