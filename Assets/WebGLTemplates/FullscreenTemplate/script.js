function order(item) {
    window.parent.postMessage({ action: "buyItem", sku: item }, "*");
    console.log(`Attempting to buy item: ${item}`);
}

function getItems() {
    console.log('call getItems()');
    window.parent.postMessage({ action: "getItems" }, "*");
}

window.addEventListener("message", (event) => {
    if (event.data.action === "receiveItems") {
        console.log('message receiveItems');
        const itemsJson = JSON.stringify({ items: event.data.items });
        console.log(itemsJson);
        window.unityInstance.SendMessage("WebAppWrapper", "OnItemsReceived", itemsJson);
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