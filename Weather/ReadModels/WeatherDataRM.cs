namespace Weather.ReadModels
{
    //A 'record' is like a 'class' except its IMMUTABLE after initialisation
    public record WeatherDataRM(
        Chart chart,
        List<Category> categories,
        List<DataSet> dataSet
        );
}
