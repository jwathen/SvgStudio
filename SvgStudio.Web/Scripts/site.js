$(function () {
    $('.validation-summary-errors').each(function () {
        $(this).addClass('alert');
        $(this).addClass('alert-danger');
    });
    $('form').each(function () {
        $(this).find('div.form-group').each(function () {
            if ($(this).find('input.input-validation-error').length > 0) {
                $(this).addClass('has-error');
                $(this).find('input.input-validation-error').
                   removeClass('input-validation-error');
            }
        });
    });

    $('#themeSelector').change(function () {
        var theme = $(this).val().toLowerCase();
        $('#themeCssLink').attr('href', 'https://maxcdn.bootstrapcdn.com/bootswatch/3.3.6/' + theme + '/bootstrap.min.css');
    });
});