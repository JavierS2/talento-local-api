using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using TalentoLocal.Services.Interfaces;

namespace TalentoLocal.Services.Implementations
{
    public class AzureBlobStorageService : IBlobStorageService
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        public AzureBlobStorageService(IConfiguration config)
        {
            _connectionString = config["AzureBlobStorage:ConnectionString"]
                ?? throw new ArgumentNullException(nameof(_connectionString));

            _containerName = config["AzureBlobStorage:ContainerName"]
                ?? throw new ArgumentNullException(nameof(_containerName));
        }

        // SUBIR ARCHIVO Y RETORNAR SOLO EL NOMBRE DEL BLOB
        public async Task<string> UploadAsync(IFormFile file)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            await containerClient.CreateIfNotExistsAsync();

            string blobName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true);

            return blobName; // << SOLO EL NOMBRE DEL ARCHIVO
        }

        // GENERAR URL SAS TEMPORAL (LECTURA)
        public string GenerateSasUrl(string blobName, int minutes = 20)
        {
            var blobClient = new BlobClient(_connectionString, _containerName, blobName);

            if (!blobClient.CanGenerateSasUri)
                throw new InvalidOperationException("No se puede generar SAS con esta configuración.");

            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = _containerName,
                BlobName = blobName,
                Resource = "b",
                ExpiresOn = DateTime.UtcNow.AddMinutes(minutes)
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            // Forzar previsualización en el navegador
            sasBuilder.ContentType = "application/pdf";
            sasBuilder.ContentDisposition = "inline";

            Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
            return sasUri.ToString();
        }


        // OBTENER SOLO EL NOMBRE DEL BLOB DESDE UNA URL (si lo necesitas)
        public string GetBlobNameFromUrl(string url)
        {
            return Path.GetFileName(new Uri(url).AbsolutePath);
        }
    }
}
