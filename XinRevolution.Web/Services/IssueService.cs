using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XinRevolution.Database.Entity;
using XinRevolution.Repository.Interface;
using XinRevolution.Web.Model;
using XinRevolution.Web.ViewModels;

namespace XinRevolution.Web.Services
{
    public class IssueService
    {
        private readonly IUnitOfWork<DbContext> _unitOfWork;

        public IssueService(IUnitOfWork<DbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ServiceResultModel<IssueViewModel> Find()
        {
            var result = new ServiceResultModel<IssueViewModel>();

            try
            {
                var issues = _unitOfWork.GetRepository<IssueEntity>()
                    .GetAll(
                        x => x.IssueItems.Count() > 0, 
                        x => x.Include(y => y.IssueItems))
                    .OrderByDescending(x => x.IssueItems.Max(y => y.ReleaseDate))
                    .AsEnumerable();

                result.Data.Issues = issues;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"查詢失敗，{ex.Message}";
            }

            return result;
        }

        public ServiceResultModel<IssueDetailViewModel> FindDetail(int issueId)
        {
            var result = new ServiceResultModel<IssueDetailViewModel>();

            try
            {
                var issue = _unitOfWork.GetRepository<IssueEntity>()
                    .Single(x => x.Id == issueId, 
                            x => x.Include(y => y.IssueItems)
                                  .Include(y => y.IssueRelativeLinks));

                result.Data.Issue = issue;
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
