/**
 * 登陆
 * @returns
 */
function signin_btn() {
    // TODO: 这里需要进行表格输入格式验证
    $.ajax({
        type: "post", /* 提交方式 */
        url: "SigninServlet", /* servlet 或者 url */
        data: signin_reqdata(),
        contentType: "application/json;charset=utf-8", /* 采用json格式数据 */
        dateType: "json", /* 数据类型为json */
        success: function (result) { /* 请求成功时被调用，result为服务器传来的数据 */
            /* 页面数据初始化 */
            let data = eval(result);  // 将json转换为jso
            if (data && data.bbs_res_code == 0) {
                let body =data.bbs_res_body;
                if (body.ret_code == 0) {
                	console.log("登录成功");
                    alert("登陆成功");
                    // TODO: 跳转到其他界面，是否由服务器端跳转？
                    //window.location.href = "/";
                } else if (body.ret_code == 1) {
                	console.log("用户名不存在");
                    alert("用户名不存在");
                } else if (body.ret_code == 2) {
                	console.log("密码错误");
                    alert("密码错误");
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) { /* 请求错误时调用，errorThrown为错误数据 */
            alert("失败: " + errorThrown);
        }
    });
}
// 从表单中获取数据并生成请求jso
function signin_reqdata() {
	return JSON.stringify({
        "req_type": "signin",
        "timestamp": new Date().getTime(),
        "username": $("#username").val(),
        "password": $("#password").val()
    });
}

// net.js
function post(bundle) {  // post({data: data, successfn: successfn});
    $.ajax({
        type: "post", /* 提交方式 */
        url: "data", /* servlet 或者 url index.jsp */
        data: bundle.jso ? JSON.stringify(bundle.jso) : bundle.json ? bundle.json : "",
        contentType: "application/json;charset=utf-8", /* 采用json格式数据 */
        dateType: "json", /* 数据类型为json */
        success: bundle.successfn ? bundle.successfn : function (result) {
            console.log("成功访问");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) { /* 请求错误时调用，errorThrown为错误数据 */
            alert("失败: " + errorThrown);
        }
    });
}
// 返回登录用户的信息
function signin_user_info() {
    let reqdata =JSON.stringify({
      "timestamp": new Date().getTime(),
      "req_type": "signin_user_info"
    });
    $.ajax({
          type : "post", /* 提交方式 */
          url : "data", /* servlet 或者 url index.jsp */
          data : reqdata, 
          contentType : "application/json;charset=utf-8", /* 采用json格式数据 */
          dateType : "json", /* 数据类型为json */
          success : function(result) { /* 请求成功时被调用，result为服务器传来的数据 */
              /* 页面数据初始化 */
              let data =eval(result);
              if (data&&data.bbs_res_code==0&&data.bbs_res_body.ret_code==0){
                  let ret_body =data.bbs_res_body.ret_body;
          console.log(ret_body);
          return ret_body;
              }
          },
          error : function(XMLHttpRequest, textStatus, errorThrown) { /* 请求错误时调用，errorThrown为错误数据 */
              alert("失败"+errorThrown);
          }
      });
  }
// 返回用户的信息
function user_info(id) {
    let reqdata =JSON.stringify({
      "timestamp": new Date().getTime(),
      "req_type": "user_info",
      "usr_id": id
    });
    $.ajax({
        type : "post", /* 提交方式 */
        url : "data", /* servlet 或者 url index.jsp */
        data : reqdata, 
        contentType : "application/json;charset=utf-8", /* 采用json格式数据 */
        dateType : "json", /* 数据类型为json */
        success : function(result) { /* 请求成功时被调用，result为服务器传来的数据 */
            /* 页面数据初始化 */
            let data =eval(result);
            if (data&&data.bbs_res_code==0&&data.bbs_res_body.ret_code==0){
                let ret_body =data.bbs_res_body.ret_body;
                    console.log(ret_body);
                    return ret_body;
                }
            },
        error : function(XMLHttpRequest, textStatus, errorThrown) { /* 请求错误时调用，errorThrown为错误数据 */
            alert("失败"+errorThrown);
        }
    });
}
// 解析url成jso
function parseQueryString(url){
    var str = url.split('?')[1], str2 = str.split('#')[0],
        items = str2.split('&'),
        result = {},
        res = {};
    for(var i = 0, len = items.length; i <len; ++i){
    	res = items[i].split("=");
    	result[res[0]] = res[1];
    }
    return result;
}
//表单重置
function reset_btn() {
	document.getElementById("form").reset()
}

var localData ={
	// 保存存储对象 key: String, val: Object
	save: function (key, val) {
		if(!sessionStorage.data){
			sessionStorage.data ='{}';
		}
		let data = JSON.parse(sessionStorage.data);
		data[key] =val;
		sessionStorage.data =JSON.stringify(data);
	},
	// 加载存储对象
	load: function(key) {
		if (sessionStorage.data) {
			console.log(sessionStorage.data);
			return JSON.parse(sessionStorage.data)[key];
		}
	}
}
// 页面滚动,不增加历史
function replaceHash(hash) {
	return location.replace(
		'#' + hash.replace(/^#/, '')
	);
}