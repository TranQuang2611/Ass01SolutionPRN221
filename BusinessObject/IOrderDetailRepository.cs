﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public interface IOrderDetailRepository
    {
        List<OrderDetail> GetListOrderDetailByOrderId(int orderId);
        List<Product> GetListOderDetail();
    }
}
