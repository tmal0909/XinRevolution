$(function () {
    // 事件 - 項目點選
    $(document).on('click', '.js-viewcategory-item', function () {
        var data = {
            eventId : $(this).data('id')
        };

        $.ajax({
            url: '/FireGeneration/ViewCategoryEvent',
            data: data,
            success: function (result) {
                if (result)
                    $('#ViewCategoryEventContainer').html(result);
            }
        });
    });

    // 預設顯示第一筆
    $('.js-viewcategory-item').first().click();
});