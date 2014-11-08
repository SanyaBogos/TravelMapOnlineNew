(function () {
    var app = angular.module('people', []);

    app.controller('SearchController', function ($http) {
        this.searchMask = '';
        this.people = [];
        this.followers = [];
        this.defaultAvatar = [];
        //this.fuck = 'baraad';

        //ppl = this.people;
        var ppl = this.people;
        var flw = this.followers;
        var avatar = this.defaultAvatar;
        this.searchPeople = function () {
            $http.get('/User/PeopleSearched?searchUser=' + this.searchMask).
            success(function (data, status, headers, config) {
                //this.people.length = 0;
                ppl.length = 0;
                for (var i = 0; i < data.length; i++) {
                    ppl.push(data[i]);
                }
            }).
        error(function (data, status, headers, config) {
            console.log(data);
        });

            $http.get('/User/GetFollowersForUser?id=null').
                success(function (data, status, headers, config) {
                    flw.length = 0;
                    for (var i = 0; i < data.length; i++) {
                        flw.push(data[i]);
                    }
                }).
            error(function (data, status, headers, config) {
                console.log(data);
            });

            $http.get('/User/GetUserStandartPhoto').
            success(function (data, status, headers, config) {
                //this.defaultAvatar = new string(data);
                //console.log(avatar);
                //avatar = new String(data);
                //avatar = [data];
                avatar.push(data.substring(1, data.length - 1));
                console.log(avatar);
            }).
            error(function (data, status, headers, config) {
                console.log(data);
            });
        };

        this.isShowFollow = function (userId) {
            var result = false;
            $.each(flw, function (index, value) {
                console.log('index=' + index + ' value=' + value.FollowerId);
                if (value.FollowerId == userId) { result = true; return false; }
            });
            return result;
        };

        this.followClick = function (manId) {
            $http.get('/User/AddFollower?id=' + manId).
            success(function (data, status, headers, config) {
                flw.push({ FollowerId: manId });
            }).
            error(function (data, status, headers, config) {
                console.log(data);
            });
        };

        this.unfollowClick = function (manId) {
            $http.get('/User/RemoveFollower?id=' + manId).
            success(function (data, status, headers, config) {
                var index;
                for (var i = 0; i < flw.length; i++) {
                    if (flw[i].FollowerId == manId) { index = i; break; }
                }
                if (index != null && index != undefined) { flw[index].FollowerId = null; }
            }).
            error(function (data, status, headers, config) {
                console.log(data);
            });
        };


    });
})();