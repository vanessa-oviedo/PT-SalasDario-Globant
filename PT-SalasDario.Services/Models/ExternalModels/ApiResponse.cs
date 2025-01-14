namespace PT_SalasDario.Services.Models.ExternalModels
{
    public class ApiResponse<T>
    {
        public Info Info { get; set; }
        public List<T> Results { get; set; }
    }
}
