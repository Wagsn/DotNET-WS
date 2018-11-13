using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WS.Core.Stores;
using WS.Music.Dto;
using WS.Music.Models;

namespace WS.Music.Stores
{
    /// <summary>
    /// User Store
    /// </summary>
    public class UserStore : StoreBase<ApplicationDbContext, User>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UserStore(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// List for User Model
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<List<TResult>> ListAsync<TResult>(Func<IQueryable<User>, IQueryable<TResult>> query, CancellationToken cancellationToken =default(CancellationToken))
        {
            CheckNull(query);

            return query.Invoke(Context.Users.Where(i => i._IsDeleted == false).AsNoTracking()).ToListAsync(cancellationToken);
        }
        
        /// <summary>
        /// 通过GUID查询用户
        /// </summary>
        /// <param name="userId">用户GUID</param>
        /// <returns></returns>
        public IQueryable<User> ById([Required][MaxLength(36)]string userId)
        {
            var query = from u in Context.Users
                        where u.Id == userId && !u._IsDeleted
                        select new User(u);
            return query;
        }

        /// <summary>
        /// 通过用户名精确查找用户
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        public IQueryable<User> ByName([Required][MaxLength(31)]string name)
        {
            var query = from u in Context.Users
                        where u.Name == name
                        select new User(u);
            return query;
        }

        /// <summary>
        /// 通过用户名模糊查询用户：只做了子串判断，TODO：以后加上字符统计判断和近似词判断，或者引入第三方库
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        public IQueryable<User> LikeName([Required][MaxLength(31)]string name)
        {
            var query = from u in Context.Users
                        where u.Name.Contains(name)
                        select new User(u);
            return query;
        }

        /// <summary>
        /// 用户名正则表达式 中文英文下划线
        /// </summary>
        public static readonly string userNameRegex = @"^[\u4E00-\u9FA5A-Za-z_]+$";

        /// <summary>
        /// 电子邮箱正则表达式，简要版
        /// </summary>
        public static readonly string emailRegex = @"^([\w-\.]+)@([\w-\.]+)(\.[a-zA-Z0-9]+)$";
        //邮箱名以数字或字母开头；邮箱名可由字母、数字、点号、减号、下划线组成；邮箱名（@前的字符）长度为3～18个字符；邮箱名不能以点号、减号或下划线结尾；不能出现连续两个或两个以上的点号、减号。  
        //string pattern = @"^[a-zA-Z0-9]((?<!(\.\.|--))[a-zA-Z0-9\._-]){1,16}[a-zA-Z0-9]@([0-9a-zA-Z][0-9a-zA-Z-]{0,62}\.)+([0-9a-zA-Z][0-9a-zA-Z-]{0,62})\.?|((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$"; 

        /// <summary>
        /// 手机号码正则表达式
        /// </summary>
        public static readonly string phoneNumberRegex = @"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$";

        /// <summary>
        /// 通过用户名|邮箱|电话号码和密码查询用户
        /// </summary>
        /// <param name="name">用户名|邮箱|电话号码</param>
        /// <param name="pwd">用户密码</param>
        /// <returns></returns>
        public IQueryable<User> Find([Required][MaxLength(320)]string name, [Required][MaxLength(63)]string pwd)
        {
            IQueryable<User> query= null;
            if (Regex.IsMatch(name, userNameRegex))
            {
                query = from u in Context.Users
                        where u.Name == name && u.Pwd == pwd
                        select new User(u);
            }
            else if (Regex.IsMatch(name, emailRegex))
            {
                query = from u in Context.Users
                        where u.Email == name && u.Pwd == pwd
                        select new User(u);
            }
            else if (Regex.IsMatch(name, phoneNumberRegex))
            {
                throw new NotSupportedException("WS------ Sorry, not support telephone number login.");
                //query = from u in Context.Users
                //        where u.Phone == name && u.Pwd == pwd
                //        select new User(u);
            }
            return query;
        }

        /// <summary>
        /// 通过电子邮箱查询用户
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IQueryable<User> ByEmail(string email)
        {
            var query = from u in Context.Users
                        where u.Email == email
                        select new User(u);
            return query;
        }
    }
}
