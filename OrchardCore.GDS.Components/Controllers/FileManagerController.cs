using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrchardCore.Admin;
using OrchardCore.GDS.Components.Handlers;
using System.Text;
using Constants = OrchardCore.GDS.Components.Config.Constants;

namespace OrchardCore.GDS.Components.Controllers
{
    [Admin]
    [Authorize]
    public class FileManagerController : Controller
    {
        private readonly IBlobFilesHandler _blobFilesHandler;
        private readonly IBlobFilesHelper _blobFilesHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAntiforgery _antiforgery;
        private readonly ILogger<FileManagerController> _logger;

        public FileManagerController(
            IBlobFilesHandler blobFilesHandler,
            IBlobFilesHelper blobFilesHelper,
            IHttpContextAccessor httpContextAccessor,
            IAntiforgery antiforgery,
            ILogger<FileManagerController> logger)
        {
            _blobFilesHandler = blobFilesHandler;
            _blobFilesHelper = blobFilesHelper;
            _httpContextAccessor = httpContextAccessor;
            _antiforgery = antiforgery;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/filemanager/list")]
        public async Task<IActionResult> List()
        {
            if (!await _antiforgery.IsRequestValidAsync(HttpContext))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid antiforgery token."
                });
            }

            try
            {
                var files = await _blobFilesHandler.GetStoredFilesAsync(GetReference());


                return Json(new
                {
                    success = true,
                    files
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load files.");

                return StatusCode(500, new
                {
                    success = false,
                    message = "Unable to retrieve files."
                });
            }
        }

        [HttpPost]
        [Route("api/filemanager/upload")]
        [RequestSizeLimit(10485760)] // 10 MB
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (!await _antiforgery.IsRequestValidAsync(HttpContext))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid antiforgery token."
                });
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "No file supplied."
                });
            }

            try
            {
                await _blobFilesHelper.UploadFile(file, GetReference());

                return Json(new
                {
                    success = true,
                    message = "File uploaded successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Upload failed.");

                return StatusCode(500, new
                {
                    success = false,
                    message = "Upload failed."
                });
            }
        }

        [HttpPost]
        [Route("api/filemanager/download")]
        public async Task<IActionResult> Download(string filepath)
        {
            if (!await _antiforgery.IsRequestValidAsync(HttpContext))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid antiforgery token."
                });
            }

            if (string.IsNullOrWhiteSpace(filepath))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "File path is required."
                });
            }

            try
            {
                var file = await _blobFilesHandler.DownloadAsync(filepath);

                if (file == null || file.Content == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "File not found."
                    });
                }

                return File(
                    file.Content,
                    file.ContentType ?? "application/octet-stream",
                    file.OriginalName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File download failed.");

                return StatusCode(500, new
                {
                    success = false,
                    message = "Download failed."
                });
            }
        }

        [HttpPost]
        [Route("api/filemanager/remove")]
        public async Task<IActionResult> Remove(string filepath)
        {
            if (!await _antiforgery.IsRequestValidAsync(HttpContext))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid antiforgery token."
                });
            }

            if (string.IsNullOrWhiteSpace(filepath))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "File path is required."
                });
            }

            try
            {
                var deleted = await _blobFilesHandler.DeleteAsync(filepath);

                return Json(new
                {
                    success = deleted,
                    message = deleted
                        ? "File removed successfully."
                        : "File could not be removed."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "File removal failed.");

                return StatusCode(500, new
                {
                    success = false,
                    message = "Remove failed."
                });
            }
        }

        private string GetReference()
        {
            string result;

            var session = _httpContextAccessor.HttpContext?.Session;

            if (string.IsNullOrEmpty(session?.GetString(Constants.Files.SessionKey)))
            {
                result = DateTime.UtcNow.Ticks.ToString("X");

                session?.SetString(Constants.Files.SessionKey, result);
            }
            else
            {
                result = session.GetString(Constants.Files.SessionKey)!;
            }

            return result;
        }

    }
}