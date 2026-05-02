using helpdesk.DTOs;
using helpdesk.Helper;

namespace helpdesk.Application;

public class FileUploader(ILogger<FileUploader> logger_)
{

    private readonly ILogger<FileUploader> logger = logger_;

    public async Task<FileMetaData> Upload(IFormFile? file)
    {

        try
        {
            string FilesFolder = Path.Combine(Directory.GetCurrentDirectory(), "FilesUploaded");
            string DocumentFolder = Path.Combine(FilesFolder, "Documents");
            string ImageFolder = Path.Combine(FilesFolder, "Images");
            Directory.CreateDirectory(DocumentFolder);
            Directory.CreateDirectory(ImageFolder);
            string ImageSafePathName = Path.Combine(ImageFolder, UniqueID.GenerateUID() + file!.FileName);
            string DocumentsSafePathName = Path.Combine(DocumentFolder, UniqueID.GenerateUID() + file.FileName);

            if (file.ContentType == "image/png" || file.ContentType == "image/jpeg")
            {
                using var ImageStream = new FileStream(ImageSafePathName, FileMode.Create);
                await file.CopyToAsync(ImageStream);
                return new FileMetaData(UniqueID.GenerateUID(), file.FileName, DocumentsSafePathName);
            }
            using var DocStream = new FileStream(DocumentsSafePathName, FileMode.Create);
            await file.CopyToAsync(DocStream);
            logger.LogInformation(" New file uploaded {file}", file);
            return new FileMetaData(UniqueID.GenerateUID(), file.FileName, DocumentsSafePathName);
        }
        catch (System.Exception)
        {

            logger.LogError("Uploading failed");
            throw;

        }


    }
}