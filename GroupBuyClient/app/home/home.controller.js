mainApp
    .controller('homeController', ['$scope', function ($scope) {
    $scope.products =
    [
        {
            name: 'Lime Flippers',
            content: 'Flippers are a nice tool to have when youre being chased by an oversized sea turtle...',
            img: 'https://www.gstatic.com/angular/material-adaptive/shrine/flippers.png',
            price: 45,
        },
        {
            name: 'Vintage Radio',
            content: 'Isnt it cool when things look old, but their not. Looks Old But Not makes awesome...',
            img: 'https://www.gstatic.com/angular/material-adaptive/shrine/radio.png',
            price: 112,
        },
        {
            name: 'Red Popsicle',
            content: 'Looks can be deceiving. This Red Popsicle comes in a wide variety of flavors,..',
            img: 'https://www.gstatic.com/angular/material-adaptive/shrine/popsicle.png',
            price: 5,
        },
        {
            name: 'Green Slip-ons',
            content: 'Feetsy has been making extraordinary slip-ons for decades. With each pair of shoes...',
            img: 'https://www.gstatic.com/angular/material-adaptive/shrine/green-shoes.png',
            price: 23,
        },
        {
            name: 'Sunglasses',
            content: 'Be an optimist. Carry Sunglasses with you at all times. All Tints and Shades products come with polar...',
            img: 'https://www.gstatic.com/angular/material-adaptive/shrine/sunnies.png',
            price: 40,
        },
        {
            name: 'Red Lipstick Set',
            content: 'Trying to find the perfect shade to match your mood? Try no longer. Red Lipstick...',
            img: 'https://www.gstatic.com/angular/material-adaptive/shrine/lipstick.png',
            price: 45,
        },
    ];
}]);