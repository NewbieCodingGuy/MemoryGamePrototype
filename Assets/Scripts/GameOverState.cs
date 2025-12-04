using UnityEngine;

public class GameOverState : IGameState
{
    private readonly GameController ctx;
    public GameOverState(GameController controller)
    {
        ctx = controller;
    }
    public void EnterState()
    {
        ctx.uiManager.ShowGameOver(ctx.scoreManager.CurrentScore, ctx.successfulMoves);
        ctx.spotManager.DestroyAllSpots();
        ctx.HidePlayersOnGameOve();
    }

    public void ExitState() { }
    public void OnSpotClicked(Spot spot) { }
    public void Update() { }
}
