using System;
using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace Aghanim.Responses
{
    [Serializable]
    public class ItemList
    {
        public List<Item> items;
    }

    [Serializable]
    public class Item
    {
        public string name;
        public string description;
        public int price;
        public int price_point;
        public int reward_points_price;
        public int discount_percent;
        public int bonus_percent;
        public string currency;
        public string sku;
        public string image_url;
        public string image_url_featured;
        public string background_image_url;
        public string type;
        public int quantity;
        public bool is_stackable;
        public bool is_currency;
        public int position;
        public List<string> categories;
        public List<NestedItem> nested_items;
        public string id;
        public List<Property> properties;
        public long created_at;
        public long modified_at;
        public long archived_at;
        public int price_minor_unit;
        public List<NestedItemRead> nested_items_read;
        public bool is_custom;
    }

    [Serializable]
    public class NestedItem
    {
        public string id;
        public int count;
    }

    [Serializable]
    public class Property
    {
        public string property_id;
        public Data data;
        public int position;
        public string id;
        public string item_id;
        public ItemProperty item_property;
    }

    [Serializable]
    public class Data
    {
        public int value;
    }

    [Serializable]
    public class ItemProperty
    {
        public string name;
        public string description;
        public string icon_url;
        public Config config;
        public string id;
    }

    [Serializable]
    public class Config
    {
        public string property_type;
        public int max;
        public int min;
    }

    [Serializable]
    public class NestedItemRead { }
}