mergeInto(LibraryManager.library, {
    IsMobile: function () {
        var ua = window.navigator.userAgent.toLowerCase();
        var mobilePattern = /android|iphone|ipad|ipod/i;

        return ua.search(mobilePattern) !== -1 || (ua.indexOf("macintosh") !== -1 && "ontouchend" in document);
    },
    /*GetOwnedLoyaltyPointsAsync: function (data) {
        window.GetOwnedLoyaltyPoints(UTF8ToString(data));
    },*/
    /*GetMinLoyaltyPointsAsync: function(data) {
        window.GetMinLoyaltyPoints(UTF8ToString(data));
    },*/
    ConsumeLoyaltyPointsCallback: function(points){
        if(!window.unityEventMap)
        {
            console.log("ready : missing event map");
            return;
        }

        const listener = window.unityEventMap.get("consume_loyalty_points");
        if(listener)
        {
            listener(points);
        }
    },
    ReadyCallback: function() {
        if(!window.unityEventMap)
        {
            console.log("ready : missing event map");
            return;
        }

        const listener = window.unityEventMap.get("ready");
        if(listener)
        {
            listener();
        }
    },
    StartCallback: function() {
        if(!window.unityEventMap) 
        {
            console.log("start: missing event map");
            return;
        }

        const listener = window.unityEventMap.get("start");
        if(listener)
        {
            listener();
        }
    },
    EndCallback: function(data) {
        if(!window.unityEventMap) 
        {
            console.log("end: missing event map");
            return;
        }
        const listener = window.unityEventMap.get("end");
        if(listener) 
        {
            listener(UTF8ToString(data));
        }
    }
});