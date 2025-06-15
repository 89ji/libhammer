using LibHammer.Gamestate.InterphaseActions;

namespace LibHammer.Gamestate;

public class PhaseManager(string Player1_Name, string Player2_Name, Gamestate state)
{
    required public Gamestate state;
    public Phase CurrentPhase = Phase.Pregame;

    public int Turn = 1;

    required public string Player1_Name;
    public InterphaseManager Player1_Interphase = new();

    required public string Player2_Name;
    public InterphaseManager Player2_Interphase = new();


    public string ActivePlayer = Player1_Name;

    public void NextPhase()
    {
        switch (CurrentPhase)
        {
            case Phase.Pregame:
                CurrentPhase = Phase.Command;
                FireInterphase(Interphase.PreCommand);
                break;

            case Phase.Command:
                FireInterphase(Interphase.PostCommand);
                CurrentPhase = Phase.Move;
                FireInterphase(Interphase.PreMove);
                break;

            case Phase.Move:
                FireInterphase(Interphase.PostMove);
                CurrentPhase = Phase.Shoot;
                FireInterphase(Interphase.PreShoot);
                break;

            case Phase.Shoot:
                FireInterphase(Interphase.PostShoot);
                CurrentPhase = Phase.Charge;
                FireInterphase(Interphase.PreCharge);
                break;

            case Phase.Charge:
                FireInterphase(Interphase.PostCharge);
                CurrentPhase = Phase.Fight;
                FireInterphase(Interphase.PreFight);
                break;

            case Phase.Fight:
                FireInterphase(Interphase.PostFight);

                // Swap the active player and increment the turn counter if p1 is back
                if (ActivePlayer == Player1_Name)
                {
                    ActivePlayer = Player2_Name;
                }
                else
                {
                    ActivePlayer = Player1_Name;
                    Turn++;
                }

                // Move to the command phase and fire the new active players precommand actions
                CurrentPhase = Phase.Command;
                FireInterphase(Interphase.PreCommand);
                break;


        }
    }


    void FireInterphase(Interphase iphase)
    {
        if (ActivePlayer == Player1_Name) Player1_Interphase.ExecuteActions(iphase, state, CurrentPhase);
        else Player2_Interphase.ExecuteActions(iphase, state, CurrentPhase);
    }
}