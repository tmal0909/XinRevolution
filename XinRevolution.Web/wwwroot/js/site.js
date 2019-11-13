$(function () {
    // 隱藏動畫視窗
    $(window).on('load', function () {
        $('#layout-animation-block').delay(1000).fadeOut("slow");
    });

    // 事件 - Popup Show Btn 點選
    $(document).on('click', '.js-popup-show-btn', function () {
        var target = $(this).data('target');

        if (target)
            toggleBlock($(target), true);
    });

    // 事件 - Popup Close Btn 點選
    $(document).on('click', '.js-popup-close-btn', function () {
        var target = $(this).parents('.js-popup-block').first();

        if (target)
            toggleBlock($(target), false);
    });
});