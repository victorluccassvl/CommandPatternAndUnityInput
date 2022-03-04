using System.Collections.Generic;
using UnityEngine;

namespace LeRatTools
{
    public abstract class Actor<T> : MonoBehaviour where T : Actor<T>
    {
        protected Director<T> director;
        [SerializeField] protected List<ICommand> commandHistory = null;

        public bool UnderControl()
        {
            return director != null;
        }

        public Director<T> GetDirector()
        {
            return director;
        }

        public bool TrySetDirector(Director<T> director)
        {
            if (director == null) return false;
            if (UnderControl() && !director.AuthorityAgainst(this.director)) return false;

            this.director = director;
            return true;
        }

        protected void Update()
        {
            ProcessUpdateCommandQueue();
            //ProcessTimedCommandQueue();
        }

        protected void FixedUpdate()
        {
            ProcessFixedUpdateCommandQueue();
        }

        // History
        protected abstract void AddHistory(ICommand command);
        protected abstract void ClearHistory();
        protected abstract ICommand GetLastCommand();

        // Command Queue
        protected abstract void ExecuteCommand(ICommand command);
        protected ICommand GetNextUpdateCommand()
        {
            if (!UnderControl()) return null;

            return director.SendNextUpdateCommand();
        }
        protected ICommand GetNextFixedUpdateCommand()
        {
            if (!UnderControl()) return null;

            return director.SendNextUpdateCommand();
        }
        protected ICommand GetNextTimedCommand()
        {
            if (!UnderControl()) return null;

            return director.SendNextUpdateCommand();
        }
        protected void ProcessUpdateCommandQueue()
        {
            ICommand command;
            while ((command = GetNextUpdateCommand()) != null)
            {
                ExecuteCommand(command);
            }
        }
        protected void ProcessFixedUpdateCommandQueue()
        {
            ICommand command;
            while ((command = GetNextFixedUpdateCommand()) != null)
            {
                ExecuteCommand(command);
            }
        }
        protected void ProcessTimedCommandQueue()
        {
            ICommand command;
            while ((command = GetNextTimedCommand()) != null)
            {
                ExecuteCommand(command);
            }
        }
    }
}