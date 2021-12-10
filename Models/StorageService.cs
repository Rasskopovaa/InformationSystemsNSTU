using lab1.Models;

namespace lab1.Storage
{
    public class StorageService
    {
        private readonly IStorage<Employee> _storage;

        public StorageService(IStorage<Employee> storage)
        {
            _storage = storage;
        }

        public string GetStorageType()
        {
            return _storage.StorageType;
        }

        public int GetNumberOfItems()
        {
            return _storage.All.Count;
        }
    }
}