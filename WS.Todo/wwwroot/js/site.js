// site.js

// 待办api
const uri = 'api/todo';

// 待办数据
let todos = null;

let signUser = {
    "id": null,
    "name": null,
    "pwd": null
};


// 当文档加载完之后
$(document).ready(function () {
    eventListenInit();  // 事件监听初始化
});

// 获取登陆用户信息
function getSignUser() {
    signUser.name = $("#username").val();
    signUser.pwd = $("#password").val();
    console.log("sign user: ", signUser);
    return signUser;
}

// 获取待办数量并打印在屏幕上
function getCount(data) {
    const el = $('#counter');
    let name = '待办项';
    if (data) {
        el.text(data +'条'+ name);
    } else {
        el.html('没有' + name);
    }
}

// 事件监听初始化，等页面加载完执行
function eventListenInit() {
    // 刷新按钮点击事件监听
    $("#refresh").on("click", function () {
        getData();
    })

    // 修改待办提交
    $('#edit-submit-btn').on('click', function () {
        const todoItem = {
            user: getSignUser(),
            model: {
                name: $('#edit-name').val(),
                isComplete: $('#edit-isComplete').is(':checked'),
                id: $('#edit-id').val()
            }
        }
        console.log('edit todo request:', todoItem)
        $.ajax({
            url: uri + '/edittodo',
            type: 'POST',
            accepts: 'application/json',
            contentType: 'application/json',
            data: JSON.stringify(todoItem),
            error: function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown);
            },
            success: function (result) {
                console.log("edit todo response:", result)
                getData();
            }
        })

        closeEditBox()
    })

    // 关闭编辑框按钮 事件监听
    $('#close-edit-box').on('click', function () {
        closeEditBox()
    })

    // 提交修改待办 未使用
    $('.my-form').on('submit', function () {
        const todoItem = {
            user: getSignUser(),
            model: {
                name: $('#edit-name').val(),
                isComplete: $('#edit-isComplete').is(':checked'),
                id: $('#edit-id').val()
            }
        }

        $.ajax({
            url: uri + '/edittodo',
            type: 'POST',
            accepts: 'application/json',
            contentType: 'application/json',
            data: JSON.stringify(todoItem),
            error: function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown);
            },
            success: function (result) {
                console.log("response:", result)
                getData();
            }
        });

        closeEditBox();
        return false;
    });
}

// 获取所有待办数据并显示在屏幕上
function getData() {
    const all = {
        user: getSignUser(),
        PageIndex: 0,
        PageSize: 10,
        FlowType: 0
    }
    console.log("all todos request:", all);
    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: uri + "/all",
        contentType: 'application/json',
        data: JSON.stringify(all),
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        },
        success: function (data) {
            console.log("all todos respone: ", data);
            $('#todos').empty();
            if (data.code != 0) {
                alert(data.message);
                return;
            }
            let items = data.extension
            getCount(items.length)
            $.each(items, function (key, item) {
                const checked = item.isComplete ? 'checked' : ''

                $('<tr><td><input disabled="true" type="checkbox" ' + checked + '></td>' +
                    '<td>' + item.name + '</td>' +
                    '<td><button class="item-edit">编辑</button></td>' +
                    '<td><button class="item-delete">删除</button></td>' +
                    '</tr>').appendTo($('#todos'));
            });
            // 绑定事件
            $('.item-edit').on("click", function () {
                editItem(this);
            })
            $('.item-delete').on("click", function () {
                deleteItem(this);
            })
            todos = items
        }
    })
}

// 添加待办
function addItem() {
    // 请求体构造
    // 对待办名称进行正则表达式输入校验
    const todoItemName = $('#add-name').val();
    const user = getSignUser();
    const item = {
        user: user,
        model: {
            "name": todoItemName
        }
    };
    console.log("add item request: ", item)
    // 发送请求
    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: uri,
        contentType: 'application/json',
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        },
        success: function (result) {
            console.log("add item response: ", result);
            getData();
            $('#add-name').val('');
        }
    });
}

// 删除待办
function deleteItem(eleItem) {
    const index = $(eleItem).parent().parent().index()  // 找到表行所在的节点
    const id = todos[index].id
    const request = {
        user: getSignUser(),
        model: {
            id: id
        }
    }
    console.log('delete todo request:', request)
    $.ajax({
        url: uri,  // 可以通过加密的方式将数据用url携带
        type: 'DELETE',
        accepts: 'application/json',
        url: uri,
        contentType: 'application/json',
        data: JSON.stringify(request),
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        },
        success: function (result) {
            console.log('delete response:', result)
            getData();
        }
    });
}

// 编辑待办，将被编辑的待办数据传到编辑框中
function editItem(eleItem) {
    let index = $(eleItem).parent().parent().index()  // 找到表行所在的节点
    console.log("click todo item, index: " + index + ", id: ", todos[index].id)
    let todoitem = todos[index]
    $('#edit-name').val(todoitem.name);
    $('#edit-id').val(todoitem.id);
    $('#edit-isComplete')[0].checked = todoitem.isComplete;
    $('#spoiler').css({ 'display': 'block' });
}

// 关闭编辑框，在页面中隐藏
function closeEditBox() {
    $('#spoiler').css({ 'display': 'none' });
}