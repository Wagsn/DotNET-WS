// site.js

// 待办api
const uri = 'api/todo';

// 待办数据
let todos = null;

// 获取待办数量并打印在屏幕上
function getCount(data) {
    const el = $('#counter');
    let name = 'to-do';
    if (data) {
        if (data > 1) {
            name = 'to-dos';
        }
        el.text(data + ' ' + name);
    } else {
        el.html('No ' + name);
    }
}

// 当文档加载完之后获取待办数据
$(document).ready(function () {
    getData();
});

// 获取所有待办数据并显示在屏幕上
function getData() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            console.log("respone: " + data);
            $('#todos').empty();
            if (data.code != 0) {
                alert(data.message);
                return;
            }
            let items = data.extension;
            getCount(items.length);
            $.each(items, function (key, item) {
                const checked = item.isComplete ? 'checked' : '';

                $('<tr><td><input disabled="true" type="checkbox" ' + checked + '></td>' +
                    '<td>' + item.name + '</td>' +
                    '<td><button onclick="editItem(' + item.id + ')">Edit</button></td>' +
                    '<td><button onclick="deleteItem(' + item.id + ')">Delete</button></td>' +
                    '</tr>').appendTo($('#todos'));
            });

            todos = items;
        }
    });
}

// 添加待办
function addItem() {
    // 对待办名称进行正则表达式输入校验
    let todoItemName = $('#add-name').val();
    const item = {
        'UseId': 0,
        'Name': todoItemName,
        'IsComplete': false
    };

    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: uri,
        contentType: 'application/json',
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert('here', errorThrown);
        },
        success: function (result) {
            getData();
            $('#add-name').val('');
        }
    });
}

// 删除待办
function deleteItem(id) {
    $.ajax({
        url: uri + '/' + id,
        type: 'DELETE',
        success: function (result) {
            getData();
        }
    });
}

// 编辑待办，将被编辑的待办数据传到编辑框中
function editItem(id) {
    $.each(todos, function (key, item) {
        if (item.id === id) {
            $('#edit-name').val(item.name);
            $('#edit-id').val(item.id);
            $('#edit-isComplete')[0].checked = item.isComplete;
        }
    });
    $('#spoiler').css({ 'display': 'block' });
}

// 提交修改待办
$('.my-form').on('submit', function () {
    const item = {
        'name': $('#edit-name').val(),
        'isComplete': $('#edit-isComplete').is(':checked'),
        'id': $('#edit-id').val()
    };

    $.ajax({
        url: uri + '/' + $('#edit-id').val(),
        type: 'PUT',
        accepts: 'application/json',
        contentType: 'application/json',
        data: JSON.stringify(item),
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

// 关闭编辑框，在页面中隐藏
function closeInput() {
    $('#spoiler').css({ 'display': 'none' });
}