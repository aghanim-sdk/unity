function order(item, itemName, description, imageUrl) {
    window.parent.postMessage({ action: "buyItem", sku: item, name: itemName, desc: description, img: imageUrl }, "*");
    console.log(`Attempting to buy item: ${item}`);
}

function getItems() {
    console.log('call getItems()');
    window.parent.postMessage({ action: "getItems" }, "*");
}

function getUnhandledPaidOrders() {
    console.log('call getUnhandledPaidOrders()');
    window.parent.postMessage({ action: "getUnhandledPaidOrders" }, "*");
}

function auth(player_id, player_name, avatar_url) {
    console.log('call auth()');
    window.parent.postMessage({ action: "auth", player: player_id, name: player_name, avatar: avatar_url }, "*");
}

window.addEventListener("message", (event) => {
    if (event.data.action === "receiveItems") {
        console.log('message receiveItems');
        const itemsJson = JSON.stringify({ items: event.data.items });
        console.log(itemsJson);
        window.unityInstance.SendMessage("AghanimSDK", "OnItemsReceived", itemsJson);
    }
    if (event.data.action === "itemPurchaseStatus") {
        console.log('message itemPurchaseStatus');
        const orderJson = JSON.stringify(event.data.order);
        console.log(orderJson);
        window.unityInstance.SendMessage("AghanimSDK", "OnItemPurchaseStatus", orderJson);
    }
    if (event.data.action === "receiveUnhandledPaidOrders") {
        console.log('message receiveUnhandledPaidOrders');
        const ordersJson = JSON.stringify({ orders: event.data.orders });
        console.log(ordersJson);
        window.unityInstance.SendMessage("AghanimSDK", "OnUnhandledPaidOrdersReceived", ordersJson);
    }
});

function appLoaded() {}

var meta = document.createElement('meta');
meta.name = 'viewport';
meta.content = 'width=device-width, height=device-height, initial-scale=1.0, user-scalable=no, shrink-to-fit=yes';
document.getElementsByTagName('head')[0].appendChild(meta);

var canvas = document.querySelector("#unity-canvas");
canvas.style.width = "100%";
canvas.style.height = "100%";
canvas.style.position = "fixed";

document.body.style.textAlign = "left";