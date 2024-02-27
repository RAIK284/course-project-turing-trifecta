using Domain;
using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public interface ISpectrumCardRepository
{
    public Task<List<SpectrumCardDTO>> GetAllCards();
}