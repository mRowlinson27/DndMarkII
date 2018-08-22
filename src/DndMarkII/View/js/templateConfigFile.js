(function () {
    ko.components.register("title-zone", {
        template: { require: "text!./tpl/titleZoneTemplate.html" }
    });
    ko.components.register("skill-table", {
        template: { require: "text!./tpl/skillTableTemplate.html" }
    });
    ko.components.register("primary-stats-table", {
        template: { require: "text!./tpl/primaryStatsTableTemplate.html" }
    });
}());