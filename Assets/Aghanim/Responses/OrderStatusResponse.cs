﻿using System;
// ReSharper disable InconsistentNaming

namespace Aghanim.Responses
{
    [Serializable]
    public class OrderStatus
    {
        public string item_sku;
        public bool is_success;
    }
}