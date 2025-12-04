using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("References")]
    public SpotManager spotManager;
    public SequenceManager sequenceManager;
    public GameCharacterController player;
    public GameCharacterController target;
    public UIManager uiManager;
    public ScoreManager scoreManager;
    public int maxSuccessfulMoves = 40;

    private IGameState currentState;

    // game tracking
    public int successfulMoves { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(InitializeRoutine());
    }

    private IEnumerator InitializeRoutine()
    {
        spotManager.SetupSpots();
        yield return new WaitForSeconds(2f);
        player.gameObject.SetActive(true);
        target.gameObject.SetActive(true);  

        var playerSpot = spotManager.GetRandomAvailableSpot();
        var targetSpot = spotManager.GetRandomAvailableSpot(exclude: playerSpot);

        player.SpawnAtSpot(playerSpot);
        target.SpawnAtSpot(targetSpot);

        sequenceManager.ClearSequence();

        //For Two Initial Moves by Target
        for (int i = 0; i < 2; i++)
            yield return StartCoroutine(TargetMakeInitialMove());

        ChangeState(new PlayerTurnState(this));

        uiManager.UpdateScore(scoreManager.CurrentScore);
        uiManager.UpdateSteps(successfulMoves);
    }

    private IEnumerator TargetMakeInitialMove()
    {
        var choice = spotManager.GetRandomValidSpotForTarget(
            target.CurrentSpot, player.CurrentSpot, sequenceManager.SequenceSpots);

        if (choice == null) yield break;

        var fromSpot = target.CurrentSpot;

        Tween moveTween = target.MoveToSpotTween(choice);
        yield return moveTween.WaitForCompletion();

        sequenceManager.AppendSpot(fromSpot);
        uiManager.ShowSequenceNumbers(sequenceManager.SequenceSpots);
    }

    public void ChangeState(IGameState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState?.EnterState();
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void NotifySpotClicked(Spot spot)
    {
        currentState?.OnSpotClicked(spot);
    }

    public IEnumerator TargetMoveAfterPlayer()
    {
        var choice = spotManager.GetRandomValidSpotForTarget(
            target.CurrentSpot, player.CurrentSpot, sequenceManager.SequenceSpots);

        if (choice == null) yield break;

        var fromSpot = target.CurrentSpot;

        Tween moveTween = target.MoveToSpotTween(choice);
        yield return moveTween.WaitForCompletion();

        sequenceManager.AppendSpot(fromSpot);
        uiManager.ShowSequenceNumbers(sequenceManager.SequenceSpots);
    }

    public void OnPlayerCorrectMoveFirstTry(Spot movedTo)
    {
        successfulMoves++;
        scoreManager.AddScore(500);
        uiManager.UpdateScore(scoreManager.CurrentScore);
        uiManager.UpdateSteps(successfulMoves);

        if (successfulMoves >= maxSuccessfulMoves)
            ChangeState(new GameOverState(this));
    }

    public void OnPlayerCorrectMoveNotFirstTry()
    {
        successfulMoves++;
        uiManager.UpdateSteps(successfulMoves);

        if (successfulMoves >= maxSuccessfulMoves)
            ChangeState(new GameOverState(this));
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HidePlayersOnGameOve()
    {
        player.gameObject.SetActive(false);
        target.gameObject.SetActive(false); 
    }
}
