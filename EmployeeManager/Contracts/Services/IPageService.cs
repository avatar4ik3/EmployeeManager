using System;

namespace EmployeeManager.Contracts.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);
    }
}
