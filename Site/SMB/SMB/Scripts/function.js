function submitContactForm() {
    var fullName = $('#name').val();
    var email = $('#email').val();
    var message = $('#message').val();

    if (fullName !== "" && email !== "" && message !== "") {
        $.ajax(
            {
                url: "/ContactUsForms/SubmitContactForm",
                data: { fullName: fullName, email: email, message: message },
                type: "GET"
            }).done(function (result) {
            if (result === "true") {
                $("#errorDiv").css('display', 'none');
                $("#SuccessDiv").css('display', 'block');
            }
            else {
                $("#errorDiv").html('an error acourred. please try again.');
                $("#errorDiv").css('display', 'block');
                $("#SuccessDiv").css('display', 'none');

            }
        });
    }
    else {
        $("#errorDiv").html('all field are required.');
        $("#errorDiv").css('display', 'block');
        $("#SuccessDiv").css('display', 'none');

    }
}

function submitServiceComment() {
    var fullName = $('#name').val();
    var email = $('#email').val();
    var comment = $('#comment').val();

    var url = window.location.pathname;
    var urlParam = url.substring(url.lastIndexOf('/') + 1);

    if (fullName !== "" && email !== "" && comment !== "") {
        $.ajax(
            {
                url: "/ServiceComments/SubmitComments",
                data: { fullName: fullName, email: email, comment: comment, urlParam: urlParam },
                type: "GET"
            }).done(function (result) {
            if (result === "true") {
                $("#errorDiv").css('display', 'none');
                $("#SuccessDiv").css('display', 'block');
            }
            else {
                $("#errorDiv").html('an error acourred. please try again.');
                $("#errorDiv").css('display', 'block');
                $("#SuccessDiv").css('display', 'none');

            }
        });
    }
    else {
        $("#errorDiv").html('all field are required.');
        $("#errorDiv").css('display', 'block');
        $("#SuccessDiv").css('display', 'none');

    }
}
function submitBlogComment() {
    var fullName = $('#name').val();
    var email = $('#email').val();
    var comment = $('#comment').val();

    var url = window.location.pathname;
    var urlParam = url.substring(url.lastIndexOf('/') + 1);

    if (fullName !== "" && email !== "" && comment !== "") {
        $.ajax(
            {
                url: "/BlogComments/SubmitComments",
                data: { fullName: fullName, email: email, comment: comment, urlParam: urlParam },
                type: "GET"
            }).done(function (result) {
            if (result === "true") {
                $("#errorDiv").css('display', 'none');
                $("#SuccessDiv").css('display', 'block');
            }
            else {
                $("#errorDiv").html('an error acourred. please try again.');
                $("#errorDiv").css('display', 'block');
                $("#SuccessDiv").css('display', 'none');

            }
        });
    }
    else {
        $("#errorDiv").html('all field are required.');
        $("#errorDiv").css('display', 'block');
        $("#SuccessDiv").css('display', 'none');

    }
}