using System;
using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace Aghanim.Responses
{
    [Serializable]
    public class OrderStatus
    {
        public string order_id;
        public string item_sku;
        public bool is_success;
    }
    
    [Serializable]
    public class OrderStatusList
    {
        public List<OrderStatus> orders;
    }
}