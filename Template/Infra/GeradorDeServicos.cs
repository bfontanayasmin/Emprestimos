using Emprestimos.Infra;
using Microsoft.Extensions.DependencyInjection;

namespace Emprestimos.Infra
{
    public static class GeradorDeServicos
    {
        private static ServiceProvider _serviceProvider;

        public static void RegistrarProvider(ServiceProvider provider)
        {
            _serviceProvider = provider;
        }

        public static DataContext CarregarContexto()
        {
            if (_serviceProvider == null)
                throw new Exception("ServiceProvider não inicializado.");

            var contexto = _serviceProvider.GetService<DataContext>();
            if (contexto == null)
                throw new Exception("DataContext não encontrado no ServiceProvider.");

            return contexto;
        }
    }
}
