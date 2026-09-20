namespace Districts.Wpf.Models;

public record DistrictDetailsModel(
    int Id,
    string Name,
    SalespersonModel PrimarySalesperson,
    List<SalespersonModel> SecondarySalespersons,
    List<StoreModel> Stores);