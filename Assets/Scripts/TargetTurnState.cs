using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TargetTurnState : IGameState
{
    private readonly GameController ctx;
    private readonly Spot spotThatJustFreedByPlayer;

    public TargetTurnState(GameController controller, Spot freedByPlayer = null)
    {
        ctx = controller;
        spotThatJustFreedByPlayer = freedByPlayer;
    }

    public void EnterState()
    {
        ctx.uiManager.SetPrompt("Target is thinking...");
        ctx.StartCoroutine(TargetMoveRoutine());
    }

    private IEnumerator TargetMoveRoutine()
    {
        var choice = ctx.spotManager.GetRandomValidSpotForTarget(
            ctx.target.CurrentSpot,
            ctx.player.CurrentSpot,
            ctx.sequenceManager.SequenceSpots
        );

        if (choice == null)
        {
            ctx.ChangeState(new PlayerTurnState(ctx));
            yield break;
        }

        var fromSpot = ctx.target.CurrentSpot;

        Tween moveTween = ctx.target.MoveToSpotTween(choice);

        yield return moveTween.WaitForCompletion();

        ctx.sequenceManager.AppendSpot(fromSpot);

        ctx.uiManager.ShowSequenceNumbers(ctx.sequenceManager.SequenceSpots);

        yield return new WaitForSeconds(0.3f);

        ctx.ChangeState(new PlayerTurnState(ctx));
    }

    public void ExitState() { }

    public void OnSpotClicked(Spot spot)
    {
        // Do Nothing - for Target
    }

    public void Update() { }
}
