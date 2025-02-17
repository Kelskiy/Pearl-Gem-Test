using UnityEngine;

public class BallContoller : MonoBehaviour
{
    public BallColor ballColor;
    private Renderer ballRenderer;

    public int segmentID { get; private set; }

    void Start()
    {
        ballRenderer = GetComponent<Renderer>();
    }

    public void InitializedBall(int ID)
    {
        segmentID = ID;

        SetColor((BallColor)(ID));

        SpawnManager.Instance.AddColor(ballColor);
    }

    public void SetColor(BallColor color)
    {
        if (ballRenderer == null)
        {
            ballRenderer = GetComponent<Renderer>();
            ballColor = color;
            ballRenderer.material.color = GetColorFromEnum(color);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerBall playerBall = collision.transform.GetComponent<PlayerBall>();

        if (playerBall != null)
        {
            if (playerBall.ballColor == ballColor)
                SpawnManager.Instance.DestroySegment(segmentID, ballColor);
        }
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
            case BallColor.None:
            default: return Color.white;
        }
    }

}