using PermissionCenter.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionCenter.Stores
{
    public class SubjectStore : Store<Subject, ApplicationDbContext>, ISubjectStore
    {
        public SubjectStore(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
