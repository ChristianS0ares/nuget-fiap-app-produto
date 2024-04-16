
using nuget_fiap_app_produto_repository.Interface;

namespace nuget_fiap_app_produto_repository.DB
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryDB _session;

        public UnitOfWork(RepositoryDB session)
        {
            _session = session;
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
