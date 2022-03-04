using System.Collections.Generic;
using UnityEngine;

namespace LeRatTools
{
    public abstract class Director<A> : MonoBehaviour where A : Actor<A>
    {
        [SerializeField] protected uint authorityLevel;
        protected Queue<ICommand> updateCommandQueue;
        protected Queue<ICommand> fixedUpdateCommandQueue;
        protected Queue<ICommand> timedCommandQueue;

        protected A currentActor;

        public A GetActor()
        {
            return currentActor;
        }

        public bool SetActor(A actor)
        {
            if (!actor) return false;
            if (!actor.TrySetDirector(this)) return false;

            currentActor = actor;
            return true;
        }

        public bool AuthorityAgainst(Director<A> challenged)
        {
            return this.authorityLevel >= challenged.authorityLevel;
        }

        // Command Queue
        protected void EnqueueNextUpdateCommand(ICommand command)
        {
            if (updateCommandQueue == null) updateCommandQueue = new Queue<ICommand>();

            updateCommandQueue.Enqueue(command);
        }
        protected void EnqueueNextFixedUpdateCommand(ICommand command)
        {
            if (fixedUpdateCommandQueue == null) fixedUpdateCommandQueue = new Queue<ICommand>();

            fixedUpdateCommandQueue.Enqueue(command);
        }
        protected void EnqueueNextTimedCommand(ICommand command)
        {
            if (timedCommandQueue == null) timedCommandQueue = new Queue<ICommand>();

            timedCommandQueue.Enqueue(command);
        }
        public ICommand SendNextUpdateCommand()
        {
            if (updateCommandQueue == null) return null;
            if (updateCommandQueue.Count == 0) return null;

            return updateCommandQueue.Dequeue();
        }
        public ICommand SendNextFixedUpdateCommand()
        {
            if (fixedUpdateCommandQueue == null) return null;
            if (fixedUpdateCommandQueue.Count == 0) return null;

            return fixedUpdateCommandQueue.Dequeue();
        }
        public ICommand SendNextTimedCommand()
        {
            if (timedCommandQueue == null) return null;
            if (timedCommandQueue.Count == 0) return null;

            return timedCommandQueue.Dequeue();
        }
    }
}
