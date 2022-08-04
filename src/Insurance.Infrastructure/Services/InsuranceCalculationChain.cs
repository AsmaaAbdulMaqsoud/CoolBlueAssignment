namespace Insurance.Infrastructure.Services;

public abstract class Link
{
    private Link? _nextLink;
    public void SetSuccessor(Link next)
    {
        _nextLink = next;
    }

    public virtual decimal Execute(decimal salesPrice)
    {
        if (_nextLink != null)
            return _nextLink.Execute(salesPrice);
        return 0;
    }
}

public class IsSalesPriceBetween500And2000 : Link
{
    public override decimal Execute(decimal salesPrice)
    {
        if (salesPrice >= 500 && salesPrice < 2000)
            return 1000;
        return base.Execute(salesPrice);
    }
}

public class IsSalesPriceMoreThaneOrEqual2000 : Link
{
    public override decimal Execute(decimal salesPrice)
    {
        if (salesPrice >= 2000)
            return 2000;
        return base.Execute(salesPrice);
    }
}
