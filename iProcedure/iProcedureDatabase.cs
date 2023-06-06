using iProcedure.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iProcedure
{
    public class iProcedureDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public iProcedureDatabase(string dbPath)
        {
            //File.Delete(dbPath);
            bool bExistDB = File.Exists(dbPath);

            _database = new SQLiteAsyncConnection(dbPath);

            //if (!bExistDB)
            {
                try
                {
                    _database.CreateTableAsync<StepBOMItem>().Wait();
                    _database.CreateTableAsync<Unit>().Wait();
                }
                catch(Exception e)
                {
                    int i = 0;
                }
                //_database.CreateTableAsync<StepBOMItem>().Wait();
            }
        }

        public Task<Unit> GetUnitDataAsync()
        {
            return _database.Table<Unit>().FirstOrDefaultAsync();
        }

        public Task<Unit[]> GetAllUnitDataAsync()
        {
            return _database.Table<Unit>().ToArrayAsync();
        }

        public Task<int> SaveUnitDataAsync(Unit unitData)
        {
            return _database.InsertAsync(unitData);
        }

        public Task<StepBOMItem[]> GetAllStepBOMItemDataAsync()
        {
            return _database.Table<StepBOMItem>().ToArrayAsync();
        }

        public Task<int> SaveStepBOMItemDataAsync(StepBOMItem stepBOMItemData)
        {
            return _database.InsertAsync(stepBOMItemData);
        }

        public Task<int> UpdateStepBOMItemDataAsync(StepBOMItem stepBOMItemData)
        {
            return _database.UpdateAsync(stepBOMItemData);
        }

        public Task<int> RemoveStepBOMItemDataAsync(StepBOMItem stepBOMItemData)
        {
            return _database.DeleteAsync(stepBOMItemData);
        }
    }
}
