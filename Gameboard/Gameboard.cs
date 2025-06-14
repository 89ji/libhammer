using LibHammer.Deployments;
using LibHammer.MissionRules;
using LibHammer.Primaries;
using LibHammer.Structs;

namespace LibHammer.Gameboard;

class Gameboard
{
    // Stuff for player 1
    string? Player1_Name;
    List<BoardTroop> Player1_Army = new();

    // Stuff for player 2
    string? Player2_Name;
    List<BoardTroop> Player2_Army = new();

    PrimaryMission Primary;
    MissionRule Rule;
    Deployment Deploy;

    public bool PlayersReady
    {
        get
        {
            return Player1_Name != null && Player2_Name != null;
        }
    }


    public void InsertArmy(List<Troop> army, string player)
    {
        if (Player1_Name == null)
        {
            Player1_Name = player;
            Player1_Army = Army2Board(army);
        }

        else if (Player2_Name == null)
        {
            Player2_Name = player;
            Player2_Army = Army2Board(army);
        }

        else throw new Exception("Both players already added!");
    }

    List<BoardTroop> Army2Board(List<Troop> army)
    {
        List<BoardTroop> boardArmy = new();
        foreach (Troop troop in army)
        {
            var btroop = new BoardTroop(troop);
            boardArmy.Add(btroop);
        }
        return boardArmy;
    }

    // Setup deployments, mission rule, and primary mission
    public void SetupMission()
    {
        if (!PlayersReady) throw new Exception("Players are not yet initialized!");
        Primary = PrimaryManager.RollPrimary();
        Rule = RuleManager.RollMission();
        Deploy = DeploymentManager.RollDeployment();
    }

    string? ChoosingPlayer;
    string? OtherPlayer;
    public string GetChoosingPlayer()
    {
        if (ChoosingPlayer == null)
        {
            Random rng = new();
            if (rng.Next(0, 2) == 0)
            {
                ChoosingPlayer = Player1_Name;
                OtherPlayer = Player2_Name;
            }
            else
            {
                ChoosingPlayer = Player2_Name;
                OtherPlayer = Player1_Name;
            }
        }
        return ChoosingPlayer;
    }

    public string Attacker;
    public string Defender;

    public void ChooseRole(bool isAttacking)
    {
        if (isAttacking)
        {
            Attacker = ChoosingPlayer;
            Defender = OtherPlayer;
        }
        else
        {
            Defender = ChoosingPlayer;
            Attacker = OtherPlayer;
        }
    }

    public void SetupSecondaries()
    {
        throw new NotImplementedException();
    }

}