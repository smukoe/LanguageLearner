$('#createAccountForm').submit(function (event) {
    var username = $('#usernameInput').val();
    var password = $('#passwordInput').val();
    
    event.preventDefault(); //Prevent normal submit
    if (username !== "" && password !== "") {
        checkDuplicateUsername(username, password);
    } else {
        alert('HEY! ENTER STUFF INTO BOTH BOXES');
    }         
});

$('#confirmPassword').keyup(function () {
    if ($(this).val() !== $('#passwordInput').val()) {
        $('#passwordMatchingError').html("Passwords do not match")
                                   .css("color", 'red')
                                   .show();
    } else {
        $('#passwordMatchingError').hide();
    }
});

function checkDuplicateUsername(username, password) {
    //GET request to see if username exists    
    $.get("/UserCreate?handler=CheckUsername", {Username: username}).done(function (result) {
        var isLoginCorrect = JSON.parse(JSON.stringify(result));
        if (isLoginCorrect.isDuplicate) {
            $('#usernameError').html('Username already exists');
            alert('Username already exists');           
        } else {
            $('#usernameError').html("Username doesn't already exist");
            submitDetails();            
        }       
    });
}

function submitDetails() {
    //Unbinds event so normal submit can resume
    $('#createAccountForm').unbind('submit').submit();
}

