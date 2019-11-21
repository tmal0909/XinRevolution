using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using XinRevolution.Database.Entity;
using XinRevolution.Repository.Interface;
using XinRevolution.Web.Model;
using XinRevolution.Web.ViewModels;

namespace XinRevolution.Web.Services
{
    public class BlogService
    {
        private readonly IUnitOfWork<DbContext> _unitOfWork;

        public BlogService(IUnitOfWork<DbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ServiceResultModel<BlogViewModel> Find()
        {
            var result = new ServiceResultModel<BlogViewModel>();

            try
            {
                var blogs = _unitOfWork.GetRepository<BlogEntity>()
                    .GetAll(new Expression<Func<BlogEntity, object>>[] {
                        x => x.BlogPosts,
                        x => x.BlogTags,
                    })
                    .OrderByDescending(x => x.ReleaseDate);

                var tags = _unitOfWork.GetRepository<TagEntity>().GetAll(x => x.Status);

                result.Data.Blogs = blogs;
                result.Data.Tags = tags;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"查詢失敗，{ex.Message}";
            }

            return result;
        }
    }
}
