namespace helpdesk.DTOs;

public record class FileMetaData(
    string ID,
    string FileName,
    string PathName,
    string DownloadURL = ""
);