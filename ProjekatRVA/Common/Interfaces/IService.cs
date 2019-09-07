using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        BindingList<Theater> GetAllTheaters();
        [OperationContract]
        BindingList<Auditorium> GetAllAuditoriums(string theater);
    }
}
