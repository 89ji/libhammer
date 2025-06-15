using LibHammer.Structs;

namespace LibHammer.Gamestate;

// Represents a troop on the board with its current move, damage taken, etc
class BoardTroop
{
    public Troop Stats;
    public int DamageTaken;

    public BoardTroop(Troop referenceTroop)
    {
        Stats = referenceTroop;
    }
}