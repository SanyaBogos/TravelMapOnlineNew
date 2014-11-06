
function updateMap() {
    var userMap = [],
        userCountries = [],
        user;
    $.get("User/GetCurrentUser", function (data) {
        user = data;
        console.log(user);
    }).done(function () {
        $.get("User/GetUserVisitedCountries?id=" + user, function (data) {
            userCountries = data;
            for (var i = 0; i < userCountries.length; i++) {
                userMap.push({ id: userCountries[i].title+"", showAsSelected: true });
            }
            AmMyMaps(userMap);
        });
    });
    
    
}

document.onload = updateMap();