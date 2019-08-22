$(function () {
    $(".toast").toast({ delay: 3000 });

    $("#submitCrawlForm").click(e => {
        e.preventDefault();

        var $form = $("#crawlForm");

        if ($("#Keyword").val() !== "" && $("#Uri").val() !== "") {
            const formData = $form.serializeArray();
            const target = $form.attr("action");

            ajax_post_promise(target, formData).then((response) => {
                $(".card-deck").append($("#resultsTemplate").tmpl(response));
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
    return new Promise((resolve, reject) => {
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

function showToast(header, body) {
    const $toast = $(".toast");
    $("#toastHeader").html(header);
    $("#toastMessage").html(body);

    $toast.toast("show");
}