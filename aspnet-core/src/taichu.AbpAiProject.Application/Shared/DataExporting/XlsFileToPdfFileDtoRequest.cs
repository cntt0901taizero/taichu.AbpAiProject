using FlexCel.Render;
using FlexCel.XlsAdapter;
using MediatR;
using BaseApplication.Dtos;
using BaseApplication.Factory;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BaseApplication.DataExporting
{
    public class XlsFileToPdfFileDtoRequest : IRequest<FileDto>
    {
        public XlsFile XlsResult { get; set; }
        public string OutputFileNameNotExtension { get; set; }
        public bool IsSetFileName { get; set; }
        public bool ViewAllSheet { get; set; } = false;
    }
    public class XlsFileToPdfFileDtoRequestHandler : IRequestHandler<XlsFileToPdfFileDtoRequest, FileDto>
    {
        private readonly IAppFactory _factory;

        public XlsFileToPdfFileDtoRequestHandler(IAppFactory factory)
        {
            _factory = factory;
        }

        public async Task<FileDto> Handle(XlsFileToPdfFileDtoRequest request, CancellationToken cancellationToken)
        {
            var fileNameOut = request.OutputFileNameNotExtension + ".pdf";
            const string fileType = "application/pdf";
            var outputFile = request.IsSetFileName ? new FileDto(fileNameOut, fileType, request.IsSetFileName) : new FileDto(fileNameOut, fileType);
            using (var msPdf = new MemoryStream())
            {
                using (var pdf = new FlexCelPdfExport(request.XlsResult, false))
                {
                    if (request.ViewAllSheet == true)
                    {
                        pdf.BeginExport(msPdf);
                        pdf.ExportAllVisibleSheets(false, "In");
                        pdf.EndExport();
                        //msPdf.Position = 0;
                    }
                    else
                    {
                        pdf.Export(msPdf);
                    }

                    await _factory.TempFileCacheManager.SetFileAsync(outputFile, msPdf.ToArray());
                    return outputFile;
                }
            }
        }
    }
}
