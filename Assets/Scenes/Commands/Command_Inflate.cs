using LeRatTools;

public class Command_Inflate : ICommand
{
    private float ratio;

    public Command_Inflate(float ratio)
    {
        this.ratio = ratio;
    }

    public float GetRatio()
    {
        return ratio;
    }
}

