using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using PsychologicalSupportPlatform.Report.Application.Interfaces;
using PsychologicalSupportPlatform.Report.Infrastructure.Config;

namespace PsychologicalSupportPlatform.Report.Infrastructure.Data.Repositories;

public class MinioRepository: IMinioRepository
{
    private readonly IMinioClient _minioClient;
    private readonly IOptions<MinioReportsConfig> _options;

    public MinioRepository(IOptions<MinioReportsConfig> minioConfig, IMinioClient minioClient)
    {
        _minioClient = minioClient;
        _options = minioConfig;
    }
    
    public async Task UploadReportAsync(Stream data, string objectName)
    {
        data.Seek(0, SeekOrigin.Begin);
        var beArgs = new BucketExistsArgs().WithBucket(_options.Value.BucketName);
        var found = await _minioClient.BucketExistsAsync(beArgs).ConfigureAwait(false);
        
        if (!found)
        {
            var mbArgs = new MakeBucketArgs().WithBucket(_options.Value.BucketName);
            await _minioClient.MakeBucketAsync(mbArgs).ConfigureAwait(false);
        }

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_options.Value.BucketName)
            .WithObject(objectName)
            .WithStreamData(data)
            .WithObjectSize(data.Length);
        await _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
    }

    public async Task<MemoryStream> DownloadReportAsync(string objectName, CancellationToken token)
    {
        var outputStream = new MemoryStream();
        var getObjectArgs = new GetObjectArgs()
            .WithBucket(_options.Value.BucketName)
            .WithObject(objectName)
            .WithCallbackStream((stream) =>
            {
                stream.CopyTo(outputStream);
            });
        await _minioClient.GetObjectAsync(getObjectArgs, token);
        outputStream.Seek(0, SeekOrigin.Begin);
        
        return outputStream;
    }
}
