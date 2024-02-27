using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Persistence.DataTransferObject;

namespace Persistence.Repositories;

public class SpectrumCardRepository : ISpectrumCardRepository
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public SpectrumCardRepository(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<List<SpectrumCardDTO>> GetAllCards()
    {
        return await context.SpectrumCards
            .ProjectTo<SpectrumCardDTO>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
}