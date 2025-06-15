namespace LibHammer.ControlPoint;

class ControlPoint
{
    public static IControlPointResolver? Resolver;

    public ObjectiveInfluence GetInfluence()
    {
        if (Resolver == null) throw new Exception("Control point resolver not set!!");
        return Resolver.ResolveInfluences();
    }
}