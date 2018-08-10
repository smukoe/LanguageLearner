$(function () {
    $('.showpassword').each(function (index, input) {
        var $input = $(input);
        $('#passwordVisibility').click(function () {
            var changeType = "";
            if ($(this).html() === "Show Password") {
                $(this).html("Hide Password");
                change = "text";
            } else {
                $(this).html("Show Password");
                change = "password";
            }
            var replace = $("<input type='" + change + "' />")
                .attr("id", $input.attr("id"))
                .attr("name", $input.attr("name"))
                .attr('class', $input.attr('class'))
                .val($input.val())
                .insertBefore($input);
            $input.remove();
            $input = replace;
        }).insertAfter($input);
    });
});

