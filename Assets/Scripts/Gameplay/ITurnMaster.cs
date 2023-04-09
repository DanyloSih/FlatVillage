namespace FlatVillage.Gameplay
{

    public interface ITurnMaster
    {
        public int MaxTurns { get; }
        public int CurrentTurnNumber { get; }
        public Player CurrentTurnPlayer { get; }
        public void StartNextTurn();
    }
}
