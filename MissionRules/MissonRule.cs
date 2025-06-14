namespace LibHammer.MissionRules;

abstract class MissionRule
{
    public string Name;
    public string Flavor;
    public string Description;

    public abstract void DoSomething();
}