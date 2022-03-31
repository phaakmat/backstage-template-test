namespace ${{ values.fileNamePrefix }}.Domain;

public interface IMeasurementRepository
{
    Measurement Add(Measurement measurement);
    Task<Measurement> FindAsync(Guid id);
}
