using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PermissionCenter.Dto;

namespace PermissionCenter.Controllers
{
    /// <summary>
    /// 主体控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/subject")]
    public class SubjectController : Controller
    {
        public SubjectController(PermissionCenter.Stores.ISubjectStore subjectStore)
        {
            _subjectStore = subjectStore ?? throw new ArgumentNullException(nameof(subjectStore));
        }

        private Stores.ISubjectStore _subjectStore { get; }

        [HttpPost("search")]
        public async Task<PagingResponseMessage<SubjectResponse>> Search([FromBody]SearchRequest request)
        {
            var resposne = new PagingResponseMessage<SubjectResponse>();
            var query = _subjectStore.Find(subject => subject.IsDeleted == false);

            //var queryData = query.Select(s => s.ToDictionary());
            if(request == null)
            {
                resposne.Code = "200";
                resposne.Message = "请求不能为空";
                return resposne;
            }
            if (request.Keyword != null)
            {
                var keyword = request.Keyword;
                query = query.Where(subject => subject.Email.Contains(keyword, StringComparison.CurrentCulture) || subject.UserName.Contains(keyword, StringComparison.CurrentCulture) || subject.Phone.Contains(keyword, StringComparison.CurrentCulture));
            }
            var queryData = query.OrderByDescending(subject => subject.CreateTime).Skip(request.PageIndex * request.PageSize).Take(request.PageSize);
            resposne = await resposne.WrapData(query, subject => new SubjectResponse 
            {

            }, request.PageIndex, request.PageSize, HttpContext.RequestAborted);
            return resposne;
        }
    }
}
