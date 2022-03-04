using LeRatTools;

public class Command_Deflate : ICommand
{
    private float ratio;

    public Command_Deflate(float ratio)
    {
        this.ratio = ratio;
    }

    public float GetRatio()
    {
        return ratio;
    }
}
