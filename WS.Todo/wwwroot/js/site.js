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

// 当文档加载完之后获取待办数据
$(document).ready(function () {
    eventInit();  // 初始化，事件监听
});

// 点击事件监听初始化，等页面加载完
function eventInit() {
    // 刷新按钮点击事件监听
    $("#refresh").on("click", function () {
        getData();
    })
    // 提交修改待办
    $('#edit-submit').on('click', function () {
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
        });

        closeEditBox();
    })

    // 提交修改待办
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
        return true;
    });
}

// 获取所有待办数据并显示在屏幕上
function getData() {
    var all = {
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
            let items = data.extension;
            getCount(items.length);
            $.each(items, function (key, item) {
                const checked = item.isComplete ? 'checked' : '';

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
            todos = items;
        }
    });
}

// 添加待办
function addItem() {
    // 对待办名称进行正则表达式输入校验
    let todoItemName = $('#add-name').val();
    let user = getSignUser();
    const item = {
        user: user,
        model: {
            "name": todoItemName
        }
    };
    console.log("add item request: ", item)
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
    let index = $(eleItem).parent().parent().index()  // 找到表行所在的节点
    let id = todos[index].id
    let item = {
        user: getSignUser(),
        model: {
            id: id
        }
    }
    $.ajax({
        url: uri,  // 可以通过加密的方式将数据用url携带
        type: 'DELETE',
        accepts: 'application/json',
        url: uri,
        contentType: 'application/json',
        data: JSON.stringify(item),
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
    console.log("index: " + index + ", todo id: ", todos[index].id)
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