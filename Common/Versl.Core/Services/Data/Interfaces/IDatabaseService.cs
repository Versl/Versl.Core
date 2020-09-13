using System;
namespace Versl.Services.Data
{
    public interface IDatabaseService<M> : IDataService<M>
    {
        string BasePath { get; }
    }
}
