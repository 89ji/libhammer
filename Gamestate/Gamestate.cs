using LibHammer.Deployments;
using LibHammer.Gamestate.Serviceproviders;
using LibHammer.MissionRules;
using LibHammer.Primaries;
using LibHammer.Secondaries;
using LibHammer.Structs;

namespace LibHammer.Gamestate;

public class Gamestate
{
    // State managers
    public PhaseManager PhaseMan;


    // Stuff for external resolvers
    public IDistanceProvider DistanceProvider;
    public IEngagementProvider EngagementProvider;
    public IViewClearanceProvider ViewClearanceProvider;


    // Stuff for player 1
    public Player Player1 = new();


    // Stuff for player 2
    public Player Player2 = new();

    PrimaryMission Primary;
    MissionRule Rule;
    Deployment Deploy;

    public bool PlayersReady
    {
        get
        {
            return Player1.Name != null && Player2.Name != null;
        }
    }


    public void InsertArmy(List<Troop> army, string player)
    {
        if (Player1.Name == null)
        {
            Player1.Name = player;
            Player1.Army = Army2Board(army);
        }

        else if (Player2.Name == null)
        {
            Player2.Name = player;
            Player2.Army = Army2Board(army);
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

        PhaseMan = new(Player1: Player1, Player2: Player2, state: this);

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
                ChoosingPlayer = Player1.Name;
                OtherPlayer = Player2.Name;
            }
            else
            {
                ChoosingPlayer = Player2.Name;
                OtherPlayer = Player1.Name;
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
}