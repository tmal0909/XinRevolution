$(function () {
    // 事件 - Tag 點選
    $(document).on('click', '.js-blog-tag', function () {
        $(this).toggleClass('active');

        var enableTagIds = getEnableTags();

        if (enableTagIds.length <= 0) {
            $('.js-blog-unit').removeClass('display-none');
            return;
        }

        $('.js-blog-unit').addClass('display-none');

        // 驗證每個 blog 是否包含開啟之標籤
        $('.js-blog-unit').each(function (index, blog) {
            var blogTags = $(blog).attr('data-tags').split(';');

            for (var i = 0, len = blogTags.length; i < len; i++) {
                if (enableTagIds.indexOf(blogTags[i]) >= 0) {
                    $(blog).removeClass('display-none');
                    break;
                }
            }
        });
    });
});

function getEnableTags() {
    var result = [];

    $('.js-blog-tag.active').each(function (index, element) {
        result.push($(element).attr('data-tagid'));
    });

    return result;
}