var map;
function AmMyMaps(userMap) {

    //var newCountry;

    //AmCharts.ready(function () {

    map = new AmCharts.AmMap();

    map.pathToImages = "../../../Images/ammap/";
    map.panEventsEnabled = true;

    map.zoomControl.panControlEnabled = true;
    map.zoomControl.zoomControlEnabled = true;
    map.zoomControl.buttonFillColor = "#282828";

    map.zoomDuration = 0.1;
    map.mouseWheelZoomEnabled = true;
    map.selectable = true;

    var smallMap = {
        backgroundAlpha: 0.5,
        borderThickness: 1
    };

    var dataProvider = {
        map: "worldLow",
        getAreasFromMap: true,
        areas: []
    };
    if (userMap) {
        dataProvider.areas = userMap;
    }
    map.dataProvider = dataProvider;
    map.smallMap = smallMap;
    map.areasSettings = {
        autoZoom: false,
        color: "#CDCDCD",
        colorSolid: "#5EB7DE",
        selectedColor: "#5EB7DE",
        outlineColor: "#666666",
        rollOverColor: "#88CAE7",
        rollOverOutlineColor: "#FFFFFF",
        selectable: true
    };

    map.addListener('clickMapObject', function (event) {
        angular.element("#mapdiv").scope().clickMap(event.mapObject/*.title*/);
    });

    function handleGoHome() {
        map.dataProvider = continentsDataProvider;
        map.validateNow();
    }

    map.addListener("homeButtonClicked", handleGoHome);
    map.write("mapdiv");

    //});

};