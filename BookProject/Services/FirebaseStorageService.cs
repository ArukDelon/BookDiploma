using Google.Cloud.Storage.V1;
using System.Text;

namespace BookProject.Services
{
    public class FirebaseStorageService
    {
        private readonly StorageClient _storageClient;
        private const string BucketName = "bookproject-4eb34.appspot.com";

        public FirebaseStorageService(StorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<Uri> UploadFile(string name, IFormFile file)
        {
            var randomGuid = Guid.NewGuid();
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var blob = await _storageClient.UploadObjectAsync(BucketName,
                $"{randomGuid}", file.ContentType, stream);

            await _storageClient.UpdateObjectAsync(new Google.Apis.Storage.v1.Data.Object
            {
                Bucket = BucketName,
                Name = blob.Name,
                ContentType = blob.ContentType,
                Acl = new List<Google.Apis.Storage.v1.Data.ObjectAccessControl>
            {
                new Google.Apis.Storage.v1.Data.ObjectAccessControl
                {
                    Entity = "allUsers",
                    Role = "READER"
                }
            }
            });

            var publicUrl = $"https://storage.googleapis.com/{BucketName}/{blob.Name}";

            return new Uri(publicUrl);
        }

        private string GetFileExtension(string contentType)
        {
            return contentType switch
            {
                "image/jpeg" => ".jpeg",
                "image/png" => ".png",
                "image/gif" => ".gif",
                "text/plain" => ".txt",
                _ => string.Empty,
            };
        }

        public async Task<string> GetFileContentFromUrlAsync(string fileUrl)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync(fileUrl);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve file content: {ex.Message}");
            }
        }
    }
}
