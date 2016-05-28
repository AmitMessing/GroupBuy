mainApp
    .controller('postController', [
        '$scope', '$stateParams', function ($scope, $stateParams) {

            $scope.discounts = [
            {
                numberOfBuyers: 5,
                value: 10
            },
            {
                numberOfBuyers: 15,
                value: 20
            }];

            $scope.addDiscount = function() {
                $scope.discounts.push({numberOfBuyers: "", value:""});
            };

            $scope.save = function() {

            };

            var imageLoader = document.getElementById('imageLoader');
            imageLoader.addEventListener('change', handleImage, false);
            var canvas = document.getElementById('imageCanvas');
            var ctx = canvas.getContext('2d');

            function handleImage(e) {
                var reader = new FileReader();
                reader.onload = function (event) {
                    var img = new Image();
                    img.onload = function () {
                        ctx.drawImage(img, 0, 0, canvas.width, canvas.height);
                    }
                    img.src = event.target.result;
                }
                reader.readAsDataURL(e.target.files[0]);
            }
        }
    ]);