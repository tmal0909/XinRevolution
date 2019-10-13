// 使用方式 : var fullName = String.format('Hello. My name is {0} {1}.', 'FirstName', 'LastName');
String.format = function () {
    var s = arguments[0];

    if (s === null)
        return "";

    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = getStringFormatPlaceHolderRegEx(i);

        s = s.replace(reg, (arguments[i + 1] === null ? "" : arguments[i + 1]));
    }

    return cleanStringFormatResult(s);
};

// 使用方式 : var fullName = 'Hello. My name is {0} {1}.'.format('FirstName', 'LastName');
String.prototype.format = function () {
    var txt = this.toString();

    for (var i = 0; i < arguments.length; i++) {
        var exp = getStringFormatPlaceHolderRegEx(i);
        txt = txt.replace(exp, (arguments[i] === null ? "" : arguments[i]));
    }

    return cleanStringFormatResult(txt);
};

// 讓輸入的字串可以包含{}
function getStringFormatPlaceHolderRegEx(placeHolderIndex) {
    return new RegExp('({)?\\{' + placeHolderIndex + '\\}(?!})', 'gm');
}
// 當format格式有多餘的position時，就不會將多餘的position輸出
function cleanStringFormatResult(txt) {
    if (txt === null)
        return "";

    return txt.replace(getStringFormatPlaceHolderRegEx("\\d+"), "");
}

// DataTable 初始化
function initDataTable($table, createUrl) {
    $table.DataTable({
        dom: '<"dataTables_button">ftipr',
        pageLength: 10,
        language: {
            "emptyTable": "無資料...",
            "processing": "處理中...",
            "loadingRecords": "載入中...",
            "lengthMenu": "顯示 _MENU_ 項結果",
            "zeroRecords": "沒有符合的結果",
            "info": "顯示第 _START_ 至 _END_ 項結果，共 _TOTAL_ 項",
            "infoEmpty": "顯示第 0 至 0 項結果，共 0 項",
            "infoFiltered": "(從 _MAX_ 項結果中過濾)",
            "infoPostFix": "",
            "search": "搜尋:",
            "paginate": {
                "first": "第一頁",
                "previous": "上一頁",
                "next": "下一頁",
                "last": "最後一頁"
            },
            "aria": {
                "sortAscending": ": 升冪排列",
                "sortDescending": ": 降冪排列"
            }
        }
    });

    if (createUrl) {
        var element = String.format("<a class='icon-create' title='新增' href={0} />", createUrl);
        $('.dataTables_button').html(element);
    }
}

// File Input 初始化
function initFileInput($fileInput, $resourceName) {
    $fileInput.on('change', function () {
        var fileName = $(this).val().split('\\').pop();

        $resourceName.val(fileName);
    });
}