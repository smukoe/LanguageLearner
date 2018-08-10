$('#loginSubmit').click(function () {
    var username = $('#usernameInput').val();
    var password = $('#passwordInput').val();
    //alert(username + ' ' + password);

    //GET request to see if username exists and if data is correct
    $.get("/UserAccount/UserLogin?handler=CheckUserLogin", { Username: username, Password: password }).done(function (result) {
        var isLoginCorrect = JSON.parse(JSON.stringify(result));
        if (isLoginCorrect.isDetailsMatch) {
            $('#loginError').html('User details are correct');
            $('#loginError').css('color' , 'green');
        }
        else {            
            $('#loginError').html('User details are incorrect');
            $('#loginError').css('color', 'red');
        }
        //alert(isLoginCorrect.isDetailsMatch);
    });
});

var inputAdjust = {
    position: 'relative',
    left: '10px'  
};

$('#usernameInput').css(inputAdjust);
    $('#passwordInput').css({ 'position': 'relative', 'left': '12px' });