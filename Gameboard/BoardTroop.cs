using LibHammer.Structs;

namespace LibHammer.Gameboard;

// Represents a troop on the board with its current move, damage taken, etc
class BoardTroop
{
    public Troop Troopstats;

    public BoardTroop(Troop referenceTroop)
    {
        Troopstats = referenceTroop;
    }
}