
(function ($) {
    "use strict";

    function initializeFileManager(root) {
        var $root = $(root);

        if ($root.data("file-manager-initialized")) {
            return;
        }

        $root.data("file-manager-initialized", true);

        function readBoolean(name) {
            return String($root.attr(name)).toLowerCase() === "true";
        }

        var config = {
            uploadUrl: $root.attr("data-upload-url"),
            downloadUrl: $root.attr("data-download-url"),
            removeUrl: $root.attr("data-remove-url"),
            listUrl: $root.attr("data-list-url"),
            maxFileSize: Number($root.attr("data-max-file-size")) || 10485760,
            withDownload: readBoolean("data-with-download"),
            withRemove: readBoolean("data-with-remove")
        };

        var token = $('form').find('input[name="__RequestVerificationToken"]').val();
        var $input = $root.find(".ol-file-manager__input");
        var $message = $root.find(".ol-file-manager__message");
        var $files = $root.find(".ol-file-manager__files");
        var busy = false;

        // Hide unavailable actions completely.
        if (!config.withDownload) {
            $root.find(".ol-file-manager__download").remove();
        }

        if (!config.withRemove) {
            $root.find(".ol-file-manager__remove").remove();
        }

        function showMessage(text, isError) {
            $message
                .text(text || "")
                .toggleClass("text-danger", !!isError)
                .toggleClass("text-success", !isError && !!text);
        }

        function setBusy(value) {
            busy = value;

            $root.find("button").prop("disabled", value);
            $input.prop("disabled", value);
        }

        function request(options) {
            if (token && options.type && options.type.toUpperCase() === "POST") {

                // FormData requests (uploads)
                if (options.data instanceof FormData) {
                    if (!options.data.has("__RequestVerificationToken")) {
                        options.data.append("__RequestVerificationToken", token);
                    }
                }
                // Standard object posts
                else {
                    options.data = options.data || {};

                    if (!options.data.__RequestVerificationToken) {
                        options.data.__RequestVerificationToken = token;
                    }
                }
            }

            return $.ajax(options);
        }

        function escapeHtml(value) {
            return $("<div>")
                .text(value == null ? "" : String(value))
                .html();
        }

        function formatSize(bytes) {
            bytes = Number(bytes) || 0;

            if (bytes < 1024) {
                return bytes + " B";
            }

            if (bytes < 1024 * 1024) {
                return (bytes / 1024).toFixed(1) + " KB";
            }

            return (bytes / (1024 * 1024)).toFixed(1) + " MB";
        }

        function normalizeFiles(response) {
            // Supports a direct array or { files: [...] }.
            if (Array.isArray(response)) {
                return response;
            }

            if (response && Array.isArray(response.files)) {
                return response.files;
            }

            return [];
        }

        function getFileIcon(contentType) {

            if (!contentType) {
                return "fa-file";
            }

            if (contentType.includes("pdf")) {
                return "fa-file-pdf text-danger";
            }

            if (contentType.includes("image")) {
                return "fa-file-image text-success";
            }

            if (contentType.includes("word")) {
                return "fa-file-word text-primary";
            }

            if (contentType.includes("excel") || contentType.includes("spreadsheet")) {
                return "fa-file-excel text-success";
            }

            return "fa-file text-secondary";
        }

        function renderFiles(files) {

            if (!files.length) {
                $files.html(
                    '<div class="alert alert-info mb-0">' +
                    '<i class="fa fa-info-circle me-2"></i>' +
                    'No files uploaded.' +
                    '</div>'
                );
                return;
            }

            var html = files.map(function (file) {

                var reference = file.reference || "";
                var originalName = file.originalName || file.name || "";
                var contentType = file.contentType || "Unknown";
                var icon = getFileIcon(contentType);
                var pathWithName = file.pathWithName || "";
                var size = formatSize(file.size);

                var buttons = "";

                if (config.withDownload) {
                    buttons +=
                        '<button type="button" ' +
                        'class="btn btn-outline-primary btn-sm ol-file-manager__download me-2" ' +
                        'data-file-id="' + escapeHtml(pathWithName) + '">' +
                        '<i class="fa fa-download me-1"></i>Download' +
                        '</button>';
                }

                if (config.withRemove) {
                    buttons +=
                        '<button type="button" ' +
                        'class="btn btn-outline-danger btn-sm ol-file-manager__remove" ' +
                        'data-file-id="' + escapeHtml(pathWithName) + '">' +
                        '<i class="fa fa-trash me-1"></i>Remove' +
                        '</button>';
                }

                return [
                    '<div class="card shadow-sm mb-3">',
                    '<div class="card-body">',

                    '<div class="d-flex justify-content-between align-items-start">',

                    '<div class="flex-grow-1">',

                    '<h6 class="mb-2">',
                    '<i class="fa ' + icon + ' me-2 text-primary"></i>',
                    escapeHtml(originalName),
                    '</h6>',

                    '<div class="mb-2">',

                    '<span class="badge bg-secondary me-2">',
                    escapeHtml(contentType),
                    '</span>',

                    '<span class="badge bg-info text-dark">',
                    escapeHtml(size),
                    '</span>',

                    '</div>',

                    '<small class="text-muted d-block">',
                    escapeHtml(pathWithName),
                    '</small>',

                    '</div>',

                    '<div class="ms-3 text-nowrap">',
                    buttons,
                    '</div>',

                    '</div>',

                    '</div>',
                    '</div>'
                ].join("");

            }).join("");

            $files.html(html);
        }

        function refreshFiles() {
            return request({
                url: config.listUrl,
                type: "POST",
                dataType: "json"
            })
            .then(function (response) {
                if (!Array.isArray(response) &&
                    response &&
                    response.success === false) {
                    return $.Deferred().reject({
                        responseJSON: response
                    }).promise();
                }

                renderFiles(normalizeFiles(response));

                return response;
            })
            .fail(function (xhr, status, error) {

                console.error("AJAX Error");
                console.error("Status:", status);
                console.error("Error:", error);
                console.error("Response:", xhr.responseText);

            });
        }

        function refreshAfterSuccess(successMessage) {
            showMessage(successMessage, false);

            return refreshFiles().fail(function () {
                showMessage(
                    successMessage +
                    " However, the file list could not be refreshed.",
                    true
                );
            });
        }

        function getError(xhr, fallback) {
            if (xhr.responseJSON && xhr.responseJSON.message) {
                return xhr.responseJSON.message;
            }

            if (xhr.status === 400) {
                return "The request was rejected. Check the antiforgery token and parameters.";
            }

            if (xhr.status === 401 || xhr.status === 403) {
                return "You are not authorized to perform this action.";
            }

            return fallback;
        }

        // UPLOAD
        $root.on("click", ".ol-file-manager__upload", function () {
            if (busy) {
                return;
            }

            var file = $input[0].files[0];

            if (!file) {
                showMessage("Please select a file.", true);
                return;
            }

            if (file.size > config.maxFileSize) {
                showMessage("The selected file exceeds the permitted size.", true);
                return;
            }

            var data = new FormData();

            data.append("file", file);
            data.append("contentItemId", config.contentItemId);

            setBusy(true);
            showMessage("Uploading...", false);

            request({
                url: config.uploadUrl,
                type: "POST",
                data: data,
                processData: false,
                contentType: false,
                dataType: "json"
            }).done(function (response) {
                console.log("List response:", response);

                if (!response || response.success !== true) {
                    showMessage(
                        response && response.message
                            ? response.message
                            : "Upload failed.",
                        true
                    );
                    return;
                }

                refreshAfterSuccess(response.message || "Upload completed.");
            }).fail(function (xhr) {
                showMessage(getError(xhr, "Upload failed."), true);
            }).always(function () {
                $input.val("");
                setBusy(false);
            });
        });

        function downloadFile(path) {

            var form = document.createElement("form");

            form.method = "POST";
            form.action = config.downloadUrl;

            var pathInput = document.createElement("input");
            pathInput.type = "hidden";
            pathInput.name = "filepath";
            pathInput.value = path;

            var tokenInput = document.createElement("input");
            tokenInput.type = "hidden";
            tokenInput.name = "__RequestVerificationToken";
            tokenInput.value = token;

            form.appendChild(pathInput);
            form.appendChild(tokenInput);

            document.body.appendChild(form);

            form.submit();

            form.remove();
        }

        // DOWNLOAD
        $root.on("click", ".ol-file-manager__download", function () {
            if (busy || !config.withDownload) {
                return;
            }

            var path = $(this).attr("data-file-id");

            setBusy(true);
            showMessage("Preparing download...", false);

            downloadFile(path);

            setBusy(false);
            refreshAfterSuccess("Download requested.");
        });

        // REMOVE
        $root.on("click", ".ol-file-manager__remove", function () {
            if (busy || !config.withRemove) {
                return;
            }

            var path = $(this).attr("data-file-id");

            if (!window.confirm("Are you sure you want to remove this file?")) {
                return;
            }

            setBusy(true);
            showMessage("Removing file...", false);

            request({
                url: config.removeUrl,
                type: "POST",
                dataType: "json",
                data: {
                    filepath: path
                }
            }).done(function (response) {
                if (!response || response.success !== true) {
                    showMessage(
                        response && response.message
                            ? response.message
                            : "Remove failed.",
                        true
                    );
                    return;
                }

                refreshAfterSuccess(response.message || "File removed.");
            }).fail(function (xhr) {
                showMessage(getError(xhr, "Remove failed."), true);
            }).always(function () {
                setBusy(false);
            });
        });

        // Initial file list.
        refreshFiles().fail(function (xhr) {
            showMessage(getError(xhr, "Unable to load the file list."), true);
        });
    }

    window.GdsFileManager = {
        initialize: function () {
            $(".ol-file-manager").each(function () {
                initializeFileManager(this);
            });
        }
    };

    $(document).ready(function () {
        window.GdsFileManager.initialize();
    });

})(jQuery);
