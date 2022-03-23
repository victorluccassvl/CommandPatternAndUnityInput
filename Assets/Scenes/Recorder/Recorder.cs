using System.Collections.Generic;
using UnityEngine;
using LeRatTools;

public class Recorder : MonoBehaviour, ICommandRecorder
{
    private Queue<ICommandRecorder.CommandRecord> history = new Queue<ICommandRecorder.CommandRecord>();
    private Queue<ICommandRecorder.CommandRecord> playlist;
    private uint currentFrame;
    private bool isRecording;
    public bool IsRecording
    {
        get
        {
            return isRecording;
        }
    }

    private void FixedUpdate()
    {
        currentFrame++;
    }

    public void AddRecord(ICommand command, ICommandReceiver receiver)
    {
        if (!isRecording) return;
        ICommandRecorder.CommandRecord record;

        record.command = command;
        record.receiver = receiver;
        record.frame = currentFrame;

        history.Enqueue(record);
    }

    public bool PlayRecords()
    {
        if (history.Count == 0) return false;
        if (playlist == null)
        {
            playlist = new Queue<ICommandRecorder.CommandRecord>(history);
            currentFrame = 0;
        }

        while (playlist.Count != 0)
        {
            ICommandRecorder.CommandRecord recordToplay = playlist.Peek();
            if (recordToplay.frame == currentFrame)
            {
                recordToplay.command.Do(recordToplay.receiver);
                playlist.Dequeue();
                continue;
            }
            return true;
        }
        return false;
    }

    public void StartRecording()
    {
        if (isRecording) return;

        print(gameObject.name + " is recording.");
        currentFrame = 0;
        history.Clear();
        isRecording = true;
    }

    public void StopRecording()
    {
        print(gameObject.name + " stopped recording.");
        if (!isRecording) return;
        isRecording = false;
    }
}
