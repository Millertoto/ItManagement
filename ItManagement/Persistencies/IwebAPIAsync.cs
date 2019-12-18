using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Persistencies
{
    public interface IwebAPIAsync<T>
    {
        // Skrevet af David

        Task<List<T>> Load();
        Task Create(T obj);
        Task<T> Read(int key);
        Task Update(int key, T obj);
        Task Delete(int key);
    }
}
