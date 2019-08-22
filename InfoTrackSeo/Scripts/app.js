$(function () {
    $(".toast").toast({ delay: 3000 });

    $("#submitCrawlForm").click(e => {
        e.preventDefault();

        var $form = $("#crawlForm");
        var formData = $form.serializeArray();

        var target = $form.attr("action");

        ajax_post_promise(target, formData).then((response) => {
            $(".card-deck").append($("#resultsTemplate").tmpl(response));
        }).catch((response) => {
            // TODO: handle errors
            $("#toastError").toast("show");
        });
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