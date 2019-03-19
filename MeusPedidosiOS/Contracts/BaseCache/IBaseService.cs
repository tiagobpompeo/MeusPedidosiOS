using System;
using System.Threading.Tasks;
using Akavache;

namespace MeusPedidosiOS.Contracts.BaseCache
{
    public interface IBaseService
    {
        Task<T> GetFromCache<T>(string cacheName);
        void InvalidateCache();
    }
}
