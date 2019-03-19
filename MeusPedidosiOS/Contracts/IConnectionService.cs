using System;
using System.Threading.Tasks;
using MeusPedidosiOS.Models;

namespace MeusPedidosiOS.Contracts
{
    public interface IConnectionService
    {
        Task<Response> CheckConnection();
        Response CheckConnectionApi();
        bool CheckConnectionTest();
    }
}
