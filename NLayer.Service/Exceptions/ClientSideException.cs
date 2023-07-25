using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Exceptions
{
    public class ClientSideException : Exception
    {
        public ClientSideException(string message) : base(message) 
        {
            //Kullanıcıdan kaynaklı hata mı yoksa kendimizin fırlattığı bir hata mı onu ayırt edebilmek için burayı kurduk.
        }

    }
}
