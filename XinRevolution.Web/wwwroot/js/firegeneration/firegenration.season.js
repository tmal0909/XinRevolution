$(function () {
    // 章節點選
    $(document).on('click', '.js-chapter-node', function () {
        var resource = $(this).data('resource');
        var name = $(this).data('name');
        var intro = $(this).data('intro');
        var id = $(this).data('id');

        $('#chapter-resource').attr('src', resource);
        $('#chapter-name').text(name);
        $('#chapter-intro').text(intro);
        $('#chapter-read-btn').data('id', id);

        $('.js-chapter-node').removeClass('active');
        $(this).addClass('active');
    });

    // 漫畫點選


    // 預設點擊第一筆章節
    $('.js-chapter-node').first().click();
});