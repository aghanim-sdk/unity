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
        public string currency;
        public float amount_decimal;
    }
    
    [Serializable]
    public class OrderStatusList
    {
        public List<OrderStatus> orders;
    }
}