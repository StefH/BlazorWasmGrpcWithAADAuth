namespace Client.Authentication.Interfaces
{
    public interface IScopeContext<T>
    {
        string[] GetScopes();
    }
}