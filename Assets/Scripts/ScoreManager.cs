public class ScoreManager : UnityEngine.MonoBehaviour
{
    public int CurrentScore { get; private set; }

    public void AddScore(int delta)
    {
        CurrentScore += delta;
    }

    public void ResetScore() => CurrentScore = 0;
}
