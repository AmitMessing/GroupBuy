mainApp
    .controller('postController', [
        '$scope', '$stateParams', '$resource', function ($scope, $stateParams, $resource) {

            var postApi = $resource("/GroupBuyServer/api/products", {});

            $scope.product = {
                name: "",
                description: "",
                endDate: "",
                image: "",
                seller: {},
                basicPrice: "",
                discounts: $scope.discounts
            };

            $scope.discounts = [
            {
                usersAmount: 5,
                precent: 10
            },
            {
                usersAmount: 15,
                precent: 20
            }];

            $scope.addDiscount = function() {
                $scope.discounts.push({ usersAmount: "", precent: "" });
            };

            $scope.save = function () {

                postApi.save($scope.product).$promise.then(function(result) {
                    if (result) {
                        
                    }
                }, function(error) {
                    alert(error.data.Message);
                });
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