using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Data_Access.Repository
{
   public interface IspCall
    {
        void excecute(string procdureName, DynamicParameters param = null);
        T single<T>(string procdureName, DynamicParameters param = null);
        T oneRecord<T>(string procdureName, DynamicParameters param = null);
        IEnumerable<T> List<T>(string procdureName, DynamicParameters param = null);
        Tuple <IEnumerable<T1>, IEnumerable<T2>> List<T1,T2>(string procdureName, DynamicParameters param = null);
        
    }
}
