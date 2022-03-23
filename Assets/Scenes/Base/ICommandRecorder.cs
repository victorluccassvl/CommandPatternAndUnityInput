namespace LeRatTools
{
    public interface ICommandRecorder
    {
        public struct CommandRecord
        {
            public ICommand command;
            public ICommandReceiver receiver;
            public uint frame;
        }

        public void AddRecord(ICommand command, ICommandReceiver receiver);

        public void StartRecording();

        public void StopRecording();

        public bool PlayRecords();
    }
}
