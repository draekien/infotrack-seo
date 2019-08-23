$(function () {
    let numResults = 1;

    // initialize toasts
    $(".toast").toast({ delay: 3000 });

    // perform the search
    $("#submitCrawlForm").click(e => {
        e.preventDefault();

        const $form = $("#crawlForm");

        // check for empty inputs
        if ($("#Keyword").val() !== "" && $("#Uri").val() !== "") {
            let uri = $("#Uri", $form).val();

            // check for https and http, set default to https
            if (uri.indexOf("https://") < 0 && uri.indexOf("http://") < 0) {
                uri = `https://${uri}`;
                $("#Uri", $form).val(uri);
            }

            // get form data and target
            const formData = $form.serializeArray();
            const target = $form.attr("action");

            ajax_post_promise(target, formData).then((response) => {
                numResults++;
                $(".custom-card-deck").prepend($("#resultsTemplate").tmpl(response));
                if (numResults >= 3) {
                    $(".custom-card-deck").css("justify-content", "space-between");
                    $(".custom-card-deck .card-result").css("margin-right", "0");
                }
                showToast("Success", "Retrieved search engine results.");
            }).catch((response) => {
                showToast("Error", response);
            });
        } else {
            showToast("Error", "Form has empty input fields");
        }
    });
});

/**
 * ajax post with promise
 * @param {string} target
 * @param {object} data
 */
function ajax_post_promise(target, data) {
    return new window.Promise((resolve, reject) => {
        $.post({ url: target, data: data })
            .done((response) => {
                if (typeof response.success !== "undefined") {
                    resolve(response.success);
                } else {
                    reject(response.error);
                }
            })
            .fail((jqXhr, textStatus, errorThrown) => reject(errorThrown));
    });
}

/**
 * show toasts after modifying header and body
 * @param {string} header header of the toast
 * @param {string} body body / message of the toast
 */
function showToast(header, body) {
    const $toast = $(".toast");
    $("#toastHeader").html(header);
    $("#toastMessage").html(body);

    $toast.toast("show");
}