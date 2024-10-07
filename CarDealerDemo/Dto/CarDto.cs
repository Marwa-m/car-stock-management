namespace CarDealerDemo.Dto
{
    public sealed record AddCarDto(string Make, string Model, int Year, int StockLevel);
    public sealed record UpdateStockLevelDto(int ID, int StockLevel);
    public sealed record SearchCarByMakeAdModelDto(string Make = "", string Model = "");
}
