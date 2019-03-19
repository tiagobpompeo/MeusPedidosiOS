﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeusPedidosiOS.Models;

namespace MeusPedidosiOS.Contracts.Data
{
    public interface ICatalogDataService
    {
        Task<IEnumerable<Products>> GetAllCatalogAsync();
        Task<IEnumerable<PriceOff>> GetProductsOfTheWeekAsync();
        Task<IEnumerable<Categories>> GetAllCategories();

    }
}
