using System;
using FlexCel.Render;
using FlexCel.Report;
using FlexCel.XlsAdapter;
using MediatR;
using BaseApplication.Dtos;
using BaseApplication.Factory;
using BaseApplication.Helper;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static BaseApplication.DataExporting.ExportFlexCelBase;

namespace BaseApplication.DataExporting
{
    /// <summary>
    /// SampleFile : Tên của file mẫu, mặc định trong thư mục sampleFiles/flexcel
    /// OutputFileNameNotExtension: Tên file xuất không có đuôi
    /// OutputFileType: Loại (excel,pdf)
    /// </summary>
    public class ExportFlexCelReportRequest: IRequest<FileDto>
    {
        public string SampleFile { get; set; }
        public bool IsSetFileName { get; set; }
        public string SampleFileFolder { get; set; }
        public string CoverSampleFile { get; set; }
        public string CoverSampleFileFolder { get; set; }
        public string OutputFileNameNotExtension { get; set; }
        public OutputFileExtension OutputFileType { get; set; }
        public Action<FlexCelReport> FlexCelAction { get; set; }


    }

    public class ExportFlexCelReportRequestHandler : IRequestHandler<ExportFlexCelReportRequest, FileDto>
    {
        private readonly IAppFactory _factory;

        public ExportFlexCelReportRequestHandler(IAppFactory factory)
        {
            _factory = factory;
        }

        public async Task<FileDto> Handle(ExportFlexCelReportRequest request, CancellationToken cancellationToken)
        {
            try
            {

                if (string.IsNullOrEmpty(request.SampleFileFolder))
                {
                    request.SampleFileFolder = "sampleFiles/flex-cel";
                }
                var path = Path.Combine(Directory.GetCurrentDirectory(),
                    request.SampleFile);
                var resultXls = new XlsFile(true);
                resultXls.Open(path);
                using (var fr = new FlexCelReport())
                {
                    request.FlexCelAction(fr);
                    fr.Run(resultXls);
                    fr.Dispose();
                }

                //cover
                if (!string.IsNullOrEmpty(request.CoverSampleFile) && !string.IsNullOrEmpty(request.CoverSampleFileFolder))
                {
                    var coverpath = Path.Combine(Directory.GetCurrentDirectory(),
                    request.CoverSampleFileFolder,
                    request.CoverSampleFile);
                    var coverresultXls = new XlsFile(true);
                    coverresultXls.Open(coverpath);
                    using (var coverfr = new FlexCelReport())
                    {
                        request.FlexCelAction(coverfr);
                        coverfr.Run(coverresultXls);
                        resultXls.InsertAndCopySheets(1, 1, 1, coverresultXls);
                        //resultXls.ActiveSheet = 1;
                        coverfr.Dispose();
                    }
                };

                switch (request.OutputFileType)
                {
                    case OutputFileExtension.Excel2003:
                        return await _factory.Mediator.Send(new XlsFileToExcelFileDtoRequest
                        {
                            XlsResult = resultXls,
                            OutputFileNameNotExtension = request.OutputFileNameNotExtension,
                            IsFileExcel2003 = true
                        }, cancellationToken);
                    case OutputFileExtension.Excel:
                        return await _factory.Mediator.Send(new XlsFileToExcelFileDtoRequest
                        {
                            XlsResult = resultXls,
                            OutputFileNameNotExtension = request.OutputFileNameNotExtension,
                            IsFileExcel2003 = false
                        }, cancellationToken);
                    case OutputFileExtension.Pdf:
                        return await _factory.Mediator.Send(new XlsFileToPdfFileDtoRequest
                        {
                            XlsResult = resultXls,
                            OutputFileNameNotExtension = request.OutputFileNameNotExtension,
                            IsSetFileName = request.IsSetFileName
                        }, cancellationToken);
                    case OutputFileExtension.PdfAllSheet:
                        return await _factory.Mediator.Send(new XlsFileToPdfFileDtoRequest
                        {
                            XlsResult = resultXls,
                            OutputFileNameNotExtension = request.OutputFileNameNotExtension,
                            IsSetFileName = request.IsSetFileName,
                            ViewAllSheet = true
                        }, cancellationToken);

                    default:
                        return new FileDto();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
