var isPasswordLengthValid = false;

$('#createAccountForm').submit(function (event) {
    var username = $('#usernameInput').val();
    var password = $('#passwordInput').val();
    
    event.preventDefault(); //Prevent normal submit

    if (username !== "" && password !== "" && isPasswordLengthValid) {
        $.when(checkDuplicateUsername(username), isValidPasswordInput(password)).done(function (duplicateUsernameResult, validPasswordResult) {
            var isUsernameValid = JSON.parse(JSON.stringify(duplicateUsernameResult));
            var isPasswordValid = JSON.parse(JSON.stringify(validPasswordResult));
            
            if (isUsernameValid[0].isValid && isPasswordValid[0].isValid) {
                $('#usernameError').hide();
                $("#passwordError").hide();
                submitDetails();
            }

            if (!isUsernameValid[0].isValid) {                
                $('#usernameError').html('Username already exists')
                    .css("color", 'red')
                    .show();                              
            }

            if (!isPasswordValid[0].isValid) {                
                $("#passwordError").html('Password must only consist of lower and upper-case characters and numbers')
                    .css("color", 'red')
                    .show();                
            }
        });
       
    } else if (username === "" || password === "") {
        alert("One or more fields have been left empty");

    } else {
        alert("Invalid inputs");
    }         
});

$("#passwordInput").keyup(function () {
    var password = $(this).val();
    if (password.length < 8 || password.length > 15) {
        isPasswordLengthValid = false;
        $("#passwordError").html("Password length must be between 8-15 characters")
            .css("color", 'red')
            .show();       
    } else {
        isPasswordLengthValid = true;
        $("#passwordError").hide();
    }

    checkIfPasswordsMatch();
});

$('.matchingPasswordInput').keyup(function () {
    checkIfPasswordsMatch();
});

function checkIfPasswordsMatch() {
    if ($("#confirmPassword").val() !== $('#passwordInput').val()) {
        $('#passwordMatchingError').html("Passwords do not match")
            .css("color", 'red')
            .show();

    } else {
        $('#passwordMatchingError').hide();
    }
}

function checkDuplicateUsername(username) {       
    return $.get("/UserAccount/UserCreate?handler=IsDuplicateUsername", { Username: username }).done(function (duplicateUsernameResult) {       
    });
}

function isValidPasswordInput(password) {
    return $.get("/UserAccount/UserCreate?handler=IsValidPassword", { Password: password }).done(function (validPasswordResult) {       
    });
}

function submitDetails() {  
    $('#createAccountForm').unbind('submit').submit();
}

