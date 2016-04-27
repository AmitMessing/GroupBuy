mainApp
    .controller('productController', [
        '$scope', '$stateParams', function($scope, $stateParams) {
            $scope.calcPrice = function(price, discount) {
                return price - ((price * discount) / 100);
            };
            $scope.products =
            [
                {
                    id: 1,
                    name: 'Lime Flippers',
                    content: 'Flippers are a nice tool to have when youre being chased by an oversized sea turtle. Never get caught again with these fast water shoes. Youre like a fish, but more graceful.Flippers are a nice tool to have when youre being chased by an oversized sea turtle. Never get caught again with these fast water shoes. Youre like a fish, but more graceful.',
                    img: 'https://www.gstatic.com/angular/material-adaptive/shrine/flippers.png',
                    price: 45,
                    discount: [{ amount: 20, dis: 20 }, { amount: 50, dis: 25, isCurrent: true }, { amount: 200, dis: 32 }]
                },
                {
                    id: 2,
                    name: 'Vintage Radio',
                    content: 'Isnt it cool when things look old, but their not. Looks Old But Not makes awesome vintage goods that are super smart. This ol’ radio just got an upgrade. Connect to it with an app and jam out to some top forty.Isnt it cool when things look old, but their not. Looks Old But Not makes awesome vintage goods that are super smart. This ol’ radio just got an upgrade. Connect to it with an app and jam out to some top forty.',
                    img: 'https://www.gstatic.com/angular/material-adaptive/shrine/radio.png',
                    price: 112,
                },
                {
                    id: 3,
                    name: 'Red Popsicle',
                    content: 'Looks can be deceiving. This Red Popsicle comes in a wide variety of flavors, including strawberry, that burst as soon as it hits the mouth. Red Popsicles melt slow, so savor the flavor. Looks can be deceiving. This Red Popsicle comes in a wide variety of flavors, including strawberry, that burst as soon as it hits the mouth. Red Popsicles melt slow, so savor the flavor.',
                    img: 'https://www.gstatic.com/angular/material-adaptive/shrine/popsicle.png',
                    price: 5,
                },
                {
                    id: 4,
                    name: 'Green Slip-ons',
                    content: 'Feetsy has been making extraordinary slip-ons for decades. With each pair of shoes purchased Feetsy donates a pair to those in need. Buy yourself a pair, buy someone else a pair. Very Comfortable.',
                    img: 'https://www.gstatic.com/angular/material-adaptive/shrine/green-shoes.png',
                    price: 23,
                },
                {
                    id: 5,
                    name: 'Sunglasses',
                    content: 'Be an optimist. Carry Sunglasses with you at all times. All Tints and Shades products come with polarized lenses and super duper UV protection so you can look at the sun for however long you want. Sunglasses make you look cool, wear them.',
                    img: 'https://www.gstatic.com/angular/material-adaptive/shrine/sunnies.png',
                    price: 40,
                },
                {
                    id: 6,
                    name: 'Red Lipstick Set',
                    content: 'Trying to find the perfect shade to match your mood? Try no longer. Red Lipstick Set by StickLips has you covered for those nights when you need to get out, or even if you’re just headed to work.',
                    img: 'https://www.gstatic.com/angular/material-adaptive/shrine/lipstick.png',
                    price: 45,
                },
            ];

            $scope.product = $scope.products[$stateParams.id - 1];
        }
    ]);