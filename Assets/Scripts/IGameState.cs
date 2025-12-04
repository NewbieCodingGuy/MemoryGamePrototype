using System;

public interface IGameState
{
    void EnterState();
    void ExitState();
    void OnSpotClicked(Spot spot);
    void Update();
}
