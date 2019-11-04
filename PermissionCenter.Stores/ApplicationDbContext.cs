using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PermissionCenter.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PermissionCenter.Stores
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// 登陆日志
	    /// from "login_log" table, "ws_unified_subject" database.
        /// </summary>
        public DbSet<LoginLog> LoginLogs { get; set; }
        /// <summary>
        /// 操作日志
	    /// from "operate_log" table, "ws_unified_subject" database.
        /// </summary>
        public DbSet<OperateLog> OperateLogs { get; set; }
        /// <summary>
        /// 权限
	    /// from "permission" table, "ws_unified_subject" database.
        /// </summary>
        public DbSet<Permission> Permissions { get; set; }
        /// <summary>
        /// 权限关联表
	    /// from "permission_relation" table, "ws_unified_subject" database.
        /// </summary>
        public DbSet<PermissionRelation> PermissionRelations { get; set; }
        /// <summary>
        /// 主体
	    /// from "subject" table, "ws_unified_subject" database.
        /// </summary>
        public DbSet<Subject> Subjects { get; set; }
        /// <summary>
        /// 主体权限扩展表
		/// 将主体自关联表和权限自关联表进行扩展，让每一个主体和对应的每一个权限关联
	    /// from "subject_permission_expansion" table, "ws_unified_subject" database.
        /// </summary>
        public DbSet<SubjectPermissionExpansion> SubjectPermissionExpansions { get; set; }
        /// <summary>
        /// 主体权限关联表
	    /// from "subject_permission_relation" table, "ws_unified_subject" database.
        /// </summary>
        public DbSet<SubjectPermissionRelation> SubjectPermissionRelations { get; set; }
        /// <summary>
        /// 主体资源表
	    /// from "subject_resource" table, "ws_unified_subject" database.
        /// </summary>
        public DbSet<SubjectResource> SubjectResources { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<LoginLog>(b =>
            {
                b.HasKey(k => new { k.Id });
                b.Property(e => e.Id).IsRequired().HasMaxLength(36);
                b.Property(e => e.LoginIp).HasMaxLength(255);
                b.Property(e => e.LoginTime).IsRequired();
                b.Property(e => e.SubjectId).IsRequired().HasMaxLength(36);
                b.Property(e => e.SubjectName).HasMaxLength(255);
            });
            builder.Entity<OperateLog>(b =>
            {
                b.HasKey(k => new { k.Id });
                b.Property(e => e.Id).IsRequired().HasMaxLength(36);
                b.Property(e => e.OperDesc);
                b.Property(e => e.OperId).HasMaxLength(36);
                b.Property(e => e.OperTime).IsRequired();
                b.Property(e => e.OperType);
                b.Property(e => e.SubjectId).HasMaxLength(36);
            });
            builder.Entity<Permission>(b =>
            {
                b.HasKey(k => new { k.Id });
                b.Property(e => e.Code).IsRequired().HasMaxLength(255);
                b.Property(e => e.Desc).HasMaxLength(255);
                b.Property(e => e.Id).IsRequired().HasMaxLength(36);
                b.Property(e => e.Name).HasMaxLength(255);
            });
            builder.Entity<PermissionRelation>(b =>
            {
                b.HasKey(k => new { k.Id });
                b.Property(e => e.ChildId).IsRequired().HasMaxLength(36);
                b.Property(e => e.Id).IsRequired().HasMaxLength(36);
                b.Property(e => e.IsDirect).IsRequired();
                b.Property(e => e.ParentId).IsRequired().HasMaxLength(36);
            });
            builder.Entity<Subject>(b =>
            {
                b.HasKey(k => new { k.Id });
                b.Property(e => e.AllowLogin);
                b.Property(e => e.CreateTime).IsRequired();
                b.Property(e => e.DeleteTime);
                b.Property(e => e.Email).HasMaxLength(511);
                b.Property(e => e.EmailConfirmed);
                b.Property(e => e.Id).IsRequired().HasMaxLength(36);
                b.Property(e => e.IsDeleted).IsRequired();
                b.Property(e => e.Password).HasMaxLength(511);
                b.Property(e => e.Phone).HasMaxLength(31);
                b.Property(e => e.PhoneConfirmed);
                b.Property(e => e.UpdateTime).IsRequired();
                b.Property(e => e.UserName).IsRequired().HasMaxLength(255);
                b.Property(e => e.WXOpenId).HasMaxLength(127);
            });
            builder.Entity<SubjectPermissionExpansion>(b =>
            {
                b.HasKey(k => new { k.PermissionId, k.SubjectId });
                b.Property(e => e.PermissionId).IsRequired().HasMaxLength(36);
                b.Property(e => e.SubjectId).IsRequired().HasMaxLength(36);
            });
            builder.Entity<SubjectPermissionRelation>(b =>
            {
                b.HasKey(k => new { k.Id });
                b.Property(e => e.Id).IsRequired().HasMaxLength(36);
                b.Property(e => e.PermissionId).IsRequired().HasMaxLength(36);
                b.Property(e => e.SubjectId).IsRequired().HasMaxLength(36);
            });
            builder.Entity<SubjectResource>(b =>
            {
                b.HasKey(k => new { k.Key, k.SubjectId });
                b.Property(e => e.Key).IsRequired().HasMaxLength(255);
                b.Property(e => e.SubjectId).IsRequired().HasMaxLength(36);
                b.Property(e => e.Value).HasMaxLength(255);
            });
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {

        }
    }
}
