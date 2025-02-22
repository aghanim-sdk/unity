﻿var plugin = 
{
    Loaded: function()
    {
        appLoaded();
    },
    GetItemsList: function()
    {
        getItems();
    },
    GetUnhandledPaidOrders: function()
    {
        getUnhandledPaidOrders();
    },
    Order: function(item, itemName, description, imageUrl)
    {
        order(UTF8ToString(item), UTF8ToString(itemName), UTF8ToString(description), UTF8ToString(imageUrl));
    },
    Auth: function(playerId, playerName, avatarUrl)
    {
        auth(UTF8ToString(playerId), UTF8ToString(playerName), UTF8ToString(avatarUrl));
    },
    AppsFlyerSendEvent: function(eventName, eventValueString)
    {
        AppsFlyer_sendEvent(eventName, eventValueString);
    },
    AppsFlyerSetCustomerUserId: function(userId)
    {
        AppsFlyer_setCustomerUserId(userId);
    },
    CallReloadPage: function()
    {
        reloadPage();
    },
};

mergeInto(LibraryManager.library, plugin);