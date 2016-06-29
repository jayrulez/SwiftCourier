$(document).ready(function () {
    $('a.print-document').each(function (index) {
        $(this).on('click', function (e) {
            e.preventDefault();

            var document = window.open($(this).attr("href"), $(this).attr('data-document-name'), "width=900,height=600");

            document.window.print();
        });
    });
});