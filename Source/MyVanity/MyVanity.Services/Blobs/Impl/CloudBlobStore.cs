using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.AzureStorage;
using Microsoft.Practices.TransientFaultHandling;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;

namespace MyVanity.Services.Blobs
{
    public class CloudBlobStore : IBlobStore
    {
        private readonly CloudBlobClient _client;
        private readonly RetryPolicy<StorageTransientErrorDetectionStrategy> _readPolicy;
        private readonly RetryPolicy<StorageTransientErrorDetectionStrategy> _writePolicy;

        public CloudBlobStore(CloudStorageAccount account)
        {
            _client = account.CreateCloudBlobClient();
            _client.RetryPolicy = new NoRetry();

            _readPolicy = new RetryPolicy<StorageTransientErrorDetectionStrategy>(new Incremental(1, TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
            _readPolicy.Retrying += (s, e) => Trace.TraceWarning("An error occurred in attempt number {1} to read from blob storage: {0}", e.LastException.Message, e.CurrentRetryCount);

            _writePolicy = new RetryPolicy<StorageTransientErrorDetectionStrategy>(new FixedInterval(1, TimeSpan.FromSeconds(10)) { FastFirstRetry = false });
            _writePolicy.Retrying += (s, e) => Trace.TraceWarning("An error occurred in attempt number {1} to write to blob storage: {0}", e.LastException.Message, e.CurrentRetryCount);
        }

        public Task<byte[]> FindAsync(string container, string path)
        {
            return
                GetContainerReferenceAsync(container)
                    .ContinueWith(
                        t =>
                        {
                            var containerRef = t.Result;
                            if (containerRef != null)
                            {
                                var blobReference = containerRef.GetBlockBlobReference(path);
                                
                                if (!blobReference.Exists())
                                    return null;

                                using (var memory = new MemoryStream())
                                {
                                    _readPolicy.ExecuteAction(() => blobReference.DownloadToStream(memory));
                                    return memory.ToArray();
                                }
                            }

                            return null;
                        });
        }

        public Task<FileWrapper> FindFileAsync(string container, string path)
        {
            return
                GetContainerReferenceAsync(container)
                    .ContinueWith(
                        t =>
                        {
                            var containerRef = t.Result;
                            if (containerRef != null)
                            {
                                var blobReference = containerRef.GetBlockBlobReference(path);

                                if (!blobReference.Exists())
                                    return null;

                                var memory = new MemoryStream();
                                _readPolicy.ExecuteAction(() => blobReference.DownloadToStream(memory));
                                memory.Seek(0, SeekOrigin.Begin);
                                    
                                return new FileWrapper
                                    {
                                        Stream = memory,
                                        ContentType = blobReference.Properties.ContentType,
                                        Name = blobReference.Metadata[MetadataName],
                                        Path = path
                                    };
                            }

                            return null;
                        });
        }

        public async Task<bool> SaveAsync(string container, string path, byte[] blob)
        {
            try
            {
                var containerReference = await GetContainerReferenceAsync(container);
                var blobReference = containerReference.GetBlockBlobReference(path);
                _writePolicy.ExecuteAction(() => blobReference.UploadFromByteArray(blob, 0, blob.Length));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SaveAsync(string container, FileWrapper file)
        {
            try
            {
                var containerRef = await GetContainerReferenceAsync(container);
                var blob = containerRef.GetBlockBlobReference(file.Path);

                blob.Properties.ContentType = file.ContentType;
                blob.Metadata[MetadataName] = file.Name;
                _writePolicy.ExecuteAction(() => blob.UploadFromStream(file.Stream));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SaveAsync(string container, string path, Stream stream)
        {
            try
            {
                var containerReference = await GetContainerReferenceAsync(container);
                var blobReference = containerReference.GetBlockBlobReference(path);
                _writePolicy.ExecuteAction(() => blobReference.UploadFromStream(stream));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<CloudBlobContainer> GetContainerReferenceAsync(string container)
        {
            try
            {
                var containerReference = _client.GetContainerReference(container);
                await containerReference.CreateIfNotExistsAsync();
                return containerReference;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Save(string container, FileWrapper file)
        {
            try
            {
                var containerRef = GetContainerReference(container);
                var blob = containerRef.GetBlockBlobReference(file.Path);

                blob.Properties.ContentType = file.ContentType;
                blob.Metadata[MetadataName] = file.Name;
                _writePolicy.ExecuteAction(() => blob.UploadFromStream(file.Stream));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private CloudBlobContainer GetContainerReference(string container)
        {
            var containerRef = _client.GetContainerReference(container);
            containerRef.CreateIfNotExists();
            return containerRef;
        }

        public Task DeleteAsync(string container, string path)
        {
            return
                GetContainerReferenceAsync(container)
                    .ContinueWith(
                        t =>
                        {
                            var blobReference = t.Result.GetBlockBlobReference(path);
                            _writePolicy.ExecuteAction(() => blobReference.DeleteIfExists());
                        });
        }

        public string MetadataName = "name";
    }
}