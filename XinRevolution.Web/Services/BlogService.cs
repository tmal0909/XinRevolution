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
                var tags = _unitOfWork.GetRepository<TagEntity>().GetAll(x => x.Status);
                var blogs = _unitOfWork.GetRepository<BlogEntity>().GetAll(
                    x => x.BlogPosts.Count > 0, 
                    x => x.Include(y => y.BlogPosts)
                          .Include(y => y.BlogTags)
                            .ThenInclude(y => y.Tag))
                    .ToList();
                                
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
