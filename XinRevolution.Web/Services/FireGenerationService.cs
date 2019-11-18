using Microsoft.EntityFrameworkCore;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Web.Services
{
    public class FireGenerationService
    {
        private readonly IUnitOfWork<DbContext> _unitOfWork;

        public FireGenerationService(IUnitOfWork<DbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
