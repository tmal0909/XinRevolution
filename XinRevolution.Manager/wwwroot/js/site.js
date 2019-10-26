// Data Table 初始化
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
function initFileInput($fileInput, $fileNameInput) {
    $fileInput.on('change', function () {
        var fileName = $(this).val().split('\\').pop();

        $fileNameInput.val(fileName);
        $fileNameInput.valid();
    });
}

// Date Picker 初始化
function initDatePicker($dateInput) {
    $dateInput.datepicker({
        format: 'yyyy-mm-dd',
        autoclose: true,
        startDate: '2019-01-01',
        enableOnReadonly: false,
        calendarWeeks: false,
        todayHighlight: true,
        language: 'zh-TW'
    });
}

// 切換部落格輸入視窗
function showBlogReferenceInputBlock($referenceType) {
    if ($referenceType.val() === '0')
        $('#TextReferenceBlock').removeClass('d-none');
    else
        $('#MediaReferenceBlock').removeClass('d-none');
}