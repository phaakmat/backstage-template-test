namespace ${{ values.namespacePrefix }}.Infrastructure.UnitTests;

public class MeasurementsTheoryData : TheoryData<string, double, string>
{
    public MeasurementsTheoryData()
    {
        Add("2080b762-05e1-4f0d-b443-6396f7d9ac6d", 15.4, "Summary 1");
        Add("2f9dd80b-daf8-4d0f-b399-13433987d363", 20.1, "Summary 2");
        Add("a00b11c8-c2e6-4b3e-9aaf-ec390910ac57", 16.8, "Summary 3");
    }
}