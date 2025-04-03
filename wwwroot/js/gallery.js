(function () {
    'use strict';

    angular
        .module('app')
        .controller('gallery', gallery);

    gallery.$inject = ['$location'];

    function gallery($location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'gallery';

        activate();

        function activate() { }
    }
})();
