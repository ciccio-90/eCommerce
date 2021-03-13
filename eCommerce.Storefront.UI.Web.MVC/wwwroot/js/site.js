function hideOverlay(overlayId) {
    $("#" + overlayId).animate({ opacity: "hide" });
}

function showOverlay(overlayId, idOfDivToOverlay) {
    var heightAdditionOfOverlay = 0;
    var pos = $("#" + idOfDivToOverlay).offset();
    var width = $("#" + idOfDivToOverlay).width();
    var height = $("#" + idOfDivToOverlay).height();

    $("#" + overlayId).css({
        "width": width + "px",
        "left": pos.left + "px",
        "top": pos.top + "px",
        "height": height + heightAdditionOfOverlay + "px"
    });

    $("#" + overlayId).show();
    $("#" + overlayId).animate({ opacity: 0.7 }, 1, function () { });
}

function updateBasketSummary(basketSummary) {
    if (basketSummary.numberOfItems == 0) {
        $('#basket-summary-text').text('is empty');
    } else {
        $('#basket-summary-text').text(basketSummary.numberOfItems + ' Item(s) at ' + basketSummary.basketTotal);
    }
}