namespace ProjectX.IdentityContext.Persistence.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Begin();
        void Commit();
        void RollBack();
    }
}
