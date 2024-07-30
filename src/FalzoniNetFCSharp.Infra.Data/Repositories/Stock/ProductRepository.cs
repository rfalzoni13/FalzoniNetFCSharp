﻿using FalzoniNetFCSharp.Domain.Entities.Stock;
using FalzoniNetFCSharp.Domain.Interfaces.Stock;
using FalzoniNetFCSharp.Infra.Data.Repositories.Base;

namespace FalzoniNetFCSharp.Infra.Data.Repositories.Stock
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
    }
}
