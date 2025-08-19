namespace Internship_Aleksa.Data.FileStorage;

public class FileStorageOptions
{
    public bool UseFileStorage { get; set; } = true;
    public string BasePath { get; set; } = "App_Data";
    public bool PrettyPrint { get; set; } = true;
}