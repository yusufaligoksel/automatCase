using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.Persistence.Services.Abstract;

namespace Automat.Persistence.Services.Concrete
{
    public class ProcessService: IProcessService
    {
        public Guid GenerateProcessId()
        {
            return Guid.NewGuid();
        }
    }
}
