namespace ${{ values.fileNamePrefix }}.Queries;

public class MeasurementQueries
{
    private readonly IMeasurementRepository _repository;

    public MeasurementQueries(IMeasurementRepository repository)
    {
        _repository = repository;
    }

    public async Task<Measurement> GetMeasurementAsync(Guid id)
    {
        return await _repository.FindAsync(id);
    }
}

