using System.Collections.Generic;

namespace Projektas_Irankiai.Controllers
{
    public interface IDataBase
    {
        List<string[]> selectData(string query);
        List<int> selectInts(string query);

        void deleteData(string query);
        void editData(string query);
        void insertData(string query);
    }
}