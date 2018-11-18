// sign.js 用以登入注册

// 登入
function signin() {
    let req = {
        name: $('#username'),
        pwd: $('#password')
    }
    $.ajax({
        type: 'post',
        url: 'api/sign/in',
        data: '',
        contentType: 'application/json',
        success: function (resbody) {
            console.log('response: ' + resbody);
        }
    });
}

// 注册
function signup() {
    let req = {
        name: $('#username'),
        pwd: $('#password'),
        mail: $('#email')
    }
    $.ajax({
        type: 'post',
        url: 'api/sign/up',
        data: '',
        contentType: 'application/json',
        success: function (resbody) {
            console.log('response: ' + resbody);
        }
    });
}

// 登出
function signout() {
    $.ajax({
        type: 'post',
        url: 'api/sign/out',
        data: '',
        contentType: 'application/json',
        success: function (resbody) {
            console.log('response: ' + resbody);
        }
    });
}

// Ajax
function ajaxtemplete() {
    $.ajax({
        type: 'post',
        url: '',
        data: '',
        contentType: 'application/json',
        success: function (resbody) {
            console.log('response: ' + resbody);
        }
    });
}