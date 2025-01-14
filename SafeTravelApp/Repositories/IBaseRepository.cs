using System.Linq.Expressions;

namespace SafeTravelApp.Repositories
{
    public interface IBaseRepository<T, TKey> where T : class
    {
        Task AddAsync(T entity); // Προσθήκη εγγραφής
        Task AddRangeAsync(IEnumerable<T> entities); // Προσθήκη πολλαπλών εγγραφών

        Task UpdateAsync(T entity); // Ενημέρωση εγγραφής
        Task<bool> DeleteAsync(TKey id); // Διαγραφή εγγραφής βάσει πρωτεύοντος κλειδιού
                
        Task<T?> GetByIdAsync(TKey id); // Λήψη εγγραφής βάσει πρωτεύοντος κλειδιού
        Task<IEnumerable<T>> GetAllAsync(); // Λήψη όλων των εγγραφών
        Task<IEnumerable<T>?> FindFilteredAsync(List<Func<T, bool>> predicates); // Εύρεση βάσει συνθήκης
        
        Task<int> GetCountAsync(); // Λήψη αριθμού εγγραφών
    }
}
