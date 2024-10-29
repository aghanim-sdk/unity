var plugin = 
{
    Loaded: function()
    {
        appLoaded();
    },
    GetItemsList: function()
    {
        getItems();
    },
    Order: function(item)
    {
        order(UTF8ToString(item));
    }
};

mergeInto(LibraryManager.library, plugin);