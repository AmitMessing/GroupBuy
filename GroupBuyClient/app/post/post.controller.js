mainApp
    .controller('postController', [
        '$scope', '$stateParams', '$resource', 'userService', function ($scope, $stateParams, $resource, userService) {

            var postApi = $resource("/GroupBuyServer/api/products", {});

            $scope.product = {
                name: "",
                description: "",
                endDate: "",
                image: "",
                seller: {},
                basicPrice: "",
                discounts: {}
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
                $scope.product.discounts = $scope.discounts;
                $scope.product.seller = userService.getLoggedUser();
                postApi.save($scope.product).$promise.then(function(result) {
                    if (result) {
                        
                    }
                }, function(error) {
                    alert(error.data.Message);
                });
            };
            var imageLoader = document.getElementById('imageLoader');
            var canvas = document.getElementById('imageCanvas');
            var ctx = canvas.getContext('2d');

            function handleImage(e) {
                var reader = new FileReader();
                reader.onload = function (event) {
                    var img = new Image();
                    img.onload = function () {
                        ctx.imageSmoothingEnabled = false;
                        ctx.drawImage(img, 0, 0, img.width, img.height);
                    }
                    img.src = event.target.result;
                    canvas.width = img.width - 1;                         
                    canvas.height = img.height - 1;
                }
                reader.readAsDataURL(e.target.files[0]);
            }

            imageLoader.addEventListener('change', handleImage, false);
        }
    ]);