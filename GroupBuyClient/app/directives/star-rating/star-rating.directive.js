mainApp
    .directive('starRating', function() {
    return {
        restrict: 'EA',
        template:
            '<ul class="star-rating">' +
                '  <li ng-repeat="star in stars" class="star" ng-class="{filled: star.filled}" ng-click="toggle($index)">' +
                '    <md-icon class="material-icons star-icon">grade</md-icon>' +
                '  </li>' +
                '</ul>',
        scope: {
            ratingValue: '=ngModel',
            max: '=?',
            onRatingSelect: '&?',
            readonly: '=?'
        },
        link: function(scope, element, attributes) {
            if (scope.max == undefined) {
                scope.max = 5;
            }

            function updateStars() {
                scope.stars = [];
                for (var i = 0; i < scope.max; i++) {
                    scope.stars.push({
                        filled: i < scope.ratingValue
                    });
                }
            };

            scope.toggle = function(index) {
                if (scope.readonly == undefined || scope.readonly === false) {
                    scope.ratingValue = index + 1;
                    scope.onRatingSelect({
                        rating: index + 1
                    });
                }
            };
            scope.$watch('ratingValue', function(oldValue, newValue) {
                if (newValue) {
                    updateStars();
                }
            });
        }
    };
});