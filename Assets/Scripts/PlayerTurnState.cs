using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerTurnState : IGameState
{
    private readonly GameController ctx;
    private bool currentIndexHadWrongTry = false;

    public PlayerTurnState(GameController controller)
    {
        ctx = controller;
    }

    public void EnterState()
    {
        InputHandler.OnSpotClicked += HandleSpotClicked;
        ctx.uiManager.SetPrompt("Player's turn: follow the sequence.");
        currentIndexHadWrongTry = false;
        if (ctx.sequenceManager.SequenceSpots.Count > 0)
        {
            ctx.uiManager.ShowSequenceNumbers(ctx.sequenceManager.SequenceSpots);
        }
    }

    public void ExitState()
    {
        InputHandler.OnSpotClicked -= HandleSpotClicked;
    }

    public void OnSpotClicked(Spot spot)
    {
        HandleSpotClicked(spot);
    }

    private void HandleSpotClicked(Spot spot)
    {
        if (spot.IsOccupied) return;
        if (ctx.sequenceManager.SequenceSpots.Count == 0)
        {
            ctx.uiManager.FlashWrong(spot);
            currentIndexHadWrongTry = true;
            return;
        }

        var expected = ctx.sequenceManager.SequenceSpots[0];

        if (spot == expected)
        {
            InputHandler.DisableInputTemporarily();

            var prev = ctx.player.CurrentSpot;

            Tween moveTween = ctx.player.MoveToSpotTween(spot);

            moveTween.OnComplete(() =>
            {
                ctx.player.StartCoroutine(ContinueAfterMove(prev));
            });
        }
        else
        {
            currentIndexHadWrongTry = true;
            ctx.uiManager.FlashWrong(spot);
        }
    }

    private IEnumerator ContinueAfterMove(Spot previousPlayerSpot)
    {
        ctx.sequenceManager.RemoveFirst();
        ctx.uiManager.ClearSequenceNumbers();

        if (!currentIndexHadWrongTry)
            ctx.OnPlayerCorrectMoveFirstTry(previousPlayerSpot);
        else
            ctx.OnPlayerCorrectMoveNotFirstTry();

        ctx.ChangeState(new TargetTurnState(ctx, previousPlayerSpot));

        yield break;
    }

    public void Update() { }
}
