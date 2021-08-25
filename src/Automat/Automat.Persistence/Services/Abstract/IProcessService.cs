using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Persistence.Services.Abstract
{
    public interface IProcessService
    {
        Guid GenerateProcessId();
    }
}
