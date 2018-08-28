var cookieValue = getCookieByName("Token");
if (cookieValue === null) {
    changeMenuForLoggedIn(false);
} else {
    changeMenuForLoggedIn(true);
}

$.get("/UserAccount/UserLogin?handler=RetrieveUserClaims", function(result) {

});



function changeMenuForLoggedIn(isLoggedIn) {
    if (isLoggedIn) {
        $('#loginButton').hide();
        $('#accountButton').show();
    } else {
        $('#loginButton').show();
        $('#accountButton').hide();
    }
}

function getCookieByName(name) {
    var match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    if (match) return match[2];
}
