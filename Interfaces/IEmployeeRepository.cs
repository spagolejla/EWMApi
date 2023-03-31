﻿using EWMApi.Model;

namespace EWMApi.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();

        Task<Employee> Get(string id);

        System.Threading.Tasks.Task Post(Employee item);

        Task<bool> Update(string id, Employee item);

        Task<bool> Remove(string id);

        Task<bool> RemoveAll();
    }
}
