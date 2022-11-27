﻿using Orders.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson6.Service
{
    internal interface IBuyerService
    {
        public IList<Buyer> GetAll();

        public Task<Buyer> GetById(int buyerId);
    }
}
