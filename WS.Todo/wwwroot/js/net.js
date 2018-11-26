
function post(bundle) {
    $.ajax({
        type: "post", /* 提交方式 */
        url: "data", /* servlet 或者 url index.jsp */
        data: jso ? JSON.stringify(jso) : json ? json : "",
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