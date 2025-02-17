using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public BallColor ballColor { get; private set; }
    public Renderer ballRenderer;

    public void SetBallColor(BallColor color)
    {
        ballColor = color;

        ballRenderer.material.color = GetColorFromEnum(color);
    }

    private Color GetColorFromEnum(BallColor color)
    {
        switch (color)
        {
            case BallColor.Red: return Color.red;
            case BallColor.Green: return Color.green;
            case BallColor.Blue: return Color.blue;
            case BallColor.Yellow: return Color.yellow;
            case BallColor.Cyan: return Color.cyan;
            case BallColor.Magenta: return Color.magenta;
            default: return Color.white;
        }
    }
}