using System;
using System.Threading.Tasks;

namespace Info.Initializations
{
    public interface IInitialization
    {
        Task Execute();
    }
}
